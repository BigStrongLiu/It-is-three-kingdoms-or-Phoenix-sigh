using System;

public class DeadState : StateBase
{
    public DeadState(ActorBevBase actorBev, ActorAIBase actorAI) :
        base(actorBev, actorAI)
    {
        this.AIState = AIStateType.Dead;
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
