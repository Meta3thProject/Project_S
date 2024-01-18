using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

// 소지 아이템
public class PossessionItem : MonoBehaviour
{
    public static PossessionItem Instance;

    public PlayerStat status;

    private void Awake()
    {
        Instance = this;

        // BSJ _ 240115
        //// { 싱글톤
        //if (null == Instance)
        //{
        //    Instance = this;
        //    DontDestroyOnLoad(this.gameObject);
        //}
        //else
        //{
        //    Destroy(this.gameObject);
        //}
        //// } 싱글톤
    }

    public bool CheckOneValue(int value1_, int value2_)
    {
        // 소지중인 별의 총 개수 체크
        if (value1_ == 7777777)
        {
            if (StarManager.starManager.getStarCount >= value2_) { return true; }
        }
        // 들고 있는 물건 체크
        else if (value1_ != 7777777)
        {
            if (value1_ == status.GetRightGrabbableID() || value1_ == status.GetLeftGrabbableID()) { return true; }
        }
        else { /* Do Nothing */ }

        return false;
    }

    public bool CheckTwoValue(int value1_, int value2_)
    {
        // 들고 있는 물건 체크
        if (value1_ == status.GetRightGrabbableID() || value1_ == status.GetLeftGrabbableID()) { return true; }
        // 반대로도 체크
        else if (value2_ == status.GetRightGrabbableID() || value2_ == status.GetLeftGrabbableID()) { return true; }
        else { return false; }
    }

    public bool CheckValue1(int value1_)
    {
        if (value1_ == status.GetRightGrabbableID() || value1_ == status.GetLeftGrabbableID()) { return true; }
        else { return false; }
    }

    public bool CheckValue2(int value2_)
    {
        if (value2_ == status.GetRightGrabbableID() || value2_ == status.GetLeftGrabbableID()) { return true; }
        else { return false; }
    }
}
