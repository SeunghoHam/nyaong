using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Monster : MonoBehaviour
{
    public MonsterData data;
    
    int turnNum;
    private int TurnCount;
    private const int MAX_TURN_COUNT = 1;
    private void OnEnable()
    {
        //MonsterMove();
    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("TurnPosition"))
        {
            //TurnActor();
        }
    }

    float delay = 5f;
    private void Update()
    {
        // 몬스터 공격 테스트를 위한 주석처리
        /*
        this.gameObject.transform.Translate(Vector3.left * Time.deltaTime * delay);
        TurnActor();*/
    }

    void IsReset()
    {
        TurnCount = 0;
        turnNum = 0;
    }

    private float refValue = 60f;
    void TurnActor()
    {
        Debug.Log("엑터 동작 중!");
        // 1번 회전 : x 0 -> -60
        // 2번 회전 : z 0 -> -60 
        // 3번 회전 : x -60 -> 0
        // 4번 회전 : z -60 > 0
        switch (turnNum)
        {
            case 0:
                if (this.gameObject.transform.localPosition.x <= -refValue)
                {
                    Turn(1);
                    Debug.Log("<color=magenta> 1번째 회전 함 </color>");
                }
                break;

            case 1:
                if (this.gameObject.transform.localPosition.z <= -refValue)
                {
                    Turn(2);
                    Debug.Log("<color=magenta> 2번째 회전 함 </color>");

                }
                break;
            case 2:
                if (this.gameObject.transform.localPosition.x >= 0)
                {
                    Turn(3); 
                    Debug.Log("<color=magenta> 3번째 회전 함 </color>");
                }
                break;
            
            // 시작점으로 복귀
            case 3:
                if (this.gameObject.transform.localPosition.z >= 0)
                {
                    Turn(4);
                    Debug.Log("<color=magenta> 4번째 회전 함 </color>");
                }
                break;
            default:
                break;
        }
    }
    /// <summary> </summary>
    /// <param name="_num"></param>
    void Turn(int _num)
    {
        if (_num == 1)
        {
            this.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, -90 + this.transform.localRotation.y, 0));
            turnNum = _num;
        }
        else if (_num == 2)
        {
            this.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, -180 + this.transform.localRotation.y, 0));
            turnNum = _num;
        }

        else if (_num == 3)
        {
            this.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, -270 + this.transform.localRotation.y, 0));
            turnNum = _num;
        }
        else if (_num == 4)
        {
            this.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0 + this.transform.localRotation.y, 0));
            turnNum = 0; // turnNum 초기화가 필요하므로
            // 총 회전 수 증가해야함
            CheckTurnCount();
        }
        //turnNum = _num;
    }

    void CheckTurnCount()
    {
        
        if (TurnCount == MAX_TURN_COUNT)
        {
            //turnNum = 0;
            IsDead();
            // 객체 사라지고 체력 깍이게
        }
        else
        {
            TurnCount++;
        }
    }

    void IsDead()
    {
        Debug.Log("적이 두바퀴를 돌고 들어왔음");
        GameManager.Instance.PlayerAttacked(data._damage);
        MonsterPool.ReturnObject(this);
    }
}