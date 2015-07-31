using UnityEngine;

public class ActorAIBase : MonoBehaviour
{
    public Transform MyTransform { get; private set; }
    public GameObject MyGameObject { get; private set; }
    public ActorAnimator Animator { get; private set; }

    private StateMachine m_StateMachine;

    #region MonoBehaviour methods

    void Awake()
    {
        this.MyTransform = this.transform;
        this.MyGameObject = this.gameObject;
        this.Animator = this.MyGameObject.AddComponent<ActorAnimator>();
    }

    void Start()
    {
        this.m_StateMachine.SetCurState(AIStateType.Idle);
    }

    void Update()
    {
        this.m_StateMachine.Update();
    }

    #endregion

    #region public methods

    public void Idle()
    {
        this.Animator.Idle();
    }

    public void Attack()
    {
        this.Animator.Attack();
    }

    public void Moves()
    {
        this.m_StateMachine.SetCurState(AIStateType.Attack);
    }

    public void InitStateMachine(ActorBevBase actorBev)
    {
        this.m_StateMachine = new StateMachine(actorBev,this);
    }
    #endregion
}
