using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    private float moveSpeed =5f;
    private Vector3 direction;
    public void Move(Vector3 direction)
	{
        this.direction = direction;
        Invoke("DestroyNote", 3f);
	}

    public void DestroyNote()
	{
        MusicGameManager.ReturnObject(this);
	}
    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction *Time.deltaTime*moveSpeed);
    }
}
