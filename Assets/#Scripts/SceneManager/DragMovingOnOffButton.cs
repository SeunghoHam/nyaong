using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using DG.Tweening;

public class DragMovingOnOffButton : MonoBehaviour
{
    bool isON = true;
    bool onoffMoving = false;
    float onoffPos = 0f;
    string onoffStr = "On";

    const float LEFT = -75f;
    const float RIGHT = 75f;
    const float WIDTH = 150f;
    const float HEIGHT = 100f;
    const float MOVE_SPEED = 1f;

    public Button btn;
    public Text text_Btn;
    public bool canbtnInput = true;

    private void Awake()
    {
        btn.onClick.AddListener(MoveBtn);
        text_Btn = btn.transform.GetChild(0).GetComponent<Text>();
        canbtnInput = true;
}
    void MoveBtn()
    {
        Debug.Log("MoveBtn 호출");
        //if (!canbtnInput) return;
        if (canbtnInput == true)
        {
            canbtnInput = false;
            if (isON) // on = Left
            {
                isON = false;
                Debug.Log("왼쪽으로 움직이기");
                btn.gameObject.transform.DOLocalMoveX(RIGHT, 0.2f).SetEase(Ease.Linear).OnComplete(() => { canbtnInput = true; });
                text_Btn.text = "On";
            }
            else // off = Right
            {
                isON = true;
                Debug.Log("오른쪽으로 움직이기");
                btn.gameObject.transform.DOLocalMoveX(LEFT, 0.2f).SetEase(Ease.Linear).OnComplete(() => { canbtnInput = true; });
                text_Btn.text = "Off";
            }
        }
    }
    void MoveActor()
    {
        Rect rect = GUILayoutUtility.GetRect(1f, HEIGHT);
        Rect bgRect = new Rect(rect);
        bgRect.x = LEFT + 1f;
        bgRect.xMax = RIGHT + WIDTH - 2f;
        EditorGUI.DrawRect(bgRect, new Color(0.15f, 0.15f, 0.15f));

        rect.width = WIDTH;
        rect.x = onoffPos;

        Color col = GUI.backgroundColor;
        GUI.backgroundColor = Color.black;

        if (GUI.Button(rect, onoffStr))
        {
            onoffMoving = true;
        }

        if (!onoffMoving)
        {
            if (isON)
            {
                onoffPos = LEFT;
                onoffStr = "On";
            }
            else
            {
                onoffPos = RIGHT;
                onoffStr = "Off";
            }
        }
        else
        {
            if (isON)
            {
                if (onoffPos < RIGHT)
                {
                    onoffPos += MOVE_SPEED;
                    //Repaint();

                    if (onoffPos >= RIGHT)
                    {
                        onoffMoving = false;
                        isON = false;
                    }
                }
            }
            else
            {
                if (onoffPos > LEFT)
                {
                    onoffPos -= MOVE_SPEED;
                    //Repaint();
                    if (onoffPos <= LEFT)
                    {
                        onoffMoving = false;
                        isON = true;
                    }
                }

            }
        }
        GUI.backgroundColor = col;
    }
}
