using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using KeyType = System.String;



#if UNITY_EDITOR
//#define DEBUG_ON
//#define TEST_ON
#endif
/// <summary>
/// 오브젝트 풀 관리 싱글톤
/// </summary>
/// 
[DisallowMultipleComponent] // 클래스 중복존재 방지
public class EnemyPoolingManager : MonoBehaviour
{
    [System.Diagnostics.Conditional("TEST_ON")]
    private void TestModeOnly(Action action)
    {
        action();
    }


    // 인스펙터에서 오브젝트 풀링 대상 정보 추가
    [SerializeField]
    private List<PoolObjectData> _poolObjectDataList = new List<PoolObjectData>(4);

    // 복제될 오브젝트의 원본 딕셔너리
    private Dictionary<KeyType, GameObject> _originDict; // Key - 복제용 오브젝트 원본

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
        _originDict = new Dictionary<KeyType, GameObject>(len);
        _dataDict = new Dictionary<KeyType, PoolObjectData>(len);
        _poolDict = new Dictionary<KeyType, Stack<GameObject>>(len);
        _clonePoolDict = new Dictionary<GameObject, Stack<GameObject>>(len * PoolObjectData.INITIAL_COUNT);

        // 2. Data 로부터 새로운  Pool 오브젝트 정보 생성
        foreach (var data in _poolObjectDataList)
        {
            RegisterInternal(data);
        }
    }


    /// <summary>
    ///  Pool 데이터로부터 새로운 Pool 오브젝트 정보 등록
    /// </summary>
    /// <param name="data"></param>
    /// 풀 한도 이상의 오브젝트 점진적 파괴


    [SerializeField]
    private float _poolCleaningInterval = 0.1f; // 풀 한도 초과 오브젝트 제거 간격
    void RegisterInternal(PoolObjectData data) // 정보 등록하기함수(Register)
    {
        // 중복 키는 등록 불가능
        if (_poolDict.ContainsKey(data.key)) // 이미 있는 키값이 포함되어 있다면
        {
            return;
        }

        // 1. 게임오브젝트 생성, PoolObject 컴포넌트 존재 확인
        GameObject go = Instantiate(data.prefab);
        go.name = data.prefab.name;
        go.SetActive(false);

        // 2. Pool Dictionary 에 풀 생성 + 풀에 미리 오브젝트들 만들어 담아놓기
        Stack<GameObject> pool = new Stack<GameObject>(data.maxObjectCount);

        for (int i = 0; i < data.initialObjectCount; i++)
        {
            GameObject clone = Instantiate(data.prefab);
            clone.SetActive(false);
            pool.Push(clone); // Stack 명령어(삽입)
            //스택 (Last In Last Out)
            // Push : 삽입
            // Pop : 가장 최근에 들어온 애를 1. 리턴,  2.삭제 한다.
            // Peek : 가장 최근에 들어온 애를 리턴한다.
            // 비어 있는 상태에서 Pop, Peek 하면 위험함.

            _clonePoolDict.Add(clone, pool);
        }

        // 3. 딕셔너리에 추가
        _originDict.Add(data.key, go);
        _dataDict.Add(data.key, data);
        _poolDict.Add(data.key, pool);
    }

    /// <summary> 샘플 오브젝트 복제하기 </summary>
    private GameObject CloneFromSample(KeyType key)
    {
        if (!_originDict.TryGetValue(key, out GameObject originGo)) return null;

        return Instantiate(originGo);
    }

    /// <summary> 풀에서 꺼내오기 </summary>
    public GameObject Spawn(KeyType key)
    {
        // 키가 존재하지 않는 경우  null 리턴
        if(!_poolDict.TryGetValue(key, out var pool))
        {
            return null;
        }
        GameObject go;

        // 1. 풀에 재고가 있는 경우 : 꺼내오기
        if (pool.Count > 0)
        {
            go = pool.Pop(); // Pop() : 가장 최근에 들어온 애 리턴 후 삭제
        }
        // 2. 재고가 없는 경우 샘플로부터 복제
        else
        {
            go = _originDict[key]; // orignDict 가 <GameObject> 를 받음으로서 그냥 [Key]를 할당시켜도 된다.
            _clonePoolDict.Add(go, pool); // Clone - Stack 캐싱
        }

        go.SetActive(true);
        go.transform.SetParent(null);

        return go;
    }

    /// <summary> 풀에 집어넣기 </summary>
    public void Despawn(GameObject go) // Despanwn (GameObject go) = 풀에 집어넣는 함수
    {
        // 캐싱된 게임오브젝트가 아닌 경우 파괴
         if(!_clonePoolDict.TryGetValue(go, out var pool))
        {
            Destroy(go);
            return;
        }

        // 집어넣기
        go.SetActive(false);
        pool.Push(go);
    }





    // ****************** 추가적 기능 *******************


    /// <summary> 키를 등록하고 새로운 풀 생성 </summary>
    /// <param name="_key"></param>
    /// <param name="_prefab"></param>
    /// <param name="initialCount"></param>
    /// <param name="maxCount"></param>
    /// 플레이 도중에도 원하는 타이밍에 동적으로 새로운 오브젝트를 풀에 등록할 수 있도록 등록할 수 있는 메소드
    public void Register(KeyType _key, GameObject _prefab, 
        int initialCount = PoolObjectData.INITIAL_COUNT, 
        int maxCount = PoolObjectData.MAX_COUNT)
    {
        // 중복 키는 등록 불가능
        if(_poolDict.ContainsKey(_key))
        {
            Debug.Log( $"{_key}키가 이미 존재함");
        }

        if (initialCount < 0) initialCount = 0;
        if (maxCount < 10) maxCount = 10;

        PoolObjectData data = new PoolObjectData
        {
            key = _key,
            prefab = _prefab,
            initialObjectCount = initialCount,
            maxObjectCount = maxCount
        };

        _poolObjectDataList.Add(data);

        RegisterInternal(data);
    }



   private IEnumerator PoolCleanerRoutine(KeyType key)
    {
        if (!_poolDict.TryGetValue(key, out var pool)) yield break;
        if (!_dataDict.TryGetValue(key, out var data)) yield break;
        WaitForSeconds wfs = new WaitForSeconds(_poolCleaningInterval);

        while (true)
        {
            if(pool.Count > data.maxObjectCount)
            {
                GameObject clone = pool.Pop(); // 풀에서 꺼내기
                
            }
        }
    }
} 
