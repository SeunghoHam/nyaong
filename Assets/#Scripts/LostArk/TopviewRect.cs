using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TopviewRect : MonoBehaviour
{
    public Camera cam;
    private bool isMove;
    private Vector3 destination;

    NavMeshAgent agent;

    public GameObject go_MoveCharacter;
    private void Awake()
    {
        cam = Camera.main;
        agent = go_MoveCharacter.GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            if(Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
            {
                setDestination(hit.point);
            }
        }
        Move();
    }
    private void LateUpdate()
    {
        //this.transform.position = go_MoveCharacter.transform.position; //+= new Vector3(0, 7.09f, -6.4f);
    }
    void setDestination(Vector3 dest)
    {
        agent.SetDestination(dest);
        destination = dest;
        isMove = true;
    }
    public void Move()
    {
        if(isMove)
        {
            //var dir = destination - transform.position;
            //transform.forward = dir;
            // transform.position += dir.normalized * Time.deltaTime * 5f;
            if (agent.velocity.magnitude == 0f)
            {
                isMove = false;
                return;
            }
            var dir = new Vector3(agent.steeringTarget.x, go_MoveCharacter.transform.position.y, agent.steeringTarget.z) 
                - go_MoveCharacter.transform.position;
            
        }

    }

    
}
