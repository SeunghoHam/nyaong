using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] public CharacterData _data;

    private void Update()
    {
        //Movement(_destination);
    }

    private Vector3 _destination;
    public void Movement(Vector3 _Des)
    {
        //_destination = _Des;
        //this.gameObject.transform.Translate(_destination * _data._speed * Time.deltaTime, Space.World);
    }
}
