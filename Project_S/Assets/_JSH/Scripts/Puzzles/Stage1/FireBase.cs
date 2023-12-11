using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FireBase : MonoBehaviour
{
    // 직렬화로 할당
    public FireBasePuzzle puzzle;

    private void OnTriggerEnter(Collider other)
    {
        // "Torch"가 아니라면 함수 종료
        if (!other.gameObject.name.Equals("Torch")) { return; }
        // "Torch"라면 불붙음
        else
        {
            // 스스로를 리스트에서 찾아서 점화
            int targetIdx_ = puzzle.fireBaseList.IndexOf(gameObject);
            puzzle.Ignite(targetIdx_);
        }
    }
}
