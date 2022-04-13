using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiManager : MonoBehaviour
{
	public static MultiManager instance;
	public CanvasManager canvas;

	public GameObject go_noteRed;
	public GameObject go_noteBlue;
	public GameObject go_noteDouble;


	Queue<Note> randomNoteQueue = new Queue<Note>(); // 오브젝트 풀링 용 큐
	Queue<Note> activeNoteQueue = new Queue<Note>(); // 활성화 되어있는 오브젝트 받아오는 큐

	public Note nearNote;

	public BoxCollider judgeLine_Perfect;
	public BoxCollider judgeLine_Normal;
	public BoxCollider judgeLine_Bad;
	public bool bPerfect;
	public bool bGood;
	public bool bBad;

	public bool note_Red;
	public bool note_Blue;
	//List<Note> ActiveNote = new List<Note>();
	private void Awake()
	{
		instance = this;
		Initialize(20);
	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			var note = GetObject();
			Vector3 direction = Vector3.left;
			//note.transform.position = direction.normalized;
			note.transform.position = this.transform.position;
			note.Move(direction.normalized);
			activeNoteQueue.Enqueue(note);
	
			//arrayUpdate();
		}
		//Debug.Log(activeNoteQueue.Count);
	}
	public Note callBack(Note note)
	{
		return note;
	}
	public void hit()
	{
		Debug.Log("웬디고 아파하는중");
		ReturnObject(activeNoteQueue.Dequeue());
		StartCoroutine( canvas.CRT_sliderValueSmooth_Decrease(5));
	}
	public void FuncJudge_Perfect()
	{
		Debug.Log("판정_퍼펙트");
		ReturnObject(activeNoteQueue.Dequeue());
	}
	public void FuncJudge_Good()
	{
		Debug.Log("판정_굿");

		ReturnObject(activeNoteQueue.Dequeue());

	}
	public void FuncJudge_Bad()
	{
		Debug.Log("판정_배드");

		ReturnObject(activeNoteQueue.Dequeue());
	}



	public void StateReset()
	{
		bBad = false;
		bGood = false;
		bPerfect = false;
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
		var newObj = Instantiate(returnRandomPrefab()).GetComponent<Note>(); // 두개의 프리팹 랜덤으로 생성되게 해봐야함
		newObj.gameObject.SetActive(false); // 생성할때는 비활성화상태로
		newObj.transform.SetParent(transform);
		newObj.transform.position = this.transform.position;
		//Debug.Log(newObj.transform.position);
		return newObj;
	}
	public static Note GetObject() //오브젝트를 활성화 시킨다. (풀링 된 다음 활성화 시키기)
	{
		
		if (instance.randomNoteQueue.Count > 0)
		{
			
			var obj = instance.randomNoteQueue.Dequeue();
			//obj.transform.SetParent();
			//obj.gameObject.transform.SetParent();
			obj.gameObject.SetActive(true);
			
			return obj;
		}
		else // 풀링되어있는 객체가 하나도 없을 때 & initialize 한 갯수보다 더 생성해야할 때
		{
			var newObj = instance.CreateNewObject();
			newObj.gameObject.SetActive(true);
			newObj.transform.SetParent(null);
			return newObj;
		}
	}
	public static void ReturnObject(Note obj)
	{
		obj.gameObject.SetActive(false);
		obj.transform.SetParent(instance.transform);
		instance.randomNoteQueue.Enqueue(obj);
	}

	GameObject returnRandomPrefab()
	{
		int count = Random.Range(0, 2);
		GameObject returnObject = null;
		switch (count)
		{
			case 0:
				returnObject = go_noteRed;
				break;
			case 1:
				returnObject = go_noteBlue;
				break;
		}
		return returnObject;

	}
}
