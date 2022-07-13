using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button btn_Create;
    // Start is called before the first frame update
    
    
    // About HP
    public Text text_Hp;
    [SerializeField] private int _Hp;
    private int _maxHp;
    private void Awake()
    {
         btn_Create.onClick.AddListener(btnFunc_Create);
         _Hp = _maxHp;
         text_Hp.text = _Hp + "/" + _maxHp;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        text_Hp.text = _Hp + "/" + _maxHp;
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
    
}
