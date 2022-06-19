using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeyType = System.String;

[DisallowMultipleComponent] 
// DisallowMultipleComponent : 한 게임오브젝트에 스크립트가 두 개 이상 중복되지 않게 함
// [DisallowMultipleComponent(type of (추가 컴포넌트)] ex) rigidbody 방식으로 자동으로 같이 생성되게 할 수 있음
 
public class PoolObject : MonoBehaviour
{
    public KeyType key;

    /// <summary>
    /// 게임 오브젝트 복제
    /// </summary>
    /// <returns> PoolingObject </returns>
   public PoolObject Clone()
    {
        GameObject go = Instantiate(gameObject); // 프리팹 생성
        if (!go.TryGetComponent(out PoolObject po)) // 구성 요소가 있으면 true 반환, 그렇지 않으면 false 반환
            po = go.AddComponent<PoolObject>(); // po 객체를 반환시키고, PoolingObject 컴포넌트를 추가시킨다.
        go.SetActive(false); // 생성과 동시에 활성화 시킬것이 아니기 때문에 초기값 = 비활성화

        return po;
   }


    [ContextMenu("활성화")]
    public void Activate() 
    {
        gameObject.SetActive(true);
    }
    [ContextMenu("비활성화")]
    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
