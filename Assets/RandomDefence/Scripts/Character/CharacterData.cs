using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Character", menuName = "RandomDefence/Character")]
public class CharacterData : ScriptableObject
{
    /// <summary> 이름 </summary>
    public string _name;

    /// <summary> 코드값 </summary>
    public string _code;

    /// <summary> 이동속도 </summary>
    public float _speed;
    
    /// <summary> 입히는 데미지 </summary>
    public int _damage;

    /// <summary> 공격 속도 </summary>
    public float _attackSpeed;
}
