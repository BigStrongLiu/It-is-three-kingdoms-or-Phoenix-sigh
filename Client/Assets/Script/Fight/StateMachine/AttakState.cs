using System;

public class AttackState : StateBase
{
    public AttackState(ActorAIBase actor) : 
        base(actor)
    {
        this.AIState = AIStateType.Attack;
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
