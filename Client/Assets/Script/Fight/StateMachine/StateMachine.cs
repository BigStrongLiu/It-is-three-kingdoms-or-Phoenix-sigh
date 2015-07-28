using System.Collections.Generic;
using UnityEngine;
public class StateMachine
{
    private Dictionary<AIStateType, StateBase> m_StateDic;
    private StateBase m_PreState;
    private StateBase m_CurState;
    private ActorAIBase m_ActorAI;

    public StateMachine(ActorAIBase actor)
    {
        this.m_StateDic = new Dictionary<AIStateType, StateBase>();
        this.m_ActorAI = actor;
    }

    #region private methods

    private StateBase GetState(AIStateType AIState)
    {
        if (this.m_StateDic.ContainsKey(AIState))
        {
            return this.m_StateDic[AIState];
        }
        StateBase state;
        switch (AIState)
        {
            case AIStateType.Attack:
                state = new AttackState(this.m_ActorAI);
                break;
            case AIStateType.Dead:
                state = new DeadState(this.m_ActorAI);
                break;
            case AIStateType.Hurt:
                state = new HurtState(this.m_ActorAI);
                break;
            case AIStateType.Idle:
                state = new IdleState(this.m_ActorAI);
                break;
            default: Debug.Log(AIState.ToString() + " not exsit "); return null;
        }
        this.m_StateDic.Add(AIState, state);
        return state;
    }

    #endregion

    #region public methods

    public void SetCurState(AIStateType AIState)
    {
        this.m_CurState = this.GetState(AIState);
        this.m_CurState.Enter();
    }

    public void ChangeState(AIStateType AIState)
    {
        if (AIState == this.m_CurState.AIState) return;
        this.m_CurState.Exit();
        this.m_PreState = this.m_CurState;
        this.m_CurState = this.GetState(AIState);
        this.m_CurState.Enter();
    }

    public void ChangeState(StateBase state)
    {
        if (state == this.m_CurState) return;
        this.m_CurState.Exit();
        this.m_PreState = this.m_CurState;
        state.Enter();
        this.m_CurState = state;
    }

    #endregion




}
