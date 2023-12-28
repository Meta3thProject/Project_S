using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterTrigger : MonoBehaviour
{
    // 이 트리거의 블록 타입
    [SerializeField] private LetterBlockType letterType;

    // Letter 블록
    private LetterBlock letterBlock;

    // 퍼즐 클리어 스크립트
    private LetterPuzzleClear letterClear;

    private void Awake()
    {
        letterClear = transform.root.GetComponent<LetterPuzzleClear>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Letter 블록 스크립트가 존재한다면
        if (other.GetComponent<LetterBlock>() != null)
        {
            // 위치를 딱 맞춰주는 로직
            letterBlock = other.GetComponent<LetterBlock>();
            // letterBlock.transform.position = new Vector3(transform.position.x, letterBlock.transform.position.y, transform.position.z);

            // 타입이 같다면,
            if (letterBlock.type == letterType)
            {
                letterBlock.EnterLetterTrigger();
                letterClear.IncreaseClearCheck((int)letterType);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<LetterBlock>() != null)
        {
            letterBlock = other.GetComponent<LetterBlock>();

            if (letterBlock.type == letterType)
            {
                letterClear.DecreaseClearCheck((int)letterType);
            }
        }
    }
}
