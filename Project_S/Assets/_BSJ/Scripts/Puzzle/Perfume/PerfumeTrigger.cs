using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerfumeTrigger : MonoBehaviour
{
    private BulbChangeClear bulbChangeClear;    // 퍼즐 클리어 스크립트

    private BulbPuzzle bulb;            // 트리거에 닿은 전구
    private Rigidbody bulbRigidBody;    // 전구의 리지드바디

    private bool isBulbInTrigger;       // 이미 트리거와 상호작용하고 있는 전구가 있는지 체크
}
