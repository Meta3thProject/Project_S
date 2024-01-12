using System.Collections.Generic;
using UnityEngine;
using BNG;

public class PlayerStat : MonoBehaviour
{
    private Dictionary<string, float> MBTIStat = default;

    private Grabbable leftHeldGrabbable = default;
    private Grabbable rightHeldGrabbable = default;

    private void Awake()
    {
        // BSJ _ 240111 : 파이어베이스에 저장된 MBTI 값을 인게임내로 가져오는 메서드.
        FirebaseManager.instance.MbtiUpdateFromDB();
        
        Init();

        // TEST 용 
        //AddPoint(Define.MBTI_N, 10f);
        //AddPoint(Define.MBTI_E, 5f);
        //AddPoint(Define.MBTI_F, 1f);
        //AddPoint(Define.MBTI_P, 8f);
        //Debug.Log($"{GetMBTIStat()}");
    }

    void Init()
    {
        // bsj 240111 : 파이어 베이스 매니저에서 값을 받아와서 초기화하는 코드로 변경했음.
        #region MBTI Stat Init
        MBTIStat = new Dictionary<string, float>
        {
            { Define.MBTI_E, FirebaseManager.instance.MBTI_E },
            { Define.MBTI_I, FirebaseManager.instance.MBTI_I },
            { Define.MBTI_N, FirebaseManager.instance.MBTI_N },
            { Define.MBTI_S, FirebaseManager.instance.MBTI_S },
            { Define.MBTI_T, FirebaseManager.instance.MBTI_T },
            { Define.MBTI_F, FirebaseManager.instance.MBTI_F },
            { Define.MBTI_J, FirebaseManager.instance.MBTI_J },
            { Define.MBTI_P, FirebaseManager.instance.MBTI_P }
        };
        #endregion        

        leftHeldGrabbable = this.gameObject.GetChildObj("Grabber").GetComponent<Grabber>().HeldGrabbable;
        rightHeldGrabbable = this.gameObject.GetChildObj("Grabber").GetComponent<Grabber>().HeldGrabbable;
    }

    public int GetRightGrabbableID()
    {
        if (GFunc.IsValid(rightHeldGrabbable) == false) { return 0; }

        return rightHeldGrabbable.GetComponent<ItemData>().itemID;
    }

    public int GetLeftGrabbableID()
    {
        if (GFunc.IsValid(leftHeldGrabbable) == false) { return 0; }

        return leftHeldGrabbable.GetComponent<ItemData>().itemID;
    }

    // ! MBTI 점수를 올릴 때 함수 호출 
    public void AddPoint(string type_, float point_)
    {
        MBTIStat[type_] += point_;

        // bsj 240111 파이어 베이스에 업데이트되는 코드 추가했음.
        FirebaseManager.instance.PlayerMbtiUpdateToDB(type_, point_);

#if UNITY_EDITOR
        Debug.LogFormat("Type {0} : Point {1}", type_, MBTIStat[type_]);
#endif
    }       // AddPoint()

    // ! MBTI 문자열을 반환하는 퍼블릭 메서드
    // ! 외부에서 사용하기 위한 메서드
    public string GetMBTIStat()
    {
        Debug.Log("4GetMBTIStat들어옴?");

        return GetMBTI();
    }       // GetMBTIStat()

    // ! MBTI 문자열을 반환하는 메서드 
    private string GetMBTI()
    {
        string tempString = default;

        tempString += MBTIStat[Define.MBTI_E] >= MBTIStat[Define.MBTI_I] ? Define.MBTI_E : Define.MBTI_I;
        tempString += MBTIStat[Define.MBTI_N] >= MBTIStat[Define.MBTI_S] ? Define.MBTI_N : Define.MBTI_S;
        tempString += MBTIStat[Define.MBTI_T] >= MBTIStat[Define.MBTI_F] ? Define.MBTI_T : Define.MBTI_F;
        tempString += MBTIStat[Define.MBTI_J] >= MBTIStat[Define.MBTI_P] ? Define.MBTI_J : Define.MBTI_P;

        return tempString;

    }       // GetMBTI()

    // ! MBTI 점수가 필요할 때 호출
    public Dictionary<string, float> GetPlayerStat()
    {
        return MBTIStat;
    }       // GetPlayerStat()
}
