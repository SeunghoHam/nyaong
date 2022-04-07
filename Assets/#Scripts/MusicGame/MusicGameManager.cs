using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicGameManager : MonoBehaviour
{
    public static MusicGameManager instance;

    public GameObject go_PoolingObject;

    Queue<Note> poolingObjectQueue = new Queue<Note>();



     int maxObjCount= 10;

	private void Awake()
	{
        instance = this;
        Initialize(maxObjCount);
	}

    void Initialize(int initCount)
	{
		for (int i = 0; i < initCount; i++)
		{
            poolingObjectQueue.Enqueue(CreateNewObject());
		}
	}
	Note CreateNewObject()
	{
        var newObj = Instantiate(go_PoolingObject).GetComponent<Note>();
        newObj.gameObject.SetActive(false); // 생성할때는 비활성화상태로
        newObj.transform.SetParent(transform);
        newObj.transform.position = this.transform.position;
        Debug.Log(newObj.transform.position);
        return newObj;
    }
    public static Note GetObject() //오브젝트를 활성화 시킨다. (풀링 된 다음 활성화 시키기)
	{
        
        if(instance.poolingObjectQueue.Count > 0)
		{
            
            var obj = instance.poolingObjectQueue.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
		}
        else // 풀링되어있는 객체가 하나도 없을 때
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
        instance.poolingObjectQueue.Enqueue(obj);
	}
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var note = GetObject();
            var direction = Vector3.left;
            note.transform.position = direction.normalized;
            note.Move(direction.normalized);
        }
    }
}
