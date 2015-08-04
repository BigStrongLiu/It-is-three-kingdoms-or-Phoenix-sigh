using System.Collections.Generic;
using UnityEngine;
public class StateMachine
{
    private Dictionary<AIStateType, StateBase> m_StateDic;//所有状态
    private StateBase m_PreState;//之前的状态
    private StateBase m_CurState;//当前的状态
    private ActorBevBase m_ActorBev;
    private ActorAIBase m_ActorAI;
    private bool m_Over;

    public StateMachine(ActorBevBase actorBev, ActorAIBase actorAI)
    {
        this.m_ActorBev = actorBev;
        this.m_ActorAI = actorAI;
        this.m_StateDic = new Dictionary<AIStateType, StateBase>();
    }

    #region private methods

    /// <summary>
    /// 获取状态
    /// </summary>
    /// <param name="AIState"></param>
    /// <returns></returns>
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
                state = new AttackState(this.m_ActorBev,this.m_ActorAI);
                break;
            case AIStateType.Dead:
                state = new DeadState(this.m_ActorBev, this.m_ActorAI);
                break;
            case AIStateType.Hurt:
                state = new HurtState(this.m_ActorBev, this.m_ActorAI);
                break;
            case AIStateType.Idle:
                state = new IdleState(this.m_ActorBev, this.m_ActorAI);
                break;
            default: Debug.Log(AIState.ToString() + " not exsit "); return null;
        }
        this.m_StateDic.Add(AIState, state);
        return state;
    }

    #endregion

    #region public methods

    /// <summary>
    /// 设置状态
    /// </summary>
    /// <param name="AIState"></param>
    public void SetCurState(AIStateType AIState)
    {
        this.m_CurState = this.GetState(AIState);
        this.m_CurState.Enter();
    }

    /// <summary>
    /// 切换状态
    /// </summary>
    /// <param name="AIState"></param>
    public void ChangeState(AIStateType AIState)
    {
        if (AIState == this.m_CurState.AIState) return;
        this.m_CurState.Exit();
        this.m_PreState = this.m_CurState;
        this.m_CurState = this.GetState(AIState);
        this.m_CurState.Enter();
    }

    /// <summary>
    /// 切换状态
    /// </summary>
    /// <param name="state"></param>
    public void ChangeState(StateBase state)
    {
        if (state == this.m_CurState) return;
        this.m_CurState.Exit();
        this.m_PreState = this.m_CurState;
        state.Enter();
        this.m_CurState = state;
    }

    public void Update()
    {
        if (!this.m_Over)
        {
            if (this.m_ActorBev.IsDead)
            {
                this.ChangeState(AIStateType.Dead);
                this.m_Over = true;
            }
            else
            {
                this.m_CurState.Execute();
            }
        }
    }

    #endregion




}
