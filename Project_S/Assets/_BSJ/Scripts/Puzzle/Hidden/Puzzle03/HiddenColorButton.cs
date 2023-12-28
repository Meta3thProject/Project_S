using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HiddenColorButton : MonoBehaviour
{
    // 이 버튼의 인덱스 번호
    [SerializeField] private int buttonIndex;

    // 정답 번호
    [SerializeField] private int answerIndex;

    // 현재 번호
    [SerializeField] private int nowIndex;

    [SerializeField] private ColorButtonChecker buttonChecker;
    [SerializeField] private MeshRenderer meshRenderer;

    private void Awake()
    {
        buttonChecker = transform.parent.GetComponent<ColorButtonChecker>();
        meshRenderer = transform.GetComponent<MeshRenderer>();

        // 초기값 셋팅
        nowIndex = 0;
    }

    /// <summary>
    /// 버튼을 누를 때 마다 색상이 변하는 메서드.
    /// </summary>
    public void ChangeColor()
    {
        switch (nowIndex)
        {
            case 0:
                meshRenderer.material = buttonChecker.red;
                nowIndex = 1;
                break;
            case 1:
                meshRenderer.material = buttonChecker.yellow;
                nowIndex = 2;
                break;
            case 2:
                meshRenderer.material = buttonChecker.green;
                nowIndex = 3;
                break;
            case 3:
                meshRenderer.material = buttonChecker.white;
                nowIndex = 0;
                break;
        }

        CheckCorrect();
    }

    /// <summary>
    /// 정답의 번호와 같은 번호인지 체크하는 메서드.
    /// </summary>
    private void CheckCorrect()
    {
        if(answerIndex == nowIndex)
        {
            buttonChecker.CheckClearButton(buttonIndex, true);
        }

        else
        {
            buttonChecker.CheckClearButton(buttonIndex, false);
        }
    }
}
