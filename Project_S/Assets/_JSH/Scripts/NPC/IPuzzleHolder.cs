using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 퍼즐형 퀘스트를 가진 NPC를 위한 인터페이스
public interface IPuzzleHolder
{
    // 퍼즐 클리어 여부를 반환하는 함수
    public bool PuzzleClearCheck();
}
