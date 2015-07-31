public abstract class StateBase:IState
{
    public AIStateType AIState { get; protected set; }

    protected ActorBevBase actorBev { get; private set; }
    protected ActorAIBase actorAI { get; private set; }
    public StateBase(ActorBevBase actorBev, ActorAIBase actorAI)
    {
        this.actorBev = actorBev;
        this.actorAI = actorAI;
    }
    public abstract void Enter();
    public abstract void Execute();
    public abstract void Exit();
}
