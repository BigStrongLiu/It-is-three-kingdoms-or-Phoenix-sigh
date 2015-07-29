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
        this.actorAI.Attack();
    }

    public override void Execute()
    {
    }

    public override void Exit()
    {
    }
}
