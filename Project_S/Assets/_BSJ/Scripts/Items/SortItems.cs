using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortItems : MonoBehaviour
{
    private DOTween_Test sortTween;

    // 자식오브젝트 순회 하면서 SortItemOriginPos() 함수 실행시키기
    public void SortAllItems()
    {
        for(int i = 0; i < transform.childCount; i++) 
        {
            sortTween = transform.GetChild(i).GetComponent<DOTween_Test>();
            sortTween.SortItemOriginPos();
        }
    }
}
