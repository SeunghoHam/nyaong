using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ChangeEndValue : MonoBehaviour
{
    // 트윈의 목표값을 바꿀 때 ChangeEndValue


    Tweener moveTween; // Tweener = Move 에 대한 트윈을 담당한다.
    Vector3 initPos;
    // Start is called before the first frame update
    void Start()
    {
        initPos = transform.position;
        moveTween = transform.DOMove(initPos, 1).SetAutoKill(false); // 자동삭제 X( Tweener 를 계속 재활용 한다)
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
		{
            //this.transform.DOKill();
            //this.transform.DOMove(Input.mousePosition, 1);
            moveTween.ChangeEndValue(Input.mousePosition, 1, true).Restart();
            // 파라미터| 1: 새 목표값, 2:새 트윈시간, 3:지금 상태에서 바로 연결할 건지

        }
        else if(Input.GetMouseButtonUp(0))
		{
            //this.transform.DOKill();
            //this.transform.DOLocalMove(Vector3.zero, 0.1f);
            moveTween.ChangeEndValue(initPos, 0.3f, true).Restart();
            // 파라미터| 1: 새 목표값, 2:새 트윈시간, 3:지금 상태에서 바로 연결할 건지

        }

        if(Input.GetKeyDown(KeyCode.Q))
		{
            moveTween.Kill();
            Debug.Log("트윈 삭제해서 움직임 없음");
		}
    }
}
