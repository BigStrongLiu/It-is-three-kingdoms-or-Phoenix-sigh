using System.Collections.Generic;

public class LevelLogicData
{
    public int ID { get { return this.m_LevelData.ID; } }
    public string Name { get { return this.m_LevelData.Name; } }
    public string Description { get { return this.m_LevelData.Description; } }
    public string Icon { get { return this.m_LevelData.Icon; } }
    public string AssetName { get { return this.m_LevelData.AssetName; } }
    public Dictionary<byte,int> BattleArray { get { return this.m_LevelData.BattleArray; } }
    private LevelData m_LevelData;
    public LevelLogicData(LevelData levelData)
    {
        this.m_LevelData = levelData;
    }
}
