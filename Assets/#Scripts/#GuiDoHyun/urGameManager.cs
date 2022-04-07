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


	RaycastHit hit;
	float MaxDistance = 40f;

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

	public float axis = 360f;
	public float angle = 2f;

	public float falseDelaytime = 0.6f;
	// Start is called before the first frame update

	private void Awake()
	{
		go_ball = new GameObject[go_turnActor.transform.childCount];

		for (int i = 0; i < go_turnActor.transform.childCount; i++)
		{
			go_ball[i] = go_turnActor.transform.GetChild(i).gameObject;
		}
		correctCount = 0;

		slider.value = sliderTime;
		slider.maxValue = sliderTime;
		isGame = true;
	}
	void Start()
	{
		//CheckCorrectCount();
	}



	void touch(RaycastHit hit, bool isCondition)
	{
		if (isCondition)
		{
			hit.transform.gameObject.SetActive(false);
			correctCount++;
			CheckCorrectCount();
			sliderReset();
		}
		else if (!isCondition)
		{
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
			Debug.DrawRay(Cam_Game.transform.position, ray.direction, Color.blue, 1f);
			if (Physics.Raycast(ray, out hit, Mathf.Infinity))
			{
				if (Input.GetMouseButtonDown(0))
				{
					//Debug.Log("클릭 인풋");
					if (hit.transform.name == "Sphere_blue")
					{
						//Debug.Log("파란색");
						touch(hit, bOnBlue);
					}
					else if (hit.transform.name == "Sphere_green")
					{
						//Debug.Log("초록색");
						touch(hit, bOnGreen);

					}
					else if (hit.transform.name == "Sphere_red")
					{
						//Debug.Log("빨간색");
						touch(hit, bOnRed);
					}
					else if(hit.transform.name == "Sphere_normal")
					{
						ballReset();
					}
				}
			}
			
			if (Input.GetKeyDown(KeyCode.Q)) axis += 10f;
			if (Input.GetKeyDown(KeyCode.W)) axis -= 10f;
			if (Input.GetKeyDown(KeyCode.E)) angle += 1f;
			if (Input.GetKeyDown(KeyCode.R)) angle -= 1f;

			CheckCorrectCount();
			CheckSlider();
		}
	}

	void CheckCorrectCount()
	{
		if(correctCount == 0)
		{
			angle = 2f;
		}
		if (correctCount == 1)
		{
			angle = 3f;
		}
		else if (correctCount == 2)
		{
			angle = 4f;
		}
		else if(correctCount == 3)
		{
			Debug.Log("correctCount = 3!");
			ballUnion();
			isGame = false;
			slider.gameObject.SetActive(false);
		}
	}

	void ballUnion()
	{
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
			Debug.Log("슬라이더 보이기");
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
		sliderActiveTime = 2f;
		Debug.Log("볼 리셋");
		for (int i = 0; i < go_ball.Length; i++)
		{
			go_ball[i].SetActive(true);
		}
		correctCount = 0;
		CheckCorrectCount();
	}
}
