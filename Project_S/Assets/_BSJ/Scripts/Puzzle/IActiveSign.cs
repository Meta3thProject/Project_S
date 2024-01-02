using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 퍼즐을 클리어하면 팻말을 활성화/비활성화 하는 인터페이스
public interface IActiveSign
{
    // 클리어 팻말의 활성화 여부
    public void ActiveClearSign(bool _isClear);
}
