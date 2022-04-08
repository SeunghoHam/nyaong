using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class urCollider_Red : MonoBehaviour
{
    public urGameManager gm;

	private void OnTriggerEnter(Collider other)
	{
		if(other.transform.name == "Sphere_red")
        {
            gm.bOnRed = true;
        }
	}

	private void OnTriggerExit(Collider other)
	{
        if (other.transform.name == "Sphere_red")
        {
            gm.bOnRed = false;
        }
    }
}
