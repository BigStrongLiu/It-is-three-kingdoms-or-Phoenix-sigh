using System;

public class IdleState : StateBase
{
    public IdleState(ActorBevBase actorBev, ActorAIBase actorAI) :
        base(actorBev, actorAI)
    {
        this.AIState = AIStateType.Idle;
    }
    public override void Enter()
    {
    }

    public override void Execute()
    {
    }

    public override void Exit()
    {
    }
}
