using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    private float moveSpeed =8f;
    private Vector3 direction;
    public void Move(Vector3 direction)
	{
        this.direction = direction;
        //Invoke("DestroyNote", 3f);
	}

    public void DestroyNote()
	{
        //MusicGameManager.ReturnObject(this);
        MultiManager.ReturnObject(this);
	}
    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction *Time.deltaTime*moveSpeed);
    }

    public Transform tf;
    public Transform setTF()
	{
        return tf;
	}


	private void OnTriggerEnter(Collider other)
	{
        if(other.transform.name == "judgeLine_Bad")
		{
            //Debug.Log("");
            MultiManager.instance.bBad = true;
        }
        else if(other.transform.name == "judgeLine_Good")
		{
            MultiManager.instance.bGood = true;
		}
        else if(other.transform.name == "judgeLine_Perfect")
		{
            MultiManager.instance.bPerfect = true;
		}

        else if(other.transform.name == "3d_BlackWendigo")
		{
            MultiManager.instance.hit();
		}
    }
}
