using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class ActorBevBase : MonoBehaviour
{
    public ActorType Type { get; protected set; }//类型
    public Transform MyTransform { get; private set; }
    public GameObject MyGameObject { get; private set; }
    public ActorAIBase ActorAI { get; private set; }
    public ActorLogicData ActorLogicData { get; private set; }
    public byte Index { get; set; }//位置索引
    public bool IsDead { get; private set; }//是否死亡

    private bool m_IsMoves;//是否出招
    private float m_iTweenMoveTime = 0.2f;//移动时间
    private Vector3 m_DefaultPosition;//初始位置
    private List<ActorBevBase> m_TargetList = new List<ActorBevBase>();//出招目标列表

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

    #region abstract methods

    protected abstract void SetActorType();

    #endregion

    #region public methods

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="data"></param>
    public void Init(ActorLogicData data)
    {
        this.ActorLogicData = data;
        this.m_DefaultPosition = this.MyTransform.position;
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
    public void Moves(ActorBevBase target, Vector3 movesPosition)
    {
        //Debug.Log("准备出招");
        this.m_TargetList.Add(target);
        //这里需要做其他计算,得出是普通攻击还是技能或者其他什么什么的。。。
        this.MoveToTarget(movesPosition, true);
        this.m_IsMoves = true;
    }

    private void MoveToTarget(Vector3 position, bool goTo)
    {
        iTween.MoveTo(this.MyGameObject, iTween.Hash(iT.MoveTo.time, this.m_iTweenMoveTime,
            iT.MoveTo.position, position, iT.MoveTo.islocal, false,
            "ignoretimescale", false, iT.MoveTo.easetype, iTween.EaseType.linear,
          iT.MoveTo.oncomplete, "MoveComplete",
          iT.MoveAdd.oncompleteparams, goTo,
          iT.MoveTo.oncompletetarget, this.MyGameObject));
    }

    private void MoveComplete(bool goTo)
    {
        if (goTo)
        {
            //Debug.Log("移动完成 攻击");
            this.ActorAI.Moves();
        }
        else
        {
            this.ActorMovesComplete();
        }
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
    /// 攻击完成
    /// </summary>
    public void AttackComplete()
    {
        //Debug.Log("攻击完成 移动回来");
        this.ActorAI.AttackComplete();
        this.MoveToTarget(this.m_DefaultPosition, false);
    }

    /// <summary>
    /// 出招完成
    /// 出招分为:
    ///     1.移动过去
    ///     2.攻击
    ///     3.移动回来
    /// </summary>
    public void ActorMovesComplete()
    {
        //Debug.Log("出招结束");
        FightMgr.Instance.CurMovesComplete();
        this.m_TargetList.Clear();
    }

    /// <summary>
    /// 被攻击
    /// </summary>
    public void BeAttacked()
    {
        Debug.Log(this.name + " 被攻击");
        this.Shake();
    }

    private void Shake()
    {
        //iTween.MoveAdd(this.MyGameObject, iTween.Hash(iT.MoveAdd.time, 0.05f, iT.MoveTo.y, 0.5f,
        //            iT.MoveTo.easetype, iTween.EaseType.linear,iT.MoveTo.islocal,false));
        //iTween.MoveAdd(this.MyGameObject, iTween.Hash(iT.MoveTo.delay, 0.05f, iT.MoveAdd.time, 0.05f, iT.MoveTo.y, 0.5f,
        //   iT.MoveTo.easetype, iTween.EaseType.linear, iT.MoveTo.islocal, false));

        iTween.RotateAdd(this.MyGameObject, iTween.Hash(iT.RotateAdd.time, 0.05f, iT.RotateAdd.x, -20,
                   iT.RotateAdd.easetype, iTween.EaseType.linear));
        iTween.RotateAdd(this.MyGameObject, iTween.Hash(iT.RotateAdd.delay, 0.05f, iT.RotateAdd.time, 0.05f, iT.RotateAdd.x, 20,
           iT.RotateAdd.easetype, iTween.EaseType.linear));
    }

    #endregion

    #region Animation Event

    /// <summary>
    /// 攻击动画时间点
    /// 发出攻击
    /// </summary>
    private void Attacked()
    {
        //Debug.Log(this.name + " 发出攻击");
        foreach (ActorBevBase actorBev in this.m_TargetList)
        {
            actorBev.BeAttacked();
        }
    }
    /// <summary>
    /// 蓄力动画时间点
    /// </summary>
    private void Charge()
    {
        //Debug.Log(this.name + " Charge");
    }

    #endregion

}
