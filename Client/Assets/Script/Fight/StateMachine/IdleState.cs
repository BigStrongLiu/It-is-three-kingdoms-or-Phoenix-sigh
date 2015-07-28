using System;

public class IdleState : StateBase
{
    public IdleState(ActorAIBase actor) :
        base(actor)
    {
        this.AIState = AIStateType.Idle;
    }
    public override void Enter()
    {
    }

    public override void Execute()
    {
        throw new NotImplementedException();
    }

    public override void Exit()
    {
        throw new NotImplementedException();
    }
}
