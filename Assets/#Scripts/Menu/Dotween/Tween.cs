using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Tween : MonoBehaviour
{
	public Button btn_Btn;
	public Button btn_Btn2;
	public Button btn_Btn3;
	public Button btn_Btn4;
	public GameObject go_Test;
	public Text text_Test;
	public GameObject go_Array;
	GameObject[] go_Eases = new GameObject[4];

	public GameObject go_Relative;
	public GameObject go_tweenParams;
	public GameObject go_Pomoolsun;
	public GameObject go_RotationlLoop;
	public GameObject go_From;
	public GameObject go_imageShow_From;
	public GameObject go_AnimationCurve;

	[Header("3dObject")]
	public GameObject go_FadeCube;
	public GameObject go_FlipCube;
	public GameObject go_Quad;

	public AnimationCurve animationCurve;
	
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
		btn_Btn3.onClick.AddListener(btn_Click3);
		btn_Btn4.onClick.AddListener(btn_Click4);
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
		go_Pomoolsun.transform.DOMoveX(0, 3).SetEase(Ease.OutQuad);
		go_Pomoolsun.transform.DOMoveY(0, 3).SetEase(Ease.InQuad);

		go_FadeCube.GetComponent<MeshRenderer>().material.DOFade(0, 1).SetLoops(-1, LoopType.Yoyo); // yoyo = 했던거 다시 리버스
		go_RotationlLoop.transform.DORotate(new Vector3(0, 0, 360), 3, RotateMode.FastBeyond360) // FastBeyond360 : 360도와 0도를 판단하게 해준다
			.SetLoops(-1, LoopType.Incremental)
			.SetEase(Ease.Linear);


		Sequence loopSequence = DOTween.Sequence()
			.Append(go_RotationlLoop.transform.DOMove(Vector3.zero, 1).SetRelative().SetLoops(-1, LoopType.Yoyo));
	}
	

	// Update is called once per frame
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Q))
		{
			//go_Relative.transform.DOLocalMoveX(200, 1f).SetRelative();
			Show();
		}
		if(Input.GetKeyDown(KeyCode.W))
		{
			//go_Relative.transform.DOLocalMoveX(-200, 1f).SetRelative();
			Hide();
		}
		if (Input.GetKeyDown(KeyCode.E))
		{
			go_Relative.transform.DOLocalMoveX(600, 200).SetRelative().SetSpeedBased();
		}
	}

	void Show()
	{
		go_Quad.GetComponent<MeshRenderer>().material.DOOffset(Vector2.zero, 3).SetEase(Ease.Linear);
	}
	void Hide()
	{
		go_Quad.GetComponent<MeshRenderer>().material.DOOffset(Vector2.down, 3).SetEase(Ease.Linear);
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
	void btn_Click3()
	{
		go_From.transform.DOLocalMoveX(200, 2).From(0, false, true).SetDelay(1);

		// From 활용하기
		go_imageShow_From.GetComponent<CanvasGroup>().DOFade(0, 2).From(); // From으로 인해서 0에서 시작되서 2초안에 1이 된다.
		go_imageShow_From.transform.DOLocalMoveY(-200, 2).From(true); // From으로 -200 이 초기값이 되고, true 파라미터로 상대이동(Relative)이 된다.
	}
	bool isCheck;
	void btn_Click4()
	{
		Vector3 movePos = new Vector3(400, 400, 0);
		go_AnimationCurve.transform.DOLocalMove(movePos, 2f).SetEase(animationCurve).From(new Vector3(-745,45,0));
		go_FlipCube.transform.DOLocalRotate(new Vector3(0, -360, 0), 1, RotateMode.FastBeyond360) // RotateMode설정해줘야 0과 360을 인식함
			.OnPlay(()=>
			{
				isCheck = false;
			})
			.OnUpdate(() =>
			{
				if (Mathf.Abs(go_FlipCube.transform.localRotation.eulerAngles.y - 180) <= 5) click4Event();
			});
	}
	void click4Event()
	{
		if (isCheck) return;
		Debug.Log("click4Event");
		isCheck = true;
	}
}