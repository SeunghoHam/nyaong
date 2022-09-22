using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class MenuProfile : MonoBehaviour
{
    [SerializeField] private Image img_Base; // 큰 전체배경
    [SerializeField] private Image img_InputField; // inputfield의 배경

    [SerializeField] private Text text_Name;
    [SerializeField] private Text text_PlaceHolder;
    
    void Start()
    {
        Init();
    }

    private void Init()
    {
        img_Base.color = new Color(1, 1, 1, 0);
        img_InputField.color = new Color(0.99f, 0.99f, 0.99f, 0);
        text_Name.color = new Color(0, 0, 0, 0);
        text_PlaceHolder.color = new Color(0.2f, 0.2f, 0.2f, 0);
    }

    private float _duration = 0.8f;
    public void ShowProfile()
    {
        img_Base.DOFade(1f, _duration).SetEase(Ease.Linear);
        img_InputField.DOFade(1f, _duration).SetEase(Ease.Linear);
        text_Name.DOFade(1f, _duration).SetEase(Ease.Linear);
        text_PlaceHolder.DOFade(1f, _duration).SetEase(Ease.Linear);
        
        img_Base.transform.DOLocalMoveY(-100, 0.5f).From(true).SetEase(Ease.OutSine); // 현재 위치로 이동하도록 하는거 찾아봐야댐
    }
}
