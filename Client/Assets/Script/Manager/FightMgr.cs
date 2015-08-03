using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class FightMgr : MonoBehaviour
{
    public static FightMgr Instance { get; private set; }
    public Transform EnemyParent;
    public Transform PlayerParent;

    private FightGridComponent m_GridComp;//网格
    private Dictionary<ActorBevBase, GridData> m_EnemyBattleArray;//敌人阵容
    private Dictionary<ActorBevBase, GridData> m_PlayerBattleArray;//玩家阵容
    private byte m_RandCount;//回合数
    private bool m_GameOver;//是否游戏结束
    private ActorBevBase m_CurMovesActor;

    #region MonoBehaviour methods

    void Awake()
    {
        Instance = this;
        this.m_GridComp = this.GetComponent<FightGridComponent>();
    }

    void Start()
    {
        this.InitPlayer();
        this.InitEnemy();
        StartCoroutine(this.StartGame());
    }

    #endregion

    #region private methods

    /// <summary>
    /// 初始化玩家
    /// </summary>
    private void InitPlayer()
    {
        this.m_PlayerBattleArray = new Dictionary<ActorBevBase, GridData>();
        foreach (KeyValuePair<byte, long> kv in LogicController.Instance.Actor.GetBattleArray())
        {
            byte index = kv.Key;
            long uid = kv.Value;
            GridData gridData = this.m_GridComp.ConvertIndexToGridData(index);
            PlayerBev playerBev = this.InstantiateActor<PlayerBev>(index,LogicController.Instance.Actor.GetActorLogicDataByUID(uid).ID);
            playerBev.transform.SetParent(this.PlayerParent);
            playerBev.transform.position = this.m_GridComp.ConvertGridToPosition(gridData,ActorType.Player);
            playerBev.transform.localRotation = Quaternion.identity;
            playerBev.transform.localScale = Vector3.one;
            playerBev.InitPlayer(uid);
            this.m_PlayerBattleArray.Add(playerBev, gridData);
        }
    }

    /// <summary>
    /// 初始化敌人
    /// </summary>
    private void InitEnemy()
    {
        this.m_EnemyBattleArray = new Dictionary<ActorBevBase, GridData>();
        LevelLogicData levelLogicData = LogicController.Instance.Level.GetLevelLogicDataByID(1000);
        foreach (KeyValuePair<byte, int> kv in levelLogicData.BattleArray)
        {
            byte index = kv.Key;
            int id = kv.Value;
            GridData gridData = this.m_GridComp.ConvertIndexToGridData(index);
            EnemyBev enemyBev = this.InstantiateActor<EnemyBev>(index,id);
            enemyBev.transform.SetParent(this.EnemyParent);
            enemyBev.transform.position = this.m_GridComp.ConvertGridToPosition(gridData,ActorType.Enemy );
            enemyBev.transform.localRotation = Quaternion.identity;
            enemyBev.transform.localScale = Vector3.one;
            enemyBev.InitEnemy(id);
            this.m_EnemyBattleArray.Add(enemyBev, gridData);
        }
    }

    /// <summary>
    /// 复制预物体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id"></param>
    /// <returns></returns>
    private T InstantiateActor<T>(byte index,int id) where T : ActorBevBase
    {
        ActorData actorData = LogicController.Instance.Actor.GetActorDataByID(id);
        GameObject actor = PoolMgr.Instance.GetModel(AssetPathConst.Actor + actorData.AssetName);
        TransUtils.ChangeLayer(actor.transform, LayerConst.Actor);
        T bev = actor.AddComponent<T>();
        bev.Index = index;
        return bev;
    }

    /// <summary>
    /// 游戏进程
    /// </summary>
    /// <returns></returns>
    private IEnumerator Gaming()
    {
        while (!this.m_GameOver)
        {

            yield return null;
        }
    }

    /// <summary>
    /// 获取下一个出招的角色
    /// </summary>
    /// <returns></returns>
    private ActorBevBase GetNextMovesActor()
    {
        ActorBevBase player = this.GetNextMovesActor(this.m_PlayerBattleArray.Keys.ToList());
        ActorBevBase enemy = this.GetNextMovesActor(this.m_EnemyBattleArray.Keys.ToList());
        return this.GetNextMovesActor(player, enemy);
    }

    /// <summary>
    /// 比较玩家和敌人,得到下一个出招的角色
    /// </summary>
    /// <returns></returns>
    private ActorBevBase GetNextMovesActor(ActorBevBase player, ActorBevBase enemy)
    {
        if (player == null) return enemy;
        if (enemy == null) return player;
        List<ActorBevBase> actorList = new List<ActorBevBase>() { player, enemy };
        return this.GetNextMovesActor(actorList);
    }

    /// <summary>
    /// 计算玩家或角色得到最符合条件的下一个出招的角色
    /// </summary>
    /// <returns></returns>
    private ActorBevBase GetNextMovesActor<T>(List<T> actorList) where T : ActorBevBase
    {
        List<T> list = actorList.Where(a => !(a.IsDead || a.IsMoves()))
            .OrderByDescending(b => b.ActorLogicData.GetAttackSpeed())
            .ThenByDescending(c => c.ActorLogicData.GetFightingPower())
            .ToList();
        return list.FirstOrDefault();
    }

    /// <summary>
    /// 回合结束
    /// </summary>
    private void EndOfRound()
    {
        this.ResetActor();
        this.m_RandCount++;
        Debug.Log("第" + (this.m_RandCount + 1) + "回合");
        if (this.m_RandCount == FightConst.MaxRoundCount)
        {
            this.GameOver();
        }
        else
        {
            this.NextActorMoves();
        }
    }

    /// <summary>
    /// 游戏结束
    /// </summary>
    private void GameOver()
    {
        this.m_GameOver = true;
    }

    /// <summary>
    /// 重置敌人和玩家
    /// </summary>
    private void ResetActor()
    {
        this.ResetActor(this.m_EnemyBattleArray.Keys.ToList());
        this.ResetActor(this.m_PlayerBattleArray.Keys.ToList());
        this.m_CurMovesActor = null;
    }

    /// <summary>
    /// 重置角色的状态
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="actorList"></param>
    private void ResetActor<T>(List<T> actorList) where T : ActorBevBase
    {
        actorList.ForEach(a =>
        {
            if (!a.IsDead)
            {
                a.NextRound();
            }
        });
    }

    private void NextActorMoves()
    {
        ActorBevBase actor = this.GetNextMovesActor();
        if (actor == null)
        {
            //回合结束
            Debug.Log("回合结束");
            this.EndOfRound();
        }
        else
        {
            //出招
            this.ActorMoves(actor);
            this.m_CurMovesActor = actor;
        }
    }

    /// <summary>
    /// 开始游戏
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartGame()
    {
        yield return null;
        Debug.Log("第" + (this.m_RandCount + 1) + "回合");
        this.NextActorMoves();
    }

    private ActorBevBase GetTarget(ActorBevBase actorBev)
    {
        switch (actorBev.Type)
        {
            case ActorType.Enemy:
                return this.GetTarget(this.m_EnemyBattleArray[ actorBev], this.m_PlayerBattleArray);
            case ActorType.Player:
                return this.GetTarget(this.m_PlayerBattleArray[ actorBev], this.m_EnemyBattleArray);
        }
        return null;
    }

    private ActorBevBase GetTarget(GridData gridData,Dictionary<ActorBevBase,GridData> targetDic)
    {
        byte minDistance = this.m_GridComp.GetMaxMagnitudeDistance();
        byte minIndex = this.m_GridComp.GetMaxIndex();
        ActorBevBase target = null;
        foreach (KeyValuePair<ActorBevBase, GridData> kv in targetDic)
        {
            ActorBevBase acotrBev = kv.Key;
            byte distance = this.m_GridComp.CalculateMagnitudeDistance(gridData,kv.Value,true);
            if (distance < minDistance ||
               (distance == minDistance && acotrBev.Index < minIndex))
            {
                //Debug.Log("minDistance " + minDistance+ " distance " + distance+ "  minIndex " + minIndex + " acotrBev.Index " + acotrBev.Index +"  actorName "+ acotrBev.name);
                minIndex = acotrBev.Index;
                minDistance = distance;
                target = acotrBev;
            }
        }
        return target;
    }

    private void ActorMoves(ActorBevBase actorBev)
    {
        Debug.Log("出招 " + actorBev.name);
        ActorBevBase target = this.GetTarget(actorBev);
        if (target == null)
        {
            Debug.LogError("Target is null");
            return;
        }
        Vector3 position = this.m_GridComp.GetMovesPosition(target);
        actorBev.Moves(target, position);
    }

    #endregion

    #region public methods

    public void CurMovesComplete()
    {
        this.NextActorMoves();
    }

    #endregion
}
