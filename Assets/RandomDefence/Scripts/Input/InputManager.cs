using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // CharcterInputMove
    [SerializeField] private bool isSelect;
    [SerializeField] private bool isMoving;
    public GameObject go_SelectCharacter;

    private Vector3 _direction;
    private Vector3 _movePos;

    [SerializeField] private bool canTransfer; // 이동 시킬때의 딜레이 ( 꾹누르기 방지 )
    
    // Checking Input
    private Camera camera;
    //private RaycastHit hitData;
    private Vector3 hitPosition;
    private float hitDistance;


    [SerializeField] private GameObject movementCollider;



    [SerializeField] private List<Character> List_Character = new List<Character>();
    [SerializeField] private List<MovementCollider> List_MovementObject = new List<MovementCollider>();
    
    private void Awake()
    {
        camera = Camera.main;
        canTransfer = true;
    }
    void MouseInput_Select()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitData;
  
        if (Physics.Raycast(ray, out hitData, 1000f))
        {
            isSelect = false;
            //if (hitData.transform.tag.Contains("SelectRange")) // 캐릭터를 이동 or 스킬 사용을 위해 클릭함
            if(hitData.transform.CompareTag("SelectRange"))
            {
                Debug.Log("캐릭터 클릭 성공");
                go_SelectCharacter = hitData.transform.parent.gameObject; // SelectRange 부모에 캐릭터
                //Debug.DrawRay(camera.transform.position, hitData.transform.position ,Color.blue ,1f);
                GameManager.Instance._ui.setSelectCharacterName(go_SelectCharacter.GetComponent<Character>()._data._name);
                //go_SelectCharacter.GetComponent<Character>().SetMoveRange(true);
                isSelect = true;
                List_Character.Add(go_SelectCharacter.GetComponent<Character>());
            }
            else
            {
                Debug.Log("태그가 비어있는 애 클릭함");
                GameManager.Instance._ui.setSelectCharacterName("");
                //go_SelectCharacter.GetComponent<Character>().SetMoveRange(false);
                go_SelectCharacter = null;
                isSelect = false;
            }
        }
    }

    /// <summary> Character가 선택 되어있는 상태에서 우클릭 시 Map 범위 내에서 이동</summary>
    void MouseInput_Movement()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitData;
        
        //hitPosition = hitData.point;
        //hitDistance = hitData.distance;
        if (Physics.Raycast(ray, out hitData))
        {
            Debug.Log("캐릭터 이동");
            // LookAt
            //go_SelectCharacter.transform.DOLookAt(hitData.transform.position, 0.15f).SetEase(Ease.Linear);
            go_SelectCharacter.transform.LookAt(hitData.transform.position);
            
            // 이동
            Vector3 dir = new Vector3(hitData.point.x - go_SelectCharacter.transform.position.x,
                0f,
                hitData.point.z - go_SelectCharacter.transform.position.z);
            _direction = dir.normalized; // 방향벡터
            _movePos = hitData.point; // 목적지의 위치
            var mmObject = Instantiate(movementCollider, _movePos, Quaternion.identity);
            //go_SelectCharacter.GetComponent<Character>().TransferMoveCollider(_movePos);
            go_SelectCharacter.GetComponent<Character>().GetMovePos(_movePos, _direction);
            List_MovementObject.Add(mmObject.GetComponent<MovementCollider>());
            Debug.Log("리스트 확인. 캐릭터 : " + List_Character.FindIndex(x=>x.Equals(go_SelectCharacter)).ToString() 
                                      +"mm오브젝트 : " + List_MovementObject.FindIndex(x => x.Equals(mmObject)));
            canTransfer = false;
            StartCoroutine(CRT_TransferDelay());
        }
    }

    IEnumerator CRT_TransferDelay()
    {
        yield return new WaitForSeconds(0.5f);
        canTransfer =true;
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MouseInput_Select();
        }
        else if (Input.GetMouseButtonDown(1) && canTransfer)
        {
            if (isSelect)
            {
                isMoving = true;
                MouseInput_Movement();
            }
            else
            {
                Debug.Log("선택 안하고 우클릭");
            }
        }
        /*
        else if (Input.GetMouseButton(1) && canTransfer)
        {
            if(isSelect)
                MouseInput_Movement();
        }*/
    }
    
    /// <summary> 마우스 우클릭으로 "이동 명령 내린 위치 || 캐릭터 현재 위치" 를 검사하여 멈추는 지점을 설정한다.</summary>
    /// _movePos = 이동해야할 지점
    void CheckPosition() 
    {
        //if(_movePos )
    }
}
