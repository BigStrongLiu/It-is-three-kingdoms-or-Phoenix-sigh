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
        this.m_StateMachine = new StateMachine(this);
        this.m_StateMachine.SetCurState(AIStateType.Idle);
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
    #endregion
}
