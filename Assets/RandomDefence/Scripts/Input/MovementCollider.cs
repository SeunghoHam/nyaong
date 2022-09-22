using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 이동하다가 이 스크립트를 가지고 있는 객체를 만나면 멈춘다.
/// </summary>
public class MovementCollider : MonoBehaviour
{
    //public Character _character

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("MoveRange"))
        {
           other.transform.parent.GetComponent<Character>().TransferComplited();
            // 캐릭터 움직임 정지시켜야함
        }
    }
}
