using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

//using Random = System.Random;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] public Transform Map;
    [SerializeField] private GameObject prefab_Character1;
    [SerializeField] private GameObject prefab_Character2;


    private int _SpawnObjectRange;
    private int[] _probability; // probability

    private int _level;
    private void Awake()
    {
        _probability = new int[4];
        setProbability(1); 
    }

    void setProbability(int _num)
    {
        // _probability [0] = normal / [1] = rare / [2] = epic / [3] = legend
        switch (_num) // _num 은 레벨
        {
            case 1:
                getParameter(10, 0, 0, 0);
                break;
            case 2:
                getParameter(8, 2, 0, 0);
                break;
            case 3:
                getParameter(6, 3, 1, 0);
                break;
            case 4:
                getParameter(4, 3, 2, 1);
                break;
            case 5:
                getParameter(2, 3, 3, 2);
                break;
        }
    }

    void getParameter(int _num1, int _num2, int _num3, int _num4)
    {
        _probability[0] = _num1;
        _probability[1] = _num2;
        _probability[2] = _num3;
        _probability[3] = _num4;
    }

    public void RandomCreate()
    {
        Instantiate(prefab_Character1,new Vector3(0,0.85f,0), quaternion.identity, Map);
    }

    void RandomCreate_Normal()
    {
    }

    void RandomCreate_Rare()
    {
    }

    void RandomCreate_Epic()
    {
    }

    void RandomCreate_Legend()
    {
    }

    float GetRandomPosition()
    {
      //float ran = Random.Rang
      return 0f;
    }
}