using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MySceneManager : MonoBehaviour
{
	public CanvasGroup img_Fade;
	float fadeDuration = 2; // 암전되는 시간

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
	public void ChangeScene(string sceneName)
	{
		Debug.Log("ChangeScene");
		img_Fade.DOFade(1, fadeDuration).OnStart(() =>
		{
			img_Fade.blocksRaycasts = true; // 클릭 막기
		}).OnComplete(() =>
		{
			// 로딩화면 띄우고 씬 로드 시작
			StartCoroutine(LoadScene(sceneName));
		});
	}

	public GameObject go_Loading;
	public Text text_Loading;
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
}
