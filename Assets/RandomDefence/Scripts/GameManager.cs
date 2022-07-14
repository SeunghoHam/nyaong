using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
    private static GameManager instance;
    public static GameManager Instance
    { 
        get
        {
            if(instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    // UI
    public UIManager _ui;
    

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            
        }
        else
        {
            Destroy(this.gameObject);
        }


    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            sw.Start();

            MonsterPool.GetMonster();
            sw.Stop();
            Debug.Log("<color=aqua> 오브젝트 생성에 걸린 시간 </color>: " + "<color=red>" + sw.ElapsedMilliseconds.ToString() + "</color> ms");
        }
    

    }
    




    /// <summary> _damage 만큼의 피해를 플레이어에게 입힌다. </summary>
    /// <param name="_damage"></param>
    public void PlayerAttacked(int _damage)
    {
        _ui.ChangeHp(_damage);
    }
    
}
