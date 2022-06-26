using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Monster : MonoBehaviour
{
    public MonsterData data;

    int turnNum;


    private void OnEnable()
    {
        //MonsterMove();
    }


    public void Destory()
    {
        MonsterPool.ReturnObject(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("TurnPosition"))
        {
            //TurnActor();
        }
    }

    float delay = 2f;
    private void Update()
    {
        this.gameObject.transform.Translate(Vector3.left * Time.deltaTime * delay);

        TurnActor();
    }

    void TurnActor()
    {
        Debug.Log("엑터 턴!");
        // 1번 회전 : x 0 -> -60
        // 2번 회전 : z 0 -> -60 
        // 3번 회전 : x -60 -> 0
        // 4번 회전 : z -60 > 0
        switch (turnNum)
        {
            case 0:
                if (this.gameObject.transform.localPosition.x <= -60)
                {
                    Turn(1);
                }
                break;

            case 1:
                if (this.gameObject.transform.localPosition.z <= -60)
                {
                    Turn(2);
                }
                break;
            default:
                break;
        }
    }
    void Turn(int _num)
    {
        this.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, -90 + this.transform.localRotation.y, 0));
        turnNum = _num;
    }
}


    

