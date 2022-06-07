using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dic_Test : MonoBehaviour
{
    Dictionary<string, dic_Item> itemMap;
    // Start is called before the first frame update

    object a = 20;
    void Start()
    {
        //Debug.Log("A = " +  a);
        //Debug.Log("A*  = " + a.ToString());

        itemMap = new Dictionary<string, dic_Item>();

        string name;

        // ?아이템 추가
        name = "요정의 화살";
        itemMap.Add(name, new dic_Item(name, 8, 15));

        name = "오크 망치";
        itemMap.Add(name, new dic_Item(name, 16, 4));

        name = "마법사의 돌";
        itemMap.Add(name, new dic_Item(name, 8, 10));

        name = "랜서의 증표";
        //itemMap.Add(name, new dic_Itme(name, 13, 15));
        itemMap["랜서의 증표"] = new dic_Item(name, 13, 15);

        // ?아이템 검색
        if (itemMap.ContainsKey ("오크 망치"))
        {
            dic_Item item = itemMap["오크 망치"];
            item.Show(); // 아이템명,넘버, 키, 인덱스 값 출력
        }

        // ?아이템 출력
        var enumerator = itemMap.GetEnumerator(); // GetEnumerator = Dictionary<TKey, TValue> 를 반복하는 열거자를 반환한다.

        while(enumerator.MoveNext()) // MoveNext = 열거자를 Dictionary<TKey, TValue> 의 다음 요소로 이동한다.
        {
            var pair = enumerator.Current;
            dic_Item item = pair.Value; // Dictionary 의 Current 로 가져온 값
            item.Show(); 
        }

        // ?아이템 삭제 ( 키 )
        bool result = itemMap.Remove("마법사의 돌"); // bool 타입 리턴
        if(result)
        {
            Debug.Log("마법사의 돌 삭제");
        }

        // ?아이템 출력
        foreach(KeyValuePair<string, dic_Item> pair in itemMap)
        {
            dic_Item item = pair.Value; // itemMap(Dictoinary<string, dic_Item>() 중에서 pair
            item.Show();
        }

        // ? Dictionary 비우기
        itemMap.Clear();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
