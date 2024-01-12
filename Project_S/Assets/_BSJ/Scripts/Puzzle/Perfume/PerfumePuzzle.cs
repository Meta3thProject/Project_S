using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PerfumeType
{
    AnswerPerfume01, AnswerPerfume02, AnswerPerfume03, WrongPerfume
}

public class PerfumePuzzle : MonoBehaviour
{
    // 이 향수의 타입
    [field: SerializeField] public PerfumeType perfumeType { get; private set; }

    Rigidbody rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// 향수 트리거에 닿았을때 실행할 메서드.
    /// </summary>
    public void EnterPerfumeTrigger(bool isEnterTrigger_ = true)
    {
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
    }
}
