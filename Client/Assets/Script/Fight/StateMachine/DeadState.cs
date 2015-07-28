using System;

public class DeadState : StateBase
{
    public DeadState(ActorAIBase actor) : 
        base(actor)
    {
        this.AIState = AIStateType.Dead;
    }

    public override void Enter()
    {
        throw new NotImplementedException();
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
