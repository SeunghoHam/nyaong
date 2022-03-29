using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Tween : MonoBehaviour
{
	public Button btn_Btn;
	public Button btn_Btn2;
	public GameObject go_Test;
	public Text text_Test;
	public GameObject go_Array;
	GameObject[] go_Eases = new GameObject[4];

	public GameObject go_Relative;
	public GameObject go_tweenParams;
	

	public Ease ease1;
	public Ease ease2;
	public Ease ease3;


	string s_content;
	Vector3 originPosition;
	

	// 파라미터로 저장해서 빠르게 사용 가능
	TweenParams tweenParams = new TweenParams().SetDelay(1).SetEase(Ease.Linear).SetRelative().SetSpeedBased();


	private void Awake()
	{
		btn_Btn.onClick.AddListener(btn_Click);
		btn_Btn2.onClick.AddListener(btn_Click2);
		s_content = "두트윈 테스트";
		text_Test.text = "";
		originPosition = go_Test.transform.position;


		for (int i = 0; i < go_Array.transform.childCount; i++)
		{
			go_Eases[i] = go_Array.transform.GetChild(i).gameObject;
		}
	}
	// Start is called before the first frame update
	void Start()
	{
		go_tweenParams.transform.DOLocalMoveX(100, 100).SetAs(tweenParams); // 저장되어있는 파라미터를 SetAs 로 불러온다.
	}
	

	// Update is called once per frame
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Q))
		{
			go_Relative.transform.DOLocalMoveX(200, 1f).SetRelative();
		}
		if(Input.GetKeyDown(KeyCode.W))
		{
			go_Relative.transform.DOLocalMoveX(-200, 1f).SetRelative();
		}
		if (Input.GetKeyDown(KeyCode.E))
		{
			go_Relative.transform.DOLocalMoveX(600, 200).SetRelative().SetSpeedBased();
		}


	}

	void btn_Click()
	{
		go_Test.GetComponent<Image>().color = new Color(1, 1, 1, 1);
		go_Test.transform.position = originPosition;
		go_Test.transform.DOMove(Vector3.zero, 2f, false);
		go_Test.transform.DORotate(new Vector3(0, 0, -360f), 2f, RotateMode.LocalAxisAdd);
		go_Test.GetComponent<Image>().DOColor(Color.red, 2f);
		go_Test.GetComponent<Image>().DOFade(0f, 2f);
		text_Test.DOText(s_content, 2f, true, ScrambleMode.All);
	}
	void btn_Click2()
	{
		go_Eases[0].gameObject.transform.localPosition = new Vector3(-800, 0, 0);
		go_Eases[0].gameObject.transform.DOLocalMoveX(0, 1).SetEase(ease1);
		go_Eases[1].gameObject.transform.localPosition = new Vector3(-800, -200, 0);
		go_Eases[1].gameObject.transform.DOLocalMoveX(0, 1).SetEase(ease2);
		go_Eases[2].gameObject.transform.localPosition = new Vector3(-800, -400, 0);
		go_Eases[2].gameObject.transform.DOLocalMoveX(0, 1).SetEase(ease3).OnComplete(() => // 람다식으로 트윈 끝난 뒤 호출시킴
		{
			Debug.Log("트윈끝남!");
		});
	}
}