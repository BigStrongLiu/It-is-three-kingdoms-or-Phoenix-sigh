using UnityEngine;

public class ActorAIBase : MonoBehaviour
{
    private StateMachine m_StateMachine;

    #region MonoBehaviour methods

    void State()
    {
        this.m_StateMachine = new StateMachine(this);
        this.m_StateMachine.SetCurState(AIStateType.Idle);
    }

    #endregion
}
