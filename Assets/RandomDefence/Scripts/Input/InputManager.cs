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
    public GameObject go_SelectCharacter;

    public Vector3 _destination;
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
            go_SelectCharacter = null;
            isSelect = false;
            Debug.Log(hitData.transform.name);
            if (hitData.transform.tag.Contains("Character"))
            {
                GameManager.Instance._ui.setSelectCharacterName(hitData.transform.GetComponent<Character>()._data._name);
                isSelect = true;
                go_SelectCharacter = hitData.transform.gameObject;
            }
            /*else
            {
                GameManager.Instance._ui.setSelectCharacterName("");
                isSelect = false;

            }*/
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
                MouseInput_Movement();
            }
        }
        Movement();
    }

    void Movement()
    {
        if(isSelect)
            go_SelectCharacter.transform.Translate(
                //new Vector3(_destination.x, 0.85f, _destination.z)
                _destination
                * Time.deltaTime * 2f, Space.World);
        
    }
}
