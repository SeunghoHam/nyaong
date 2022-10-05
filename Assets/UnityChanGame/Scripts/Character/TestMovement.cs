using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

namespace UC
{
    public class TestMovement : MonoBehaviour
    {
        private CharacterController controller;
        [SerializeField] private CinemachineVirtualCamera _virtualCam;

        [SerializeField] private UC_AnimController _animController;


        private UC_CameraController cameraController;

        void Awake()
        {
            controller = this.GetComponent<CharacterController>();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;

            RotateActor = this.transform.GetChild(0).gameObject;


            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");

            cameraController = Camera.main.GetComponent<UC_CameraController>();
        }

        int count = 0 ;
        void Update()
        {
            Movement();
            MouseRotator();
            if (Input.GetKeyDown(KeyCode.J))
            {
                _animController.Kick();
            }
            else if(Input.GetKeyDown(KeyCode.K))
            {
                _animController.Punch();
            }

            if (Input.GetKeyDown(KeyCode.Space))
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
        }

        // 조건문
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

            /*
            Vector3 direction2 = 
                new Vector3(
                    RotateActor.transform.localEulerAngles.y,
                    0f, 
                    RotateActor.transform.localEulerAngles.y).normalized;
                    */
            if (direction.magnitude >= 0.1f)
            {
                if (!MoveStart)
                {
                    MoveStart = true;
                    //_animController.Walk(true);
                    _animController.TriggerMove();
                }

                //controller.Move(RotateActor.transform.forward * speed * Time.deltaTime);
                controller.Move(RotateActor.transform.localRotation * direction * speed * Time.deltaTime);

                //Debug.Log("입력있다");
                //_animController.WalkSetting(horizontal, vertical);
                _animController.TestDirection(horizontal);
                //StartCoroutine(CRT_ChangeDirecitonValue(horizontal, horizontal, 1f));
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

        // 마우스 Y좌표에 해당하는것들 따로 작업
        void SightChange()
        {
            //_virtualCam.
        }
    }
}