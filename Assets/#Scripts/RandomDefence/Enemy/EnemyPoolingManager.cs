using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using KeyType = System.String;

/// <summary>
/// 오브젝트 풀 관리 싱글톤
/// </summary>
/// 
[DisallowMultipleComponent] // 클래스 중복존재 방지
public class EnemyPoolingManager : MonoBehaviour
{
    // 인스펙터에서 오브젝트 풀링 대상 정보 추가
    [SerializeField]
    private List<PoolObjectData> _poolObjectDataList = new List<PoolObjectData>(4);

    // 복제될 오브젝트의 원본 딕셔너리
    private Dictionary<KeyType, PoolObject> _originDict; // Key - 복제용 오브젝트 원본

    // 풀링 정보 딕셔너리
    private Dictionary<KeyType, PoolObjectData> _dataDict; // Key - 풀 정보

    // 풀 딕셔너리
    private Dictionary<KeyType, Stack<GameObject>> _poolDict; // Key - 풀

    private Dictionary<GameObject, Stack<GameObject>> _clonePoolDict; // 복제된 게임오브젝트 - 풀


    private void Awake()
    {
        Init();
    }
    
    private void Init()
    {
        int len = _poolObjectDataList.Count;
        if (len == 0) return; // PoolingObjectData르 담고있는 리스트의 개수가 0 이라면 반환시킨다.

        // 1. 딕셔너리 생성
        _originDict = new Dictionary<KeyType, PoolObject>(len);
        _dataDict = new Dictionary<KeyType, PoolObjectData>(len);
        _poolDict = new Dictionary<KeyType, Stack<GameObject>>(len);
        _clonePoolDict = new Dictionary<GameObject, Stack<GameObject>>(len * PoolObjectData.INITIAL_COUNT);  

        // 2. Data 로부터 새로운  Pool 오브젝트 정보 생성
        foreach(var data in _poolObjectDataList)
        {
            RegisteriNTERNAL(data);
        }
    }


    /// <summary>
    ///  Pool 데이터로부터 새로운 Pool 오브젝트 정보 등록
    /// </summary>
    /// <param name="data"></param>
    void RegisteriNTERNAL(PoolObjectData data) // 정보 등록하기함수(Register)
    {
        // 중복 키는 등록 불가능
        if(_poolDict.ContainsKey(data.key)) // 이미 있는 키값이 포함되어 있다면
        {
            return; 
        }



        // https://rito15.github.io/posts/unity-object-pooling/ 
        // ObjectPoolManager 클래스 부터 





        // 1. 게임오브젝트 생성, PoolObject 컴포넌트 존재 확인
        GameObject go = Instantiate(data.prefab);
        go.name = data.prefab.name;
        if(!go.TryGetComponent(out PoolObject po))
        {
            po = go.AddComponent<PoolObject>();
            po.key = data.key;
        }

        go.SetActive(false);


        // 2. Pool Dictionary 에 풀 생성 + 풀에 미리 오브젝트를 만들어 담아놓기
        Stack<PoolObject> pool = new Stack<PoolObject>(data.maxObjectCount); // 미리 담아놓기(최대값)
        for (int i = 0; i < data.maxObjectCount; i++)
        {
            PoolObject clone = po.Clone();
            pool.Push(clone); // Stack 명령어(삽입)
            //스택 (Last In Last Out)
            // Push : 삽입
            // Pop : 가장 최근에 들어온 애를 1. 리턴,  2.삭제 한다.
            // Peek : 가장 최근에 들어온 애를 리턴한다.
            // 비어 있는 상태에서 Pop, Peek 하면 위험함.
        }

        // 3. 딕셔너리에 추가
        _originDict.Add(data.key, po);
        _dataDict.Add(data.key, data);
        _poolDict.Add(data.key, pool);
    }

    /// <summary>
    /// 풀에서 꺼내오기
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public PoolObject Spawn(KeyType key)
    {
        // 키가 존재하지 않는 경우  null 리턴
        if(!_poolDict.TryGetValue(key, out var pool))
        {
            return null;
        }
        PoolObject po;

        // 1. 풀에 재고가 있는 경우 : 꺼내오기
        if (pool.Count > 0)
        {
            po = pool.Pop(); // Pop() : 가장 최근에 들어온 애 리턴 후 삭제
        }
        // 2. 재고가 없는 경우 샘플로부터 복제
        else
        {
            po = _originDict[key].Clone();
        }

        po.Activate();

        return po;
    }

    /// <summary>
    /// 풀에 집어넣기
    /// </summary>
    /// <param name="po"></param>
    public void Despawn(PoolObject po) // Despanwn (PoolingObject a) = 풀에 집어넣는 함수
    {
        // 키가 존재하지 않는 경우 종료
         if(!_poolDict.TryGetValue(po.key, out var pool))
        {
            return;
        }

        KeyType key = po.key;

        // 1. 풀에 넣을 수 있는 경우 : 풀에 넣기
        if (pool.Count < _dataDict[key].maxObjectCount)
        {
            pool.Push(po);
            po.Deactivate();
        }
        // 2. 풀의 한도가 가득 찬 경우 : 파괴하기
        else
        {
            Destroy(po.gameObject);
        }
    }
}
