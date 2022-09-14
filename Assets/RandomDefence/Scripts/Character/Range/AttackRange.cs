using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    [SerializeField] private Character _character;


    private void Awake()
    {
        _character = this.gameObject.transform.parent.GetComponent<Character>();
    }

    public void GetCharacter( Character _instance)
    {
        _character = _instance;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (_character.isAttacking == false)
        {
            if (other.transform.tag.Contains("Monster"))
            {
                Debug.Log("나 적을 발견했어!");
                _character.AttackMode();
            }    
        }
    }
}
