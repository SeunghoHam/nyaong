using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MenuScene : MonoBehaviour
{
    [SerializeField] private Image img_FrontScreen;
    [SerializeField] private Image img_BackScreen;
    [SerializeField] private MenuProfile Profile;
    [SerializeField] private InputField _codeInputField;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        _codeInputField.onEndEdit.AddListener(delegate {CodeCheck(_codeInputField); });
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Profile.ShowProfile();
        }
    }

    void CodeCheck(InputField _input)
    {
        Debug.Log("입력 끝 입력된 수는 : "  +_input.text.Length);
        if (_input.text == "asdf")
        {
            SceneManager.LoadScene("RD");
        }
    }
}
