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
        this.actorBev.DelayMovesComplete();
    }

    public override void Execute()
    {

    }

    public override void Exit()
    {
    }
}
