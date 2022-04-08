using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class urGameManager : MonoBehaviour
{
	public Camera Cam_Game;
	public BoxCollider col_Green;
	public BoxCollider col_Red;
	public BoxCollider col_Blue;

	public GameObject go_turnActor;
	public GameObject[] go_ball;
	private GameObject go_ChangedBall;
	//public GameObject[] activeBalls;

	RaycastHit hit;
	float MaxDistance = 100f;

	public bool bOnRed = false;
	public bool bOnBlue = false;
	public bool bOnGreen = false;
	public int correctCount;

	public Toggle toggle_Red;
	public Toggle toggle_Blue;
	public Toggle toggle_Green;

	public Slider slider;
	public float sliderTime = 5f;
	public float sliderActiveTime = 2f;
	public bool isOnslider;

	public bool isGame;

	public float axis = 360f; // 회전 방향
	public float angle = 1f; // 회전 속도
	const float turnSpeed = 1f;

	public float falseDelaytime = 0.6f;
	// Start is called before the first frame update

	private void Awake()
	{
		go_ball = new GameObject[go_turnActor.transform.childCount];
		for (int i = 0; i < go_turnActor.transform.childCount; i++)
		{
			go_ball[i] = go_turnActor.transform.GetChild(i).gameObject;
			go_ball[i].transform.GetChild(0).gameObject.SetActive(false);
		}
		correctCount = 0;

		slider.value = sliderTime;
		slider.maxValue = sliderTime;
		isGame = true;
	}
	void Start()
	{
		CheckCorrectCount();
	}



	void touch( bool isCondition)
	{
		if (isCondition)
		{
			Debug.Log("조건이 참");
			go_ChangedBall.gameObject.SetActive(false);
			correctCount++;
			CheckCorrectCount();
			sliderReset();
			if (go_ball.Length > 0)
				CheckActiveBall(go_ball.Length);
			else go_ball[0].gameObject.SetActive(false);

			stateReset();
		}
		else if (!isCondition)
		{
			Debug.Log("조건이 거짓");
			ballReset();
		}
	}

	// Update is called once per frame
	void Update()
	{
		go_turnActor.transform.Rotate(new Vector3(0, axis, 0), angle);
		if (isGame)
		{
			toggle_Blue.isOn = bOnBlue;
			toggle_Red.isOn = bOnRed;
			toggle_Green.isOn = bOnGreen;
			Ray ray = Cam_Game.ScreenPointToRay(Input.mousePosition);
			//Debug.DrawRay(Cam_Game.transform.position, ray.direction, Color.blue, 1f);
			if (Physics.Raycast(ray, out hit, Mathf.Infinity))
			{
				if (Input.GetMouseButtonDown(0))
				{
					if(hit.transform.name == "col_Red")
					{
						touch(bOnRed);
					}
					else if(hit.transform.name == "col_Blue")
					{
						touch(bOnBlue);
					}
					else if(hit.transform.name == "col_Green")
					{
						touch(bOnGreen);
					}
				}
			}
			
			if (Input.GetKeyDown(KeyCode.Q)) axis += 10f;
			if (Input.GetKeyDown(KeyCode.W)) axis -= 10f;
			if (Input.GetKeyDown(KeyCode.E)) angle += 1f;
			if (Input.GetKeyDown(KeyCode.R)) angle -= 1f;

			
			if (Input.GetKeyDown(KeyCode.T)) return_RandomObejctChange(); 
			CheckCorrectCount();
			//CheckSlider();
		}
	}

	void CheckCorrectCount()
	{
		if(correctCount == 0)
		{
			angle = turnSpeed;
		}
		if (correctCount == 1)
		{
			angle = turnSpeed + 0.1f;
		}
		else if (correctCount == 2)
		{
			angle = turnSpeed + 0.2f;
		}
		else if(correctCount == 3)
		{
			angle = turnSpeed + 0.3f;
		}
		else if(correctCount == 4)
		{
			angle = turnSpeed + 0.4f;
		}
		else if(correctCount == 5)
		{
			angle = turnSpeed + 0.5f;
		}
		else if (correctCount == 6)
		{
			Debug.Log("클리어!");
			ballUnion();
			isGame = false;
			slider.gameObject.SetActive(false);
		}

	}

	void ballUnion() // 엔딩 부분 스토리보드 나와야 알듯
	{
		for (int i = 0; i < go_turnActor.transform.childCount; i++)
		{
			go_turnActor.transform.GetChild(i).GetComponent<urBall>().ColorEnd();
		}


		float time = 4f;
		angle = 10f;
		go_turnActor.transform.DOScaleX(0f, time);
		go_turnActor.transform.DOScaleY(0f, time);
		go_turnActor.transform.DOScaleZ(0f, time).OnComplete(()=> 
		{
			go_turnActor.SetActive(false);
		});
	}

	void CheckSlider()
	{
		if (sliderActiveTime > 0)
		{
			sliderActiveTime -= Time.deltaTime;
			slider.gameObject.SetActive(false);
			sliderTime = 5f;
			slider.value = sliderTime;
		}
		else
		{
			//Debug.Log("슬라이더 보이기");
			activeSlider();
		}
	}
	void activeSlider()
	{

		sliderActiveTime = 0;
		slider.gameObject.SetActive(true);
		sliderTime -= Time.deltaTime;
		slider.value = sliderTime;

		if(sliderTime < 0 )
		{
			ballReset();
		}
	}
	void sliderReset()
	{
		sliderActiveTime = 2f;
	}
	void ballReset()
	{
		sliderReset();
		Debug.Log("볼 리셋");
		go_ball = new GameObject[go_turnActor.transform.childCount];

		for (int i = 0; i < go_turnActor.transform.childCount; i++)
		{
			go_ball[i] = go_turnActor.transform.GetChild(i).gameObject;
			go_ball[i].GetComponent<urBall>().ColorReset();
			go_ball[i].SetActive(true);
		}
		stateReset();
		correctCount = 0;
		CheckCorrectCount();
	}
	void stateReset()
	{
		bOnBlue = false;
		bOnRed = false;
		bOnGreen = false;
	}
	void CheckActiveBall(int activeBallCount)
	{
		//int j = 0;
		go_ball = new GameObject[activeBallCount-1];
		
		for (int i = 0; i < go_turnActor.transform.childCount; i++) 
		{ // 6번 반복
			if (go_turnActor.transform.GetChild(i).transform.gameObject.activeSelf)
			{ // turnActor의 자식중에 활성화 되어있는 애
				for (int j = 0; j < activeBallCount-1;)
				{
					go_ball[j] = go_turnActor.transform.GetChild(i).gameObject;
					j++;
				}
				//활성화 되어있는 turnActor의 자식을 go_ball 배열에 넣는다
			}
		}
		
	}
	void return_RandomObejctChange()
	{
		int ran_Obj = UnityEngine.Random.Range(0, go_ball.Length);
		int ran_Color = UnityEngine.Random.Range(0, 3);

		go_ball[ran_Obj].GetComponent<urBall>().Change(ran_Color);
		go_ChangedBall = go_ball[ran_Obj];
	}

}
