using System;

public class HurtState : StateBase
{
    public HurtState(ActorBevBase actorBev, ActorAIBase actorAI) :
        base(actorBev, actorAI)
    {
        this.AIState = AIStateType.Hurt;
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
