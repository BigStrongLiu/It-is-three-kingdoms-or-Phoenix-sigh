using UnityEngine;

public class ActorBevBase:MonoBehaviour
{
    public ActorType Type { get; protected set; }
    public Transform MyTransform { get; private set; }
    public GameObject MyGameObject{ get; private set; }
    public ActorAIBase ActorAI { get; private set; }
    public ActorLogicData ActorLogicData { get; private set; }

    private bool m_IsMoves;//是否出招

    #region MonoBehaviour methods

    void Awake()
    {
        this.MyTransform = this.transform;
        this.MyGameObject = this.gameObject;
        this.SetActorType();
    }

    void Start()
    {
        this.ActorAI = this.MyGameObject.AddComponent<ActorAIBase>();
    }

    #endregion

    #region virtual methods

    protected virtual void SetActorType()    {    }

    #endregion

    #region protected methods

    protected void SetActorType(ActorType type)
    {
        this.Type = type;
    }

    #endregion

    #region public methods

    public void Init(ActorLogicData data)
    {
        this.ActorLogicData = data;
    }

    public bool IsMoves()
    {
        return this.m_IsMoves;
    }

    public void SetMoves()
    {
        this.m_IsMoves = true;
    }

    public void ClearMoves()
    {
        this.m_IsMoves = false;
    }

    #endregion
}
