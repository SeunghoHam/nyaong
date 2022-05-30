using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInputEffect : MonoBehaviour
{
    public InputField myInputField;
    public Text myText;

    private void Awake()
    {
        //myInputField.onValueChanged
    }
    void Start()
    {

    }
    
    // Update is called once per frame
    void Update()
    {
        //myInputField.onValueChanged.Invoke("isInput");
        //myInputField.onValueChanged.ToString();
        myText.text = myInputField.text;
    }
    string isInput()
    {
        string st = "";
        Debug.Log("입력에 변화가 있음");

        return st;
    }
    

    
}
