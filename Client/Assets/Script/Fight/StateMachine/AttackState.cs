using System;

public class AttackState : StateBase
{
    public AttackState(ActorBevBase actorBev, ActorAIBase actorAI) :
        base(actorBev, actorAI)
    {
        this.AIState = AIStateType.Attack;
    }

    public override void Enter()
    {
        this.actorAI.Attack();
    }

    public override void Execute()
    {
        if (this.actorAI.Animator.IsCompleteAttackAnimation())
            actorBev.AttackComplete();
    }

    public override void Exit()
    {
    }
}
