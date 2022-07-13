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


    public Camera camera;
    private RaycastHit hitData;
    private Vector3 hitPosition;
    private float hitDistance;
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

        camera = Camera.main;
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
    
        if (Input.GetMouseButtonDown(0))
        {
            MouseInput_Movement();
        }
    }
    
    void MouseInput_Movement()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        
        hitPosition = hitData.point;
        hitDistance = hitData.distance;
        if (Physics.Raycast(ray, out hitData))
        {
            Debug.Log(hitData.transform.name);
        }
    }
}
