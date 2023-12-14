using System;

// IESNTFJP 순서
public enum MBTI
{
    I = 1,
    E = 2,
    N = 3,
    S = 4,
    F = 5,
    T = 6,
    P = 7,
    J = 8
}

[Serializable]
public class Choice
{
    // ID는 key

    /// <summary>
    /// 첫번째 선택지
    /// </summary>
    public string choice1;
    /// <summary>
    /// 첫번째 선택지 선택 시 출력문 ID
    /// </summary>
    public int linkDlg1;
    /// <summary>
    /// 첫번째 선택지 선택 시 상승할 MBTI
    /// </summary>
    public MBTI value1;
    /// <summary>
    /// 첫번째 선택지 선택 시 상승량
    /// </summary>
    public int value2;

    /// <summary>
    /// 두번째 선택지
    /// </summary>
    public string choice2;
    /// <summary>
    /// 두번째 선택지 선택 시 출력문 ID
    /// </summary>
    public int linkDlg2;
    /// <summary>
    /// 두번째 선택지 선택 시 상승할 MBTI
    /// </summary>
    public MBTI value3;
    /// <summary>
    /// 두번째 선택지 선택 시 상승량
    /// </summary>
    public int value4;

    public Choice(CHOICE_TABLEData data_)
    {
        choice1 = data_.CHOICE1;
        linkDlg1 = data_.LINK_DLG1;

        value1 = (MBTI)data_.VALUE1;
        value2 = data_.VALUE2;

        choice2 = data_.CHOICE2;
        linkDlg2 = data_.LINK_DLG2;

        value3 = (MBTI)data_.VALUE3;
        value4 = data_.VALUE4;
    }
}
