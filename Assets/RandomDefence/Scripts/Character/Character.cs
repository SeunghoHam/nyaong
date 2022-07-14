using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] public CharacterData _data;
    [SerializeField] private GameObject _target;

    private void OnEnable()
    {
        StartCoroutine(BehaviorTree());
    }

    IEnumerator BehaviorTree()
    {
        while (_target == null)
        {
            
        }
        yield return null;
    }
}
