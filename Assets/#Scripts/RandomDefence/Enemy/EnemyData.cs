using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "RandomDefence/EnemyData")]
public class EnemyData : ScriptableObject
{
    public string _name;  
    public string _desc;
    public float _speed;
    public int _hp;
    public int _damage;
    public int _gold;
    public GameObject _model;
}
