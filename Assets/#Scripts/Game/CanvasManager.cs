using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] Slider slider_hp;
    [SerializeField] Slider slider_mp;
    [SerializeField] Slider slider_exp;
    [SerializeField] Text text_Level;

    [SerializeField] int maxHp;
    [SerializeField] int maxMp;
    [SerializeField] int maxExp;

    [SerializeField] int curHp;
    [SerializeField] int curMp;
    [SerializeField] int curExp;

    const int lv1_hp = 200;
    const int lv1_mp = 200;
    const int lv1_exp = 20;
    [SerializeField] int currentLevel = 1;
    private void Awake()
    {
        setDefaultSliderValue();
    }

    void setDefaultSliderValue()
    {
        maxHp = lv1_hp;
        maxMp = lv1_mp;
        maxExp = lv1_exp;
        slider_hp.maxValue = maxHp;
        slider_mp.maxValue = maxMp;
        slider_exp.maxValue = maxExp;
        slider_hp.value = maxHp;
        slider_mp.value = maxMp;
        slider_exp.value = 0;
    }

    public void Damaged(int _damage)
    {
        curHp -= _damage;
        slider_hp.value = curHp;

        if(slider_hp.value <= 0 )
        {
            slider_hp.value = 0;
            Debug.Log("ав╬З╢ы");
        }
    }
    public void getExp(int getLength)
    {
        curExp += getLength;
        slider_exp.value = curExp;

        if (curExp >= maxExp)
        {
            LevelUp();
        }
    }
    void LevelUp()
    {
        currentLevel++;
        curExp = 0;
        slider_exp.value = curExp;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
