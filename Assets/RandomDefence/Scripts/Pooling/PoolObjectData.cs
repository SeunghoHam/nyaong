using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Keytype = System.String;


[System.Serializable]
public class PoolObjectData
{
    public const int INITIAL_COUNT = 10;
    public const int MAX_COUNT = 50;

    public Keytype key;
    public GameObject prefab;
    /// <summary>
    ///  initialObjectCount : 오브젝트 초기 생성 개수
    /// </summary>
    public int initialObjectCount = INITIAL_COUNT;

    /// <summary>
    /// maxObjectCount : 큐 내에 보관되는 최대 오브젝트 개수
    /// </summary>
    public int maxObjectCount = MAX_COUNT;
}
