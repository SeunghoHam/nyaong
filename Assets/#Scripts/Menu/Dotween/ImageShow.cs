using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ImageShow : MonoBehaviour
{
	Sequence mySequence;
	// Start is called before the first frame update

	private void OnEnable()
	{
		mySequence.Restart();
	}
	void Start()
	{
		/*
		mySequence = DOTween.Sequence()
			.SetAutoKill(false)
			.OnStart(() =>
		{
			this.transform.localScale = Vector3.zero;
			this.GetComponent<CanvasGroup>().alpha = 0;
		})
	.Append(this.transform.DOScale(1, 1).SetEase(Ease.OutBounce))
	.Join(this.GetComponent<CanvasGroup>().DOFade(1, 1));*/
	//.SetDelay(0.3f));

		mySequence = DOTween.Sequence()
			.SetAutoKill(false)
			.Append(this.transform.DOScale(1, 1).From(0)
			.SetEase(Ease.OutBounce))
			.Join(GetComponent<CanvasGroup>().DOFade(1, 1).From(0)).Pause();  // pause : 초기화 되자마자 바로 실행되지 않게.
		// OnStart에서 정의해주던것을 From의 파라미터 값 0 으로 인해서 초기값이 0 이 된다.
			
	}

	// Update is called once per frame
	void Update()
	{

	}
}
