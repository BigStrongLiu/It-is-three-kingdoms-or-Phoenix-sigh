using System.Collections.Generic;

public sealed class LevelModule
{
    private Dictionary<int, LevelLogicData> m_LevelDic;
    public LevelModule()
    {
        this.InitLevel();
    }

    #region private methods

    private void InitLevel()
    {
        Dictionary<byte,int> level0BattleArray = new Dictionary<byte,int>();
        level0BattleArray.Add(0,2000);
        level0BattleArray.Add(5,2001);
        level0BattleArray.Add(6,2002);
        level0BattleArray.Add(7,2003);
        level0BattleArray.Add(8,2004);
        level0BattleArray.Add(9,2005);
        LevelData levelData0 = new LevelData(1000, "关卡0","关卡0描述", "", "", level0BattleArray);
        LevelLogicData levelLogicData0 = new LevelLogicData(levelData0);
        this.m_LevelDic = new Dictionary<int, LevelLogicData>();
        this.m_LevelDic.Add(1000,levelLogicData0);
    }

    #endregion

    #region public methods

    public LevelLogicData GetLevelLogicDataByID(int id)
    {
        return this.m_LevelDic.ContainsKey(id) ? this.m_LevelDic[id] : null;
    }


    #endregion
}
