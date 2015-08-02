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
    private Dictionary<GridData, EnemyBev> m_EnemyBattleArray;//敌人阵容
    private Dictionary<GridData, PlayerBev> m_PlayerBattleArray;//玩家阵容
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
        this.m_PlayerBattleArray = new Dictionary<GridData, PlayerBev>();
        foreach (KeyValuePair<byte, long> kv in LogicController.Instance.Actor.GetBattleArray())
        {
            GridData gridData = this.m_GridComp.ConvertIndexToGridData(kv.Key);
            PlayerBev playerBev = this.InstantiateActor<PlayerBev>(LogicController.Instance.Actor.GetActorLogicDataByUID(kv.Value).ID);
            playerBev.transform.SetParent(this.PlayerParent);
            playerBev.transform.position = this.m_GridComp.ConvertGridToPosition(gridData, ActorType.Player);
            playerBev.transform.localRotation = Quaternion.identity;
            playerBev.transform.localScale = Vector3.one;
            playerBev.InitPlayer(kv.Value);
            this.m_PlayerBattleArray.Add(gridData, playerBev);
        }
    }

    /// <summary>
    /// 初始化敌人
    /// </summary>
    private void InitEnemy()
    {
        this.m_EnemyBattleArray = new Dictionary<GridData, EnemyBev>();
        LevelLogicData levelLogicData = LogicController.Instance.Level.GetLevelLogicDataByID(1000);
        foreach (KeyValuePair<byte, int> kv in levelLogicData.BattleArray)
        {
            GridData gridData = this.m_GridComp.ConvertIndexToGridData(kv.Key);
            EnemyBev enemyBev = this.InstantiateActor<EnemyBev>(kv.Value);
            enemyBev.transform.SetParent(this.EnemyParent);
            enemyBev.transform.position = this.m_GridComp.ConvertGridToPosition(gridData, ActorType.Enemy);
            enemyBev.transform.localRotation = Quaternion.identity;
            enemyBev.transform.localScale = Vector3.one;
            enemyBev.InitEnemy(kv.Value);
            this.m_EnemyBattleArray.Add(gridData, enemyBev);
        }
    }

    /// <summary>
    /// 复制预物体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id"></param>
    /// <returns></returns>
    private T InstantiateActor<T>(int id) where T : ActorBevBase
    {
        ActorData actorData = LogicController.Instance.Actor.GetActorDataByID(id);
        GameObject actor = PoolMgr.Instance.GetModel(AssetPathConst.Actor + actorData.AssetName);
        TransUtils.ChangeLayer(actor.transform, LayerConst.Actor);
        return actor.AddComponent<T>();
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
        ActorBevBase player = this.GetNextMovesActor(this.m_PlayerBattleArray.Values.ToList());
        ActorBevBase enemy = this.GetNextMovesActor(this.m_EnemyBattleArray.Values.ToList());
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
    private ActorBevBase GetNextMovesActor<T>(List<T> actorList)where T :ActorBevBase
    {
        List<T> list = actorList.Where(a => !(a.IsDead ||a.IsMoves()))
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
        Debug.Log("第" + (this.m_RandCount + 1) +"回合");
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
        this.ResetActor(this.m_EnemyBattleArray.Values.ToList());
        this.ResetActor(this.m_PlayerBattleArray.Values.ToList());
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
            Debug.Log("出招 " + actor.name);
            actor.Moves();
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

    #endregion

    #region public methods

    public void CurMovesComplete()
    {
        this.NextActorMoves();
    }

    #endregion
}
