using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MySceneManager : MonoBehaviour
{
	public CanvasGroup img_Fade; // 검정색 배경
	float fadeDuration = 2; // 암전되는 시간
	public GameObject go_Loading; // 로딩 애니메이션 같은거 들어있는 게임 오브젝트
	public Text text_Loading; // 퍼센트 게이지 텍스트


	public InputField inputField_ID;
	public InputField inputField_PW;

	string answer_ID = "root";
	string answer_PW = "1234";
	public GameObject go_LoginView;
	public GameObject go_SelectView;

	public Button btn_Login;
	public Button btn_Start;
	public static MySceneManager Instance
	{
		get
		{
            return instance;
		}
	}
    public static MySceneManager instance;
	private void Start()
	{
		if(instance != null)
		{
			Destroy(this.gameObject);
			return;
		}
		instance = this;

		DontDestroyOnLoad(this.gameObject);
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void Awake()
	{
		Debug.Log("Awake");
		btn_Login.onClick.AddListener(btnFunc_Login);
		btn_Start.onClick.AddListener(btnFunc_Start);
		inputField_ID.Select();
		go_LoginView.gameObject.SetActive(true);
		go_SelectView.gameObject.SetActive(false);
	}

	
	private void Update()
    {
		if (Input.GetKeyDown(KeyCode.Tab)) tabkeyManager();
		if (Input.GetKeyDown(KeyCode.Return)) btnFunc_Login(); // KeyCode.Return = Enter키
	}


	void tabkeyManager()
    {
		inputField_PW.Select();
    }
	void btnFunc_Login()
    {
		if (inputField_ID.text == answer_ID)
		{
			if (inputField_PW.text == answer_PW)
			{
				Debug.Log("로그인 성공!");
				MySceneManager.instance.ChangeScene("2");
			}
			else
			{
				Debug.Log("비밀번호 확인해");
				inputField_PW.text = null;
			}
		}
		else
		{
			Debug.Log("존재하지 않는 계정이다");
			inputField_PW.text = null;
		}
    }
	void btnFunc_Start()
    {
		MySceneManager.instance.ChangeScene("3");
    }


    #region Login
    private void OnDestroy()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}
	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		img_Fade.DOFade(0, fadeDuration) // alpha 0되기
			.OnStart(() =>
			{
				go_Loading.SetActive(false); // 로딩 이미지 안보여야함
			})
			.OnComplete(() =>
			{
				img_Fade.blocksRaycasts = false;
			});
	}
	public void ChangeScene(string sceneName) //전환할 씬 이름
	{
		img_Fade.DOFade(1, fadeDuration).OnStart(() =>
		{
			img_Fade.blocksRaycasts = true; // 클릭 막기
		}).OnComplete(() =>
		{
			// 로딩화면 띄우고 씬 로드 시작
			StartCoroutine(LoadScene(sceneName));
			go_LoginView.gameObject.SetActive(false);
			go_SelectView.gameObject.SetActive(true);
		});
    }


    IEnumerator LoadScene(string sceneName)
    {
        go_Loading.SetActive(true);

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false; // 퍼센트 딜레이용 | 퍼센트 100 되면 활성화 시킴

        float past_time = 0;
        float percentage = 0;

        while (!async.isDone)
        {
            yield return null;

            past_time += Time.deltaTime;

            if (percentage >= 90)
            {
                percentage = Mathf.Lerp(percentage, 100, past_time);

                if (percentage == 100)
                {
                    async.allowSceneActivation = true; // 씬 전환 준비 완료(percentage 가 100이다)
                }
            }
            else
            {
                percentage = Mathf.Lerp(percentage, async.progress * 100f, past_time);
                if (percentage >= 90)
                    past_time = 0;
            }
            text_Loading.text = percentage.ToString("0") + "%";  // 퍼센트 표기
        }
    }
    #endregion
}
