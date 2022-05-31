using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemControl : MonoBehaviour
{
    private NavMeshAgent navAgent;
    private float wanderDistacne = 3;

    public EnemData data;
    //public enem
    // Start is called before the first frame update
    void Start()
    {
        if (navAgent == null)
            navAgent = this.GetComponent<NavMeshAgent>();

        if (data != null)
            LoadEnemy(data);    
    }

    // Update is called once per frame
    void Update()
    {
        if (data == null)
            return;

        if (navAgent.remainingDistance < 1f)
            GetNewDestination();
    }

    void LoadEnemy(EnemData _data)
    {
        foreach (Transform child in this.transform)
        {
            if (Application.isEditor)
                DestroyImmediate(child.gameObject);

            else Destroy(child.gameObject);
        }

        GameObject visuals = Instantiate(data.model);
        visuals.transform.SetParent(this.transform);
        visuals.transform.localPosition = Vector3.zero;
        visuals.transform.rotation = Quaternion.identity;


        if (navAgent == null)
            navAgent = this.GetComponent<NavMeshAgent>();

        this.navAgent.speed = data.speed;
    }         
    void GetNewDestination()
    {
        Vector3 nextDestination = this.transform.position;
        nextDestination += wanderDistacne * new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(nextDestination, out hit, 3f, NavMesh.AllAreas))
            navAgent.SetDestination(hit.position);
    }
}
