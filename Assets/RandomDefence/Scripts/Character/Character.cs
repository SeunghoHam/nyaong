using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Character : MonoBehaviour
{
    [SerializeField] public CharacterData _data;
    [SerializeField] private GameObject _target;

    [SerializeField] private bool isAttacking;
    private void OnTriggerEnter(Collider other)
    {
        if (isAttacking == false)
        {
            if (other.transform.tag.Contains("Monster"))
            {
                Debug.Log("나 적을 발견했어!");
                AttackMode();
            }    
        }
    }

    void AttackMode()
    {
        // 공격 진행 후, 사정거리 밖이거나 공격하던 몬스터 사망 시 리셋 필요함
    }

    private Ray ray;

    private void Update()
    {
        //Physics.Raycast(ray, )
        allDirectionRay();
    }


    void allDirectionRay()
    {
        ray = new Ray(this.transform.position, transform.forward);
        RaycastHit hitData;
        Debug.DrawRay(this.transform.position, Vector3.forward * 5f, Color.red);
        
        if (Physics.Raycast(ray, out hitData))
        {
            
        }

    }
    
}
