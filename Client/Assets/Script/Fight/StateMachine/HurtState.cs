using System;

public class HurtState : StateBase
{
    public HurtState(ActorAIBase actor) : 
        base(actor)
    {
        this.AIState = AIStateType.Hurt;
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
