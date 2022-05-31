/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
public class InputMovement : MonoBehaviour
{
    public bool useCharacterForward = false;
    public bool lockToCameraForward = false;
    public float turnSpeed = 10f;

    float turnSpeedMultiplier;
    float speed = 0f;
    float direction = 0f;

    Animator anim;
    Vector3 targetDirection;
    Vector2 input;
    Quaternion freeRotation;
    Camera Cam_Game;
    float velocity;
    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();
        Cam_Game = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        Debug.Log("동작중?");
        //input.x = Input.GetAxis("Horizontal");
        //input.y = Input.GetAxis("Vertical");
        if (Input.GetKey(KeyCode.W))
            input.y = 1;
        else if (Input.GetKey(KeyCode.S))
            input.y = -1;

        if (Input.GetKey(KeyCode.D))
            input.x = 1;
        else if (Input.GetKey(KeyCode.A))
            input.x = -1;



        if (useCharacterForward)
            speed = Mathf.Abs(input.x) + input.y;
        else 
            speed = Mathf.Abs(input.x) + Mathf.Abs(input.y);

        speed = Mathf.Clamp(speed, 0f, 1f);

        if (input.y < 0f && useCharacterForward)
            direction = input.y;
        else
            direction = 0f;

        UpdateTargetDirection();

        if (input != Vector2.zero && targetDirection.magnitude > 0.1f)
        {
            Vector3 lookDirection = targetDirection.normalized;
            freeRotation = Quaternion.LookRotation(lookDirection, transform.up);
            var diferenceRotatoin = freeRotation.eulerAngles.y - transform.eulerAngles.y;
            var eulerY = transform.eulerAngles.y;

            if (diferenceRotatoin < 0 || diferenceRotatoin > 0) eulerY = freeRotation.eulerAngles.y;
            var euler = new Vector3(0, eulerY, 0);

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(euler), turnSpeed * turnSpeed * Time.deltaTime);
        }
    }

    public virtual void UpdateTargetDirection()
    {
        if(!useCharacterForward)
        {
            turnSpeedMultiplier = 1f;

        }
        else // useCharacterForward == true
        {
            turnSpeedMultiplier = -0.2f;
            var forward = transform.TransformDirection(Vector3.forward);
            forward.y = 0;

            var right = transform.TransformDirection(Vector3.right);
            targetDirection = input.x * right + Mathf.Abs(input.y) * forward;
        }
    }

}
*/
using UnityEngine;

namespace Cinemachine.Examples
{

    [AddComponentMenu("")] // Don't display in add component menu
    public class InputMovement : MonoBehaviour
    {
        public bool useCharacterForward = false;
        public bool lockToCameraForward = false;
        public float turnSpeed = 10f;
        public KeyCode sprintJoystick = KeyCode.JoystickButton2;
        public KeyCode sprintKeyboard = KeyCode.Space;

        private float turnSpeedMultiplier;
        private float speed = 0f;
        private float direction = 0f;
        //private bool isSprinting = false;
        private Animator anim;
        private Vector3 targetDirection;
        private Vector2 input;
        private Quaternion freeRotation;
        private Camera mainCamera;
        private Vector3 velocity;


        // UnityChan
        public float animSpeed = 1.5f;
        AnimatorStateInfo currentBaseState;

        public bool isMove = false;

        // 애니이션 각 스테이트
        static int idleState = Animator.StringToHash("Base Layer.Idle");
        static int loclState = Animator.StringToHash("Base Layer.Locomotion");
        static int jumpState = Animator.StringToHash("Base Layer.Jump");
        static int restState = Animator.StringToHash("Base Layer.Rest");

        // Use this for initialization
        void Start()
        {
            anim = GetComponent<Animator>();
            mainCamera = Camera.main;
        }
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if(!anim.IsInTransition(0))
                {
                    anim.SetTrigger("triggerJump");
                }
            }


        }
        // Update is called once per frame
        void FixedUpdate()
        {
            input.x = Input.GetAxis("Horizontal");
            input.y = Input.GetAxis("Vertical");


            // 유니티짱 이동 방식 적용 & Cinemaachine캐릭터의 회전 방식 적용
            float h = input.x;
            float v = input.y;
            float moveSpeed = 2f;
            anim.speed = animSpeed;
            currentBaseState = anim.GetCurrentAnimatorStateInfo(0);
            velocity = new Vector3(h, 0, v);
            //velocity = transform.TransformDirection(velocity);
            this.transform.position += velocity * Time.deltaTime * moveSpeed;
            anim.SetFloat("Speed", v);
            anim.SetFloat("Direction", h);


            if ((v != 0 || h != 0) && !isMove)// && moveState == 1) // 움직임 있음
            {
                Debug.Log("무브");
                anim.SetBool("isMove", true);
                isMove = true;
            }
            else if(isMove && (v ==0 && h ==0)) // 움직임 없음
            {
                Debug.Log("정지");
                anim.SetBool("isMove", false);
                isMove = false;
            }












            //anim.speed=  
            // set speed to both vertical and horizontal inputs
            if (useCharacterForward)
                speed = Mathf.Abs(input.x) + input.y;
            else
                speed = Mathf.Abs(input.x) + Mathf.Abs(input.y);

            //speed = Mathf.Clamp(speed, 0f, 1f);
            //speed = Mathf.SmoothDamp(anim.GetFloat("Speed"), speed, ref velocity, 0.1f);
            //anim.SetFloat("Speed", speed);

            if (input.y < 0f && useCharacterForward)
                direction = input.y;
            else
                direction = 0f;

            //anim.SetFloat("Direction", direction);

            // set sprinting
            //isSprinting = ((Input.GetKey(sprintJoystick) || Input.GetKey(sprintKeyboard)) && input != Vector2.zero && direction >= 0f);
            //anim.SetBool("isSprinting", isSprinting);

            // Update target direction relative to the camera view (or not if the Keep Direction option is checked)
            UpdateTargetDirection();
            if (input != Vector2.zero && targetDirection.magnitude > 0.1f)
            {
                Vector3 lookDirection = targetDirection.normalized;
                freeRotation = Quaternion.LookRotation(lookDirection, transform.up);
                var diferenceRotation = freeRotation.eulerAngles.y - transform.eulerAngles.y;
                var eulerY = transform.eulerAngles.y;

                if (diferenceRotation < 0 || diferenceRotation > 0) eulerY = freeRotation.eulerAngles.y;
                var euler = new Vector3(0, eulerY, 0);

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(euler), turnSpeed * turnSpeedMultiplier * Time.deltaTime);
            }
        }

        public virtual void UpdateTargetDirection()
        {
            if (!useCharacterForward)
            {
                turnSpeedMultiplier = 1f;
                var forward = mainCamera.transform.TransformDirection(Vector3.forward);
                forward.y = 0;

                //get the right-facing direction of the referenceTransform
                var right = mainCamera.transform.TransformDirection(Vector3.right);

                // determine the direction the player will face based on input and the referenceTransform's right and forward directions
                targetDirection = input.x * right + input.y * forward;
            }
            else
            {
                turnSpeedMultiplier = 0.2f;
                var forward = transform.TransformDirection(Vector3.forward);
                forward.y = 0;

                //get the right-facing direction of the referenceTransform
                var right = transform.TransformDirection(Vector3.right);
                targetDirection = input.x * right + Mathf.Abs(input.y) * forward;
            }
        }
    }

}
