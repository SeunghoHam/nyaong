using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]
public class MonsterPool : MonoBehaviour
{
    public static MonsterPool Instance;

    [SerializeField] GameObject poolingObjectPrefab;
    [SerializeField] Transform ActivePool;
    [SerializeField] Transform DeActivePool;
    Queue<Monster> poolingObjectQueue = new Queue<Monster>();

    private void Awake()
    {
        Instance = this;

    }


    public void Init(int _initCount)
    {
        Initialize(_initCount);
    }
    void Initialize(int initCount)
    {
        for (int i = 0; i < initCount; i++)
        {
            poolingObjectQueue.Enqueue(CreateNewMonster());
        }
    }
    Monster CreateNewMonster()
    {
        var newMonster = Instantiate(poolingObjectPrefab).GetComponent<Monster>();
        newMonster.transform.SetParent(DeActivePool);
        newMonster.transform.position = DeActivePool.position;
        newMonster.gameObject.SetActive(false);
        return newMonster;
    }
    public static Monster GetMonster()
    {
        //Debug.Log("풀링오브젝트큐 카운트 : " + Instance.poolingObjectQueue.Count);
        if(Instance.poolingObjectQueue.Count > 0)
        {
            Debug.Log("<color=fushsia>" + "오브젝트 생성" + " </color>");
            var obj = Instance.poolingObjectQueue.Dequeue();
            obj.transform.SetParent(MonsterPool.Instance.ActivePool); // 어디 자식으로 할당되는게 아니라 하이어라키에 그냥 존재하게된다.[ 수정필요 ]
            obj.gameObject.SetActive(true);
            //obj.gameObject.GetComponent<Monster>().move
            return obj;
        }
        else
        {
            Debug.Log("<color = magenta>"  +"초기 설정값보다 더 생성하려고 함 </color>");
            var newObj = Instance.CreateNewMonster();
            newObj.gameObject.SetActive(true);
            newObj.transform.SetParent(MonsterPool.Instance.ActivePool);
            return newObj;
        }
    }

    /// <summary> 반환인데, 반환이 필요할지는 모르겠다. </summary>
    /// <param name="_monster"></param>
    public static void ReturnObject(Monster _monster) 
    {
        _monster.gameObject.SetActive(false);
        _monster.transform.SetParent(Instance.DeActivePool);
        Instance.poolingObjectQueue.Enqueue(_monster);
    }


}
