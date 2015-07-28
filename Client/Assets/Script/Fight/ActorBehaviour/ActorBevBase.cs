using UnityEngine;

public class ActorBevBase:MonoBehaviour
{
    public ActorType Type { get; protected set; }
    public Transform MyTransform { get; private set; }
    public GameObject MyGameObject{ get; private set; }
    public ActorAIBase ActorAI { get; private set; }
    public ActorLogicData ActorLogicData { get; private set; }

    #region MonoBehaviour methods

    void Awake()
    {
        this.MyTransform = this.transform;
        this.MyGameObject = this.gameObject;
    }

    void Start()
    {
        this.ActorAI = this.MyGameObject.AddComponent<ActorAIBase>();
    }

    #endregion

    #region public methods

    public void Init(ActorLogicData data)
    {
        this.ActorLogicData = data;
    }

    #endregion
}
