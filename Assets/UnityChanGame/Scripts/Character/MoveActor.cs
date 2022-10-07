using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Cinemachine;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using UnityEngine.Rendering.UI;
using UnityEngine.UI;

namespace UC
{
    public class MoveActor : MonoBehaviour
    {
        private CharacterController controller;
        [SerializeField] private CinemachineVirtualCamera _virtualCam;

        [SerializeField] private UC_AnimController _animController;
        [SerializeField] private UC_Canvas _canvas;
        private UC_CameraController cameraController;
        private Rigidbody myRigid;
        private enum State
        {
            SIGHT_1, SIGHT_3, ATTACK,JUMP,
        }

        private State _state = State.SIGHT_1;

        [SerializeField] private UC_Mesh meshManager;
        
        
        // 움직임 관련
        private bool canSuperJump = false;
        
        
        void Awake()
        {
            controller = this.GetComponent<CharacterController>();
            //myRigid = this.GetComponent<Rigidbody>();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;

            RotateActor = this.transform.GetChild(0).gameObject;


            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");

            cameraController = Camera.main.GetComponent<UC_CameraController>();
            meshManager.MeshControl("First");
            cameraController.ChangeSight("First");
        }

        int count = 0 ;
        
        void Update()
        {
            switch (_state)
            {
                case State.SIGHT_1:
                    MouseRot_FirstSight();
                    Movement_FirstSight();
                    break;
                
                case State.SIGHT_3 :
                    Movement(); 
                    MouseRotator();
                    break;
            }

            if (Input.GetKeyDown(KeyCode.J))
            {
                _animController.Kick();
            }
            else if(Input.GetKeyDown(KeyCode.K))
            {
                _animController.Punch();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                if (count % 2 == 0)
                {
                    cameraController.ChangeCameraMode("Game");
                }
                else
                {
                    cameraController.ChangeCameraMode("Default");
                }

                count++;
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (count % 2 == 0)
                {
                    cameraController.ChangeSight("First");
                    meshManager.MeshControl("First");
                    _state = State.SIGHT_1;
                }
                else
                {
                    cameraController.ChangeSight("Third");
                    meshManager.MeshControl("Third");
                    _state = State.SIGHT_3;
                }

                count++;
            }

            if(Input.GetKeyDown(KeyCode.Space))
            {
                float currentY = this.gameObject.transform.position.y;
                //_animController.Jump();
                this.gameObject.transform.DOLocalJump
                    (Vector3.up, 
                        0.5f,
                        1,
                        1f).SetRelative(true);
                    /*
                    .OnComplete(() => // 점프 끝. 땅끝으로 이동
                        {
                            this.gameObject.transform.DOLocalMoveY(currentY, 0.2f).SetEase(Ease.Linear);
                        }
                        );*/
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                _canvas.SKILL_DRIFT_DOWN();
                canSuperJump = true;
                // 슈퍼점프 가능
            }
            else if(Input.GetKeyUp(KeyCode.LeftShift))
            {
                StartCoroutine(CRT_Skill_Drift());
                // Up 하고 0.5초 이내까지는 슈퍼점프 되도록
            }
        }

        IEnumerator CRT_Skill_Drift()
        {
            float _jumpdelay = 0.5f;
            float _cooltime = 1.5f;
            _canvas.SKILL_DRIFT_UP(_cooltime);
            yield return new WaitForSeconds(_jumpdelay);
            canSuperJump = false;   
            yield return new WaitForSeconds(_cooltime - _jumpdelay);
            _canvas.COOL_DOWN(0);
            
        }
        float horizontal; // = Input.GetAxisRaw("Horizontal");
        float vertical; // = Input.GetAxisRaw("Vertical");
        private Rigidbody rb;
        private float speed = 4f;

        bool MoveStart = false;
        void Movement()
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");

            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized; // 이동 각도 설정
            if (direction.magnitude >= 0.1f)
            {
                if (!MoveStart)
                {
                    MoveStart = true;
                    _animController.TriggerMove();
                }
                controller.Move(RotateActor.transform.localRotation * direction * speed * Time.deltaTime);
                
                _animController.TestDirection(horizontal);
            }
            else
            {
                if (MoveStart)
                {
                    MoveStart = false;
                    _animController.Walk(false);
                    _animController.TestIdle();
                }
            }
        }
        void Movement_FirstSight()
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");

            
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                if (!MoveStart)
                {
                    // 1인칭에서는 애니메이션 안보임
                    MoveStart = true;
                }
                controller.Move(RotateActor.transform.localRotation * direction *speed*  Time.deltaTime);
            }
            else
            {
                if (MoveStart)
                {
                    MoveStart = false; 
                }
            }


        }

        [SerializeField] private CinemachineVirtualCamera cam_1stSight;
        [SerializeField] private Transform tf_1stCamTf;
        void MouseRot_FirstSight()
        {
            mouseX = Input.GetAxisRaw("Mouse X") * _sensitivity;
            mouseY = Input.GetAxisRaw("Mouse Y") * _sensitivity;

            cam_1stSight.transform.position = tf_1stCamTf.position;

            cam_1stSight.transform.eulerAngles +=
                new Vector3(
                    -mouseY,
                    mouseX,
                    0);
            
            // RotateActor(캐릭터 회전을 위해서) 도 카메라 회전에 맞춰서 회전
            RotateActor.transform.eulerAngles =
                new Vector3(
                    0, cam_1stSight.transform.eulerAngles.y, 0);
        }

        float LIMIT_mouseY(float _value)
        {
            return Mathf.Clamp(_value, -30f, 30f);
        }
        IEnumerator CRT_ChangeDirecitonValue(float _startValue, float _endValue, float _duration)
        {
            float elapsed = 0.0f;
            while (elapsed < _duration)
            {
                Debug.Log("<color=red> StartValue : " + _startValue + "</color>");
                Debug.Log("<color=yellow> EndValue : " + _endValue + "</color>");
                horizontal = Mathf.Lerp(_startValue, _endValue, elapsed / _duration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            horizontal = _endValue;
        }

        private float mouseX;
        private float mouseY;
        private float _sensitivity = 0.8f;

        private GameObject RotateActor;

        void MouseRotator()
        {
            mouseX = Input.GetAxis("Mouse X") * _sensitivity;
            mouseY = Input.GetAxis("Mouse Y") * _sensitivity;

            RotateActor.transform.eulerAngles +=
                new Vector3(
                    //-Mathf.Clamp(mouseY, -10f, 10f),
                    -0,
                    mouseX,
                    0);
        }


        void Raycast_1stSight()
        {
            
        }
    }
}