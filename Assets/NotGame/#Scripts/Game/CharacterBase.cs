using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterBase : MonoBehaviour
{
    [SerializeField] GameObject go_Character;
    Transform lookPoint;
    [SerializeField] Camera Cam_Game;

    [SerializeField] KeyCode moveFront; 
    [SerializeField] KeyCode moveBack; 
    [SerializeField] KeyCode moveRight; 
    [SerializeField] KeyCode moveLeft; 

    bool camstate_NORMAL;

    public Vector3 springArm = new Vector3(0,1.8f, -2f);
    private void Awake()
    {
        go_Character = this.gameObject;
        lookPoint = go_Character.transform.GetChild(1).transform;
        Cam_Game = this.gameObject.transform.GetChild(0).gameObject.GetComponent<Camera>();
        defaultSetting();
    }

    void defaultSetting()
    {
        moveFront = KeyCode.W;
        moveBack = KeyCode.S;
        moveRight = KeyCode.D;
        moveLeft = KeyCode.A;
        // 환경설정에서 키 바꾸면 이런식으로 바꾸게 되겠지?
    }
    // Start is called before the first frame update
    void Start()
    {
        camstate_NORMAL = false;
    }

    // Update is called once per frame
    void Update()
    {
        MoveInputSystem();

        if (camstate_NORMAL)
        {
            Cam_Game.transform.localPosition = springArm;
            Cam_Game.transform.LookAt(lookPoint);
        }
        else
        {
            Cam_Game.transform.DOLocalMove(springArm, 0.2f);
            Cam_Game.transform.DOLookAt(lookPoint.position, 0.2f, AxisConstraint.None);
        }
    }



    void MoveInputSystem()
    {
        float moveSpeed = 5f;
        float moveLength = Time.deltaTime * moveSpeed;
        if(Input.GetKey(moveFront))
        {
            go_Character.transform.position += Vector3.forward * moveLength;
        }
        else if (Input.GetKey(moveBack))
        {
            go_Character.transform.position += Vector3.back * moveLength;

        }

        if (Input.GetKey(moveRight))
        {
            go_Character.transform.position += Vector3.right * moveLength;

        }
        else if(Input.GetKey(moveLeft))
        {
            go_Character.transform.position += Vector3.left * moveLength;

        }


    }
}
