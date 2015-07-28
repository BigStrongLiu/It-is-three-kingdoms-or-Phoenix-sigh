public abstract class StateBase:IState
{
    public AIStateType AIState { get; protected set; }

    protected ActorAIBase actorAI;
    public StateBase(ActorAIBase actor)
    {
        this.actorAI = actor;
    }
    public abstract void Enter();
    public abstract void Execute();
    public abstract void Exit();
}
