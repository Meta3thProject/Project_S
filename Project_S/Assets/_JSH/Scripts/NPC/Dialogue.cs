using System;

[Serializable]
public class Dialogue
{
    // ID는 key

    /// <summary>
    /// 출력문
    /// </summary>
    public string dialogue;
    /// <summary>
    /// 다음 출력문 ID
    /// </summary>
    public int linkDialogue;

    public Dialogue(DIALOGUE_TABLEData data_)
    {
        dialogue = data_.DIALOGUE;
        linkDialogue = data_.LINK_DIALOG;
    }
}
