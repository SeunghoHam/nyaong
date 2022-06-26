using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
    public MonsterPool monsterpool;
    
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
        if(Input.GetKeyDown(KeyCode.Q)) // MontserPool 직접 할당 -> Get
        {
            sw.Start();
            //Debug.Log("<color=red>딜레이 체크 시작 </color>");
            //monsterpool.GetComponent<MonsterPool>().GetComponent)_);
            
            sw.Stop();
            Debug.Log("<color=red>" + sw.ElapsedMilliseconds.ToString() + "</color> ms");
        }

        if(Input.GetKeyDown(KeyCode.W))
        {
            sw.Start();

            MonsterPool.GetMonster();
            sw.Stop();
            Debug.Log("<color=aqua> 오브젝트 생성에 걸린 시간 </color>: " + "<color=red>" + sw.ElapsedMilliseconds.ToString() + "</color> ms");
        }
    }
}
