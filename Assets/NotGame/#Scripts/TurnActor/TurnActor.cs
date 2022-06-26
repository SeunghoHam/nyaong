using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TurnActor : MonoBehaviour
{
    public GameObject go_BaseActor;
    public GameObject go_TurnActor;
    public Vector3 pos_Start;
    public float newY;
    public bool isActorMove;

    //public float rotLength = 10f;
    // 우로회전 : rotY 값 +,
    // 좌로회전 : rotY 값 -,


    // 버튼 관련
    bool isOn = true;
    string onStr = "On";
    string offStr = "Off";
    const float LEFT = -75f;
    const float RIGHT = 75f;

    public Button btn;
    Text text_Btn;
    bool canbtnInput = true;

    private void Awake()
    {
        btn.onClick.AddListener(MoveBtn);
        text_Btn = btn.transform.GetChild(0).GetComponent<Text>();
        text_Btn.text = onStr;
        canbtnInput = true;
    }
    void MoveBtn()
    {
        if(canbtnInput)
        {
            canbtnInput = false;
            if (isOn) // 켜짐 → 꺼짐, 왼쪽 → 오른쪽
            {
                isOn = false;
                btn.gameObject.transform.DOLocalMoveX(RIGHT, 0.2f).SetEase(Ease.Linear).OnComplete(() => { canbtnInput = true; });
                text_Btn.text = offStr;
            }
            else // 꺼짐 → 켜짐, 오른쪽 → 왼쪽
            {
                isOn = true;
                btn.gameObject.transform.DOLocalMoveX(LEFT, 0.2f).SetEase(Ease.Linear).OnComplete(() => { canbtnInput = true; });
                text_Btn.text = onStr;
            }
        }
    }
    void Update()
    {
        if(isOn)
        {
            setInput();
            setSwipe();
        }
    }

    void setInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            pos_Start.x = Input.mousePosition.x;
            isActorMove = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isActorMove =false;
            setTurnActorLocalzero();
        }
    }
    void setTurnActorLocalzero() // 스와이프 하다가 손 때었을 때, TurnActor의 localRotaton을 BaseActor의 localRotaion 으로 전달.
                                 // 이유 : 다시 클릭했을 때, 찍은 점의 위치 - 스와이프 한 위치 = 0.  강제로 0으로 회전 변경되어버림.
    {
        go_BaseActor.transform.localEulerAngles += go_TurnActor.transform.localEulerAngles; 
        go_TurnActor.transform.localRotation = Quaternion.identity;
    }
    void setSwipe() // 스와이프중이라면 원래 찍은 점의 위치를 기준으로 X 축 이동시 Actor 회전
    {
        if (isActorMove)
        {
            newY = pos_Start.x - Input.mousePosition.x;
            go_TurnActor.transform.localRotation = Quaternion.Euler(new Vector3(0, newY, 0));
        }

    }
}
