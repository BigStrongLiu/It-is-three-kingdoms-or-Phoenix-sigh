using UnityEngine;
using System.Collections.Generic;

public class FightMgr : MonoBehaviour
{
    public Transform EnemyParent;
    public Transform PlayerParent;

    private FightGridComponent m_GridComp;
    private Dictionary<GridData,EnemyBev> m_EnemyBattleArray;
    private Dictionary<GridData,PlayerBev> m_PlayerBattleArray;

    #region MonoBehaviour methods

    void Awake()
    {
        this.m_GridComp = this.GetComponent<FightGridComponent>();
    }

    void Start()
    {
        this.InitPlayer();
        this.InitEnemy();
    }

    #endregion

    #region private methods
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
            this.m_PlayerBattleArray.Add(gridData, playerBev);
        }
    }

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
            this.m_EnemyBattleArray.Add(gridData,enemyBev);
        }
    }

    private T InstantiateActor<T>(int id) where T : ActorBevBase
    {
        ActorData actorData = LogicController.Instance.Actor.GetActorDataByID(id);
        GameObject actor = PoolMgr.Instance.GetModel(AssetPath.Actor + actorData.AssetName);
        TransUtils.ChangeLayer(actor.transform, LayerConst.Actor);
        return actor.AddComponent<T>();
    }

   
    #endregion
}
