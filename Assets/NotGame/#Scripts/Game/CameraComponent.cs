using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraComponent : MonoBehaviour
{
    // 마우스 이동으로 시야 범위 회전

    public Transform objectToFollow;
    public float followSpeed = 10f;
    public float sensitivity = 500;
    public float clampAngle = 70f;

    private float rotX;
    private float rotY;

    public Transform realCamera;
    public Camera Cam_Game;
    public Vector3 dirNormalized; // 방향만 나타낼거
    public Vector3 finalDir; // 최종적으로 정해질 벡터값

    public float minDistance; // 최소거리
    public float maxDistance; // 최대거리
    public float finalDistance;
    public float smoothness = 10f;



    // 마우스 휠로 카메라 시야각변경
    public GameObject characterHead;
    float wheelSpeed = 10.0f;
    public Transform cameraTarget;
    Vector3 worldDefaultForward; // 바라보게 되는 방향, 현제 그냥 정면으로 되어있음. 캐릭터가 바라보는 방향으로 변경해줘야함
    private void Awake()
    {
        //t_Cam = this.gameObject.transform;
        Cam_Game = realCamera.GetComponent<Camera>();

        //worldDefaultForward = transform.forward; // 정면 보게 하는거 이게 문제인가?
    }
    private void Start()
    {
        rotX = transform.localRotation.eulerAngles.x;
        rotY = transform.localRotation.eulerAngles.y;

        dirNormalized = realCamera.localPosition.normalized; // 방향만 적용
        finalDistance = realCamera.localPosition.magnitude; // 크기만 적용

       
    }
    private void Update()
    {
        CamMoveSystem_update();
    }
    private void LateUpdate()
    {
        CamMoveSystem_lateupdate();
    }
    void CamMoveSystem_update()
    {
        rotX += -(Input.GetAxis("Mouse Y")) * sensitivity * Time.deltaTime;
        rotY += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        Quaternion rot = Quaternion.Euler(rotX, rotY, 0);
        transform.rotation = rot;
    }
    void CamMoveSystem_lateupdate()
    {
        //transform.position = Vector3.MoveTowards(transform.position, objectToFollow.position, followSpeed * Time.deltaTime);

        finalDir = transform.TransformPoint(dirNormalized * maxDistance);

        RaycastHit hit;
        if (Physics.Linecast(transform.position, finalDir, out hit))
        {
            finalDistance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
        }
        else
        {
            finalDistance = maxDistance;
        }
        realCamera.localPosition = Vector3.Lerp(realCamera.localPosition, dirNormalized * finalDistance, Time.deltaTime * smoothness);

    }



    void sightviewChange()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel") * wheelSpeed;

        // 최대 줌인
        if (Cam_Game.fieldOfView <= 20.0f && scroll < 0)
        {
            Cam_Game.fieldOfView = 20.0f;
        }
        // 최대 줌 아웃
        else if (Cam_Game.fieldOfView >= 60.0f && scroll > 0)
        {
            Cam_Game.fieldOfView = 60.0f;
        }
        else
        {
            Cam_Game.fieldOfView += scroll;
        }

        // 일정 구간 줌으로 들어가면 캐릭터 바라보도록
        if (Cam_Game.fieldOfView <= 30.0f)
        {
            Debug.Log("캐릭터 쳐다봐");
            Cam_Game.transform.rotation = Quaternion.Slerp(Cam_Game.transform.rotation,
                Quaternion.LookRotation(characterHead.transform.position - Cam_Game.transform.position), -0.15f);

            ///Cam_Game.transform.LookAt(characterHead.transform);


        }
        else
        {
            Debug.Log("일반 상태");

            Cam_Game.transform.rotation = Quaternion.Slerp(Cam_Game.transform.rotation, Quaternion.LookRotation(worldDefaultForward), 0.15f);
        }

    }
}
