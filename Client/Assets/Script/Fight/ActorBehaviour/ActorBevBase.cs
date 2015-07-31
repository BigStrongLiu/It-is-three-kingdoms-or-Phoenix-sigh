using UnityEngine;
using System.Collections;

public class ActorBevBase : MonoBehaviour
{
    public ActorType Type { get; protected set; }
    public Transform MyTransform { get; private set; }
    public GameObject MyGameObject { get; private set; }
    public ActorAIBase ActorAI { get; private set; }
    public ActorLogicData ActorLogicData { get; private set; }
    public bool IsDead { get; private set; }

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
        this.ActorAI.InitStateMachine(this);
    }

    #endregion

    #region virtual methods

    protected virtual void SetActorType() { }

    #endregion

    #region protected methods

    protected void SetActorType(ActorType type)
    {
        this.Type = type;
    }

    #endregion

    #region public methods

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="data"></param>
    public void Init(ActorLogicData data)
    {
        this.ActorLogicData = data;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="data"></param>
    public void InitPlayer(long uid)
    {
        this.Init(LogicController.Instance.Actor.GetActorLogicDataByUID(uid));
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="data"></param>
    public void InitEnemy(int id)
    {
        ActorData actorData = LogicController.Instance.Actor.GetActorDataByID(id);
        ActorLogicData actorLogicData = new ActorLogicData(0, actorData);
        this.Init(actorLogicData);
    }

    /// <summary>
    /// 是否出招
    /// </summary>
    /// <returns></returns>
    public bool IsMoves()
    {
        return this.m_IsMoves;
    }

    /// <summary>
    /// 出招
    /// </summary>
    public void Moves()
    {
        this.m_IsMoves = true;
        //这里需要做其他技术,得出是普通攻击还是技能或者其他什么什么。。。
        this.ActorAI.Moves();
    }

    /// <summary>
    /// 清空出招
    /// </summary>
    public void ClearMoves()
    {
        this.m_IsMoves = false;
    }

    /// <summary>
    /// 下一回合
    /// </summary>
    public void NextRound()
    {
        this.ClearMoves();
    }

    /// <summary>
    /// 出招完成
    /// </summary>
    public void MovesComplete()
    {
        FightMgr.Instance.CurMovesComplete();
    }

    public void DelayMovesComplete()
    {
        StartCoroutine(this.DelayMovesComplete(2.0f));
    }

    private IEnumerator DelayMovesComplete(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        this.MovesComplete();
    }

    /// <summary>
    /// 攻击动画时间点
    /// </summary>
    private void Attacked()
    {

    }
    /// <summary>
    /// 蓄力动画时间点
    /// </summary>
    private void Charge() { }

    #endregion
}
