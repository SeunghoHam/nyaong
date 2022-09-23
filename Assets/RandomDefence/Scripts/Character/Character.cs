using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [SerializeField] public CharacterData _data;
    //[SerializeField] private GameObject _target;

    [HideInInspector] public bool isAttacking;

    private Vector3 _movePos; // 이동을 멈출 곳(InputManager -> hitdata.point)
    private Vector3 _direction; // 이동을 진행할 방향벡터

    private bool isMoveing; // 이동중인지 / 공격시 이동이 멈추도록
    private Animator _animator; // 스크립트 위치랑 애니메이터 위치랑 같은 컴포넌트가 가지게 해야함
    
    private BoxCollider MoveRange;
    
    //[SerializeField] private GameObject MovementObject; 
    public void AttackMode()
    {
        // 공격 진행 후, 사정거리 밖이거나 공격하던 몬스터 사망 시 리셋 필요함
        Debug.Log("나 공격모드다!");
    }
    
    private Ray ray;

    private void Awake()
    {
        _animator = this.gameObject.GetComponent<Animator>();
        MoveRange = this.transform.GetChild(2).GetComponent<BoxCollider>(); // 자식들 위치 바뀌게 되면 설정 다시 해줘야 함
        SetMoveRange(false);
    }

    public void SetMoveRange(bool _isOn)
    {
        MoveRange.enabled = _isOn;
    }

    
    public void GetMovePos(Vector3 _PARAMmovePos, Vector3 _PARAMdirection)
    {
        isMoveing = true;
        _movePos = _PARAMmovePos;
        _direction = _PARAMdirection;
        _animator.SetBool("IsWalk", true);
    }
    private void Update()
    {
        KeyInput_Jump();
        
        Movement();
        if (Input.GetKeyDown(KeyCode.S))
        {
            isMoveing = false;
        }
    }   
    
    void Movement()
    {
        if (isMoveing)
        {
            this.transform.Translate(_direction * Time.deltaTime *2f, Space.World);
            //if (Mathf.Approximately(go_SelectCharacter.transform.position.x, _movePos.x)) // 이거 안됨
            if (false) // 목적지의 근사값 도착
            {
                isMoveing = false;
            }
        }
        
    }
    void KeyInput_Jump()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _animator.SetTrigger("Jump_Normal");
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            _animator.SetTrigger("Jump_Attack");
        }
    }

    /// <summary>
    /// InputManager에서 MovementObject의 위치를 할당시켜준다.
    /// </summary>
    /// <param name="_movePos">MovementObject에게 할당될 위치값</param>
    public void TransferMoveCollider(Vector3 _movePos)
    {
        //MovementObject.transform.position = _movePos;
        //MovementObject.GetComponent<BoxCollider>().enabled = true;
    }

    
    /// <summary>
    /// Charcter - MovemntCollider 가 충돌했다 / 이동 끝
    /// </summary>
    public void TransferComplited()
    {
        Debug.Log("이동 끝 멈춰라!");
        isMoveing= false;
        _animator.SetBool("IsWalk", false);
    }
}
