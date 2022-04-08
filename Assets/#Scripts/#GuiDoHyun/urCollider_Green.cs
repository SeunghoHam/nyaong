using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class urCollider_Green : MonoBehaviour
{
    public urGameManager gm;
	private void OnTriggerEnter(Collider other)
	{
        if (other.transform.name == "Sphere_green")
        {
            gm.bOnGreen = true;
        }
    }
	private void OnTriggerExit(Collider other)
	{
        if (other.transform.name == "Sphere_green")
        {
            gm.bOnGreen = false;
        }
    }
}
