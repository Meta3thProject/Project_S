using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class BrazierChecker : MonoBehaviour
{
    // 이 퍼즐은 2번째 퍼즐입니다. 퍼즐의 총 갯수 (0, 1, 2, 3)
    const int PUZZLEINDEX = 1;

    // BSJ _ 240115
    //[Header("횟불 정답")]
    //[SerializeField] bool answerBrazier00;
    //[SerializeField] bool answerBrazier01;
    //[SerializeField] bool answerBrazier02;
    //[SerializeField] bool answerBrazier03;

    [Header("현재 켜진 횟불")]
    [SerializeField] bool mirrorBrazier00;
    [SerializeField] bool mirrorBrazier01;
    [SerializeField] bool mirrorBrazier02;
    [SerializeField] bool mirrorBrazier03;

    // 클리어 여부를 확인하기 위한 스크립트
    private HiddenPuzzleClear hiddenPuzzleClear;

    private void Awake()
    {
        // 이 퍼즐의 인덱스 번호는 17번입니다.
        hiddenPuzzleClear = transform.root.GetChild(17).GetComponent<HiddenPuzzleClear>();

        // 정답은 미리 협의된 2, 3번이 정답으로 설정
        // BSJ _ 240115
        //answerBrazier00 = false;
        //answerBrazier01 = true;
        //answerBrazier02 = true;
        //answerBrazier03 = false;
    }

    /// <summary>
    /// 트리거에 토치가 닿아 불이 켜졌다면 체크할 메서드.
    /// </summary>
    /// <param name="_index"></param>
    /// <param name="_isFire"></param>
    public void CheckLightFire(int _index, bool _isFire)
    {
        switch (_index)
        {
            case 0:
                mirrorBrazier00 = _isFire;
                break;
            case 1:
                mirrorBrazier01 = _isFire;
                break;
            case 2:
                mirrorBrazier02 = _isFire;
                break;
            case 3:
                mirrorBrazier03 = _isFire;
                break;

            default: break;
        }

        ClearCheck();
    }

    /// <summary>
    /// 횟불을 올바르게 켰다면 해당 퍼즐의 클리어 여부를 히든 퍼즐 클리어 스크립트에 전달하는 메서드.
    /// </summary>
    public void ClearCheck()
    {
        // 횟불이 정답과 올바르게 켜졌다면, ( 0 = false / 1 = true / 2 = true / 3 = false )
        if (mirrorBrazier01 && mirrorBrazier02 && mirrorBrazier00 == false && mirrorBrazier03 == false)
        {
            hiddenPuzzleClear.IncreaseClearCheck(PUZZLEINDEX);
        }

        else
        {
            hiddenPuzzleClear.DecreaseClearCheck(PUZZLEINDEX);
        }
    }
}
