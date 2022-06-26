using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemManager : MonoBehaviour
{
    public List<EnemData> enemList = new List<EnemData>();

    void DoEnemyTurns()
    {
        foreach(EnemData enemy in enemList)
        {
            enemy.DoTurn();
        }
    }

}
