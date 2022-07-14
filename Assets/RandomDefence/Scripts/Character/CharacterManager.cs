using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Mathematics;
using UnityChan;
using UnityEngine;
using Random = UnityEngine.Random;

//using Random = System.Random;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] public Transform Map;
    [SerializeField] private GameObject prefab_Character1;
    [SerializeField] private GameObject prefab_Character2;


    private int _SpawnObjectRange;
    [SerializeField] private int[] _probability; // probability

    private int _level;

    private int amount_Normal = 2;
    private int amount_Rare = 4;
    private int amount_Epic = 3;
    private int amount_Legend = 2;

    private float posY = 0.85f;
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
        //return _probability;
    }

    public void RandomCreate()
    {
        // 레벨에 따른 등급 렌덤 생성 과정이 필요함
        int ran = Random.Range(0, 10); // 0 ~ 9
        
        
        RandomCreate_Normal();
    }

    void RandomCreate_Normal()
    {
        int ran = Random.Range(0, amount_Normal); // 0, 1
        switch (ran)
        {
            case 0:
                Instantiate(prefab_Character1, new Vector3(0, posY, 0), quaternion.identity, Map);
                break;
            case 1:
                Instantiate(prefab_Character2, new Vector3(0, posY, 0), quaternion.identity, Map);
                break;
                    
        }
    }

    void RandomCreate_Rare()
    {
        int ran = Random.Range(0, amount_Rare);
    }

    void RandomCreate_Epic()
    {
        int ran = Random.Range(0, amount_Epic);
    }

    void RandomCreate_Legend()
    {
        int ran = Random.Range(0, amount_Legend);
    }

    float GetRandomPosition()
    {
        //float ran = Random.Rang
        return 0f;
    }
}