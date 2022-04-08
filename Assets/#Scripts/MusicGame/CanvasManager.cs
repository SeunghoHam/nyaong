using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public Button btn_Left;
    public Button btn_Right;
	private void Awake()
	{
        btn_Left.onClick.AddListener(btnFunc_Left);
        btn_Right.onClick.AddListener(btnFunc_Right);
	}
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void btnFunc_Left()
    { 
        
    }
    void btnFunc_Right()
    {
    }
}
