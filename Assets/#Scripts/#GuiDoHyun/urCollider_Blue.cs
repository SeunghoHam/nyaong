using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class urCollider_Blue : MonoBehaviour
{
    public urGameManager gm;
	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.name == "Sphere_blue")
		{
			gm.bOnBlue = true;
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.transform.name == "Sphere_blue")
		{
			gm.bOnBlue = false;
		}
	}
}
