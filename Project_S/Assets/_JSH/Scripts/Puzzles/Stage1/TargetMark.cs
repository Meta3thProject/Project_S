using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMark : MonoBehaviour
{
    // { 직렬화로 할당
    public BuildTestPuzzle puzzle;
    // 3번째 firebase
    public GameObject fireBase;
    // } 직렬화로 할당

    private void OnTriggerEnter(Collider other)
    {
        // 어차피 날아와서 부딪힐 건 정령의 손밖에 없음
        // 3번째 불 점화
        int targetIdx_ = puzzle.fireBaseList.IndexOf(fireBase);
        puzzle.Ignite(targetIdx_);
    }
}
