using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiManager : MonoBehaviour
{
    public static MultiManager instance;

    public GameObject go_noteRed;
    public GameObject go_noteBlue;
    Queue<Note> randomNoteQueue = new Queue<Note>();

	private void Awake()
	{
		instance = this;
	}
	void Initialize(int initCount)
	{
		for (int i = 0; i < initCount; i++)
		{
			randomNoteQueue.Enqueue(CreateNewObject());
		}
	}
	Note CreateNewObject()
	{
		var newObj = Instantiate(go_noteRed).GetComponent<Note>(); // 두개의 프리팹 랜덤으로 생성되게 해봐야함
		newObj.gameObject.SetActive(false); // 생성할때는 비활성화상태로
		newObj.transform.SetParent(transform);
		newObj.transform.position = this.transform.position;
		Debug.Log(newObj.transform.position);
		return newObj;
	}

}
