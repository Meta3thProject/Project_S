using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPCState : MonoBehaviour
{
    /// <summary>
    /// 상태 진입시 실행되는 함수
    /// </summary>
    public abstract void OnStateEnter();

    /// <summary>
    /// 상태가 지속되는 한 실행되는 함수(Update) 
    /// <summary>
    public abstract void OnStateExecute();

    /// <summary>
    /// 다른 상태로 전이시 실행되는 함수
    /// </summary>
    public abstract void OnStateExit();
}
