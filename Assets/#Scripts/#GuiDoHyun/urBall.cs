using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class urBall : MonoBehaviour
{
    public urGameManager gm;
    private ParticleSystem particle_Aura;
    public bool isChange = false;


    Color32 color_Red = new Color32(255, 50, 50, 255);
    Color32 color_Blue = new Color32(40, 70, 255, 255);
    Color32 color_Green = new Color32(59, 255, 40, 255);
    Color32 color_Normal = new Color32(255, 255, 255, 255);
    Color32 color_End = new Color32(11, 0, 67, 255);
	private void Awake()
	{
        particle_Aura = this.transform.GetChild(0).GetComponent<ParticleSystem>();
	}
    public void ColorEnd()
	{
        this.gameObject.SetActive(true);
        particle_Aura.startColor = color_End;
        particle_Aura.gameObject.SetActive(true);
	}
    public void ColorReset()
	{
        this.transform.name = "Sphere_ball"; // 기본 상태로 이전
        particle_Aura.startColor = color_Normal;
        particle_Aura.gameObject.SetActive(false);
    }
    public void Change(int num)
	{
        if (num == 0)
		{
            // 초록색 파티클
            //particle_Aura.startColor = new Color32(1, 1, 1, 1);
            particle_Aura.startColor = color_Green;
            Debug.Log("초록");
            this.transform.name = "Sphere_green";
		}
        else if(num == 1)
        {
            // 빨간색 파티클
            particle_Aura.startColor = color_Red;
            Debug.Log("빨강");
            this.transform.name = "Sphere_red";
        }
        else if(num == 2)
		{
            // 파란색 파티클
            particle_Aura.startColor = color_Blue;
            Debug.Log("파랑");
            this.transform.name = "Sphere_blue";
        }
        particle_Aura.gameObject.SetActive(true);
    }
}
