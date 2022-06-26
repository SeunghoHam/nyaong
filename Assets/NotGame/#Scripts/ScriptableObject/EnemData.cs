using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemydata1", menuName ="MyGame/EnemyData")]
public class EnemData : EnemBase
{
    public string name;
    public string description;
    public GameObject model;
    public int health;
    public float speed = 2f;
    public float detecRange = 10f;
    public int damage = 10;

    public override void DoTurn()
    {
        //throw new System.NotImplementedException();
    }
}
