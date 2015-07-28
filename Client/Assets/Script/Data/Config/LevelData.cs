using System.Collections.Generic;

public class LevelData
{
    public int ID { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Icon { get; private set; }
    public string AssetName { get; private set; }
    public Dictionary<byte,int> BattleArray { get; private set; }
    public LevelData(int id, string name, string description, string icon, string assetName,Dictionary<byte,int> enemyBattleArray)
    {
        this.ID = id;
        this.Name = name;
        this.Description = description;
        this.Icon = icon;
        this.AssetName = assetName;
        this.BattleArray = enemyBattleArray;
    }
}
