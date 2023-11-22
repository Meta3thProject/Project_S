using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStateMachine : MonoBehaviour
{
    public enum State
    {
        /// <summary> 대기 </summary>
        Idle,
        /// <summary> 플레이어 추적 </summary>
        Detect,
        /// <summary> 대화 </summary>
        Interact
    }

    public State currentState;

    private NPC_Idle idleState;
    private NPC_Detect detectState;
    private NPC_Interact interactState;

    public Dictionary<State, NPCState> enumToState;

    public NPCStateMachine()
    {
        currentState = State.Idle;

        idleState = new NPC_Idle();
        detectState = new NPC_Detect();
        interactState = new NPC_Interact();

        enumToState = new Dictionary<State, NPCState>();

        enumToState.Add(State.Idle, idleState);
        enumToState.Add(State.Detect, detectState);
        enumToState.Add(State.Interact, interactState);
    }

    public void ChangeState(State nextState_)
    {
        if (currentState == default)
        {
            return;
        }

        if (nextState_ != default)
        {
            enumToState[currentState].OnStateExit();

            currentState = nextState_;

            enumToState[currentState].OnStateEnter();
        }
    }
}
