using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TopviewCharacter : MonoBehaviour
{
    public void AutoMove(Vector3 _movePos)
    {
        this.transform.position = Vector3.MoveTowards(transform.position, _movePos, Time.deltaTime * 10f);
    }

    
}
