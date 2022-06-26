using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{
    public GameObject img_Black;
    public Text text_Loading;
    public GameObject block_Right;
    public GameObject block_Left;


    string string_Loading;

    Vector3 originPos_Right;
    Vector3 originPos_Left;
    // Start is called before the first frame update
    bool isClick;
	private void Awake()
	{
        originPos_Right = new Vector3(600, block_Right.transform.position.y, 0f);
        originPos_Left = new Vector3(-600, block_Left.transform.position.y, 0f);
        isClick = false;
        string_Loading = "...HMD Connection in Progress...";
        text_Loading.text = "";

        block_Right.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        block_Left.GetComponent<Image>().color = new Color(1, 1, 1, 0);

    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) //&& !isClick)
		{
            //isClick = true;
            Click();
		}
    }

    void Click()
	{
        text_Loading.DOText(string_Loading, 2f, true, ScrambleMode.All);
        block_Right.transform.DOLocalMoveX(-300, 2f).SetRelative().From(originPos_Right);
        block_Left.transform.DOLocalMoveX(300, 2f).SetRelative().From(originPos_Left);
        block_Right.GetComponent<Image>().DOFade(1, 2f).From(0);
        block_Left.GetComponent<Image>().DOFade(1, 2f).From(0);
    }
}
