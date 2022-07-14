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

    public Vector3 _destination;

    public Vector3 _movePos;
    // Checking Input
    private Camera camera;
    private RaycastHit hitData;
    private Vector3 hitPosition;
    private float hitDistance;

    private void Awake()
    {
        camera = Camera.main;
    }
    void MouseInput_Select()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        
        hitPosition = hitData.point;
        hitDistance = hitData.distance;
        if (Physics.Raycast(ray, out hitData))
        {
            //go_SelectCharacter = null;
            isSelect = false;
            Debug.Log(hitData.transform.name);
            if (hitData.transform.tag.Contains("Character"))
            {
                GameManager.Instance._ui.setSelectCharacterName(hitData.transform.GetComponent<Character>()._data._name);
                isSelect = true;
                go_SelectCharacter = hitData.transform.gameObject;
            }
            else if(hitData.transform.tag.Contains(""))
            {
                Debug.Log("태그가 비어있는 애 클릭함");
                GameManager.Instance._ui.setSelectCharacterName("");
                isSelect = false;

            }
        }
    }

    /// <summary> Character가 선택 되어있는 상태에서 우클릭 시 Map 범위 내에서 이동</summary>
    void MouseInput_Movement()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        //hitPosition = hitData.point;
        //hitDistance = hitData.distance;
        if (Physics.Raycast(ray, out hitData))
        {
            Vector3 dir = new Vector3(hitData.point.x - go_SelectCharacter.transform.position.x,
                0f,
                hitData.point.z - go_SelectCharacter.transform.position.z);
            //go_SelectCharacter.transform.rotation = Quaternion.LookRotation(dir);

            _destination = dir.normalized;
            _movePos = hitData.point;
        }
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MouseInput_Select();
        }
        else if(Input.GetMouseButtonDown(1))
        {
            if (isSelect)
            {
                isMoving = true;
                MouseInput_Movement();
            }
        }
        else if (Input.GetMouseButton(1))
        {
            if(isSelect)
                MouseInput_Movement();
        }
        
        Movement();
    }

    void Movement()
    {
        if (isMoving)
        {
            go_SelectCharacter.transform.Translate(
                //new Vector3(_destination.x, 0.85f, _destination.z)
                _destination
                * Time.deltaTime * 2f, Space.World);


            if(Mathf.Approximately(go_SelectCharacter.transform.position.x , _movePos.x)) //&&
               //Mathf.Approximately(go_SelectCharacter.transform.position.z , _destination.z))
               //if(go_SelectCharacter.transform.position == _movePos)
            
            
            
            //if(go_SelectCharacter.transform.position.x >_movePos.x) // 이동하려는 지점이 왼쪽에 있음  
            {
                Debug.Log("근사값 도착");
                isMoving = false;
            }
            else
            {
                
                Debug.Log("이동중 남은 거리 : " + (go_SelectCharacter.transform.position.x)+ " , " + (_movePos.x));
            }
        }
            
    }
}
