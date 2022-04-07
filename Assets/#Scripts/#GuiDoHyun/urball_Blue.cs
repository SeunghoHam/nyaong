using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class urball_Blue : MonoBehaviour
{
	public urGameManager gm;
	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.name == "col_Blue") gm.bOnBlue = true;

	}
	private void OnTriggerExit(Collider other)
	{
		if (other.transform.name == "col_Blue") StartCoroutine(CRT_falseDelay());
	}
	IEnumerator CRT_falseDelay()
	{
		if (gm.correctCount == 0)
		{ yield return new WaitForSeconds(gm.falseDelaytime); //Debug.Log("correctCount = 0"); 
		}
		else if (gm.correctCount == 1)
		{ yield return new WaitForSeconds(gm.falseDelaytime); //Debug.Log("correctCount =1"); 
		}
		else if (gm.correctCount == 2)
		{ yield return new WaitForSeconds(gm.falseDelaytime); //Debug.Log("correctCount = 2"); 
		}
		gm.bOnBlue = false;
	}

}
