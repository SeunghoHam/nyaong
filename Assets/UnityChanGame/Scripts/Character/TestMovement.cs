using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.UI;

public class TestMovement : MonoBehaviour
{
    private CharacterController controller;
 //   [SerializeField] private CinemachineVirtualCamera _camera;
    void Awake()
    {
        controller = this.GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
            
        rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;

            RotateActor = this.transform.GetChild(0).gameObject;
    }
    void Update()
    {
        SetDirectoin();
        Movement();
        MouseRotator();
        
    }

    void SetDirectoin()
    {
        
    }
    private Rigidbody rb;
    private float speed = 4f;
    void Movement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized; // 이동 각도 설정

        Vector3 direction2 = 
            new Vector3(
                RotateActor.transform.localEulerAngles.y,
                0f, 
                RotateActor.transform.localEulerAngles.y).normalized;
        if (direction.magnitude >= 0.1f)
        {
            Debug.Log("direction2" + direction2);
            //controller.Move(direction2 * speed * Time.deltaTime);
            controller.Move(RotateActor.transform.forward * speed * Time.deltaTime);
            
            // 입력이 있을 경우
            //this.transform.Translate();
            //Vector3 move = transform.right * horizontal + transform.forward * vertical;
            
            // RotateActor의 방향을 받아와서 그 방향으로 이동할 수 있도록 해야함
            Debug.Log("입력있다");
        }

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
            0,
            mouseX, 
            0);
    }
}
