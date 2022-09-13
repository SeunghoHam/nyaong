using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button btn_Create;

    public Button btn_Create_Character;
    
    
    
    // About HP
    public Text text_Hp;
    [SerializeField] private int _Hp;
    private int _maxHp;
    
    
    // About Money
    [SerializeField] private int _gold;
    
    
    // About Select
    public Text text_SelectCharacter;    
    
    // CharacterManager
    [Header("캐릭터매니저")] // 인스펙터에 드래그로 가져오는 방식으로 되어있는데, 자동으로 찾는 방법 찾아야함
    public CharacterManager chaMgr;
    private void Awake()
    {
         btn_Create.onClick.AddListener(btnFunc_Create);
         btn_Create_Character.onClick.AddListener(btnFunc_Create_Character);
         
         _Hp = _maxHp;
         text_Hp.text = "현재체력 : " + _Hp + " / 최대체력 : " + _maxHp;
    }


    public void setSelectCharacterName(string _name)
    {
        text_SelectCharacter.text = _name;
    }
    
    /// <summary>
    /// 체력에 변경사항이 있을 경우 호출됨.
    /// </summary>
    /// <param name="_value"></param>
    public void ChangeHp(int _value)
    {
        _Hp += _value;
        CheckHp();
    }

    /// <summary> 체력을 검사하여 죽었는지 확인 </summary>
    void CheckHp()
    {
        text_Hp.text = "현재체력 : " + _Hp + " / 최대체력 : " + _maxHp;
        if (_Hp > 0)
        {
            // 살아있다.
        }
        else
        {
            // 죽음.
        }
    }
    void btnFunc_Create()
    {
        MonsterPool.GetMonster();
    }

    /// <summary>
    /// 캐릭터 생성 함수
    /// </summary>
    void btnFunc_Create_Character()
    {
        chaMgr.RandomCreate(); 
    }
}
