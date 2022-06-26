using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Monster", menuName = "RandomDefence/EnemyData")]
public class MonsterData : ScriptableObject
{
    /// <summary> 이름 </summary>
    public string _name;

    /// <summary> 간단한 설명 </summary>
    public string _desc;

    /// <summary> 이동속도 </summary>
    public float _speed;

    /// <summary> 체력 </summary>
    public int _hp;

    /// <summary> 처치 실패 시 받게 될 데미지 </summary>
    public int _damage;

    /// <summary> 처치 시 지급되는 금액 </summary>
    public int _gold;

    /// <summary> 할당되는 프리팹 </summary>
    public GameObject _model;

    /// <summary> 이동 중 목적지 </summary>
    //public Vector3[] _destination;
}
