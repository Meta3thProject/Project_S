using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class GFunc
{
    // { 확률을 매개변수로 받아 성공했는지 아닌지 반환하는 함수
    public static bool CheckRate(float rate_)
    {
        // 0과 99 중 하나의 숫자 (난수)
        float randomNumber = Random.Range(0, 100);

        // 0부터 시작하므로 난수가 확률보다 낮으면 성공
        if (randomNumber < rate_) { return true; }
        // 확률보다 크거나 같으면 실패 
        else /*(randomNumber >= rate_)*/ { return false; }
    }
    // } 확률을 매개변수로 받아 성공했는지 아닌지 반환하는 함수


}
