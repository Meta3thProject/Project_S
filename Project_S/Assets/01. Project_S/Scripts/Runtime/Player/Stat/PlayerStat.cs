using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    // TEST : MBTI 표현 방식에 따라 변수 및 선언 방식은 달라질 듯함
    // 현재 형태는 임시형태
    private Dictionary<string, float> MBTIStat = default;

    private void Awake()
    {
        Init();
    }

    void Init()
    {
        #region MBTI Stat Init
        MBTIStat = new Dictionary<string, float>
        {
            { Define.MBTI_I, 0f },
            { Define.MBTI_E, 0f },
            { Define.MBTI_N, 0f },
            { Define.MBTI_S, 0f },
            { Define.MBTI_F, 0f },
            { Define.MBTI_T, 0f },
            { Define.MBTI_P, 0f },
            { Define.MBTI_J, 0f }
        };
        #endregion        
    }

    // ! MBTI 점수를 올릴 때 함수 호출 
    public void AddPoint(string type_, float point_)
    {
        MBTIStat[type_] += point_;

#if UNITY_EDITOR       
            Debug.LogFormat("Type {0} : Point {1}", type_, MBTIStat[type_]);
#endif
    }       // AddPoint()

    // ! MBTI 점수가 필요할 때 호출
    public Dictionary<string, float> GetPlayerStat()
    {
        return MBTIStat;
    }       // GetPlayerStat()
}
