public class ActorData
{
    public int ID { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Icon { get; private set; }
    public string AssetName { get; private set; }
    public float BaseAttackSpeed { get; private set; }
    public ActorData(int id, string name, string description, string icon, string assetName,float baseAttackSpeed)
    {
        this.ID = id;
        this.Name = name;
        this.Description = description;
        this.Icon = icon;
        this.AssetName = assetName;
        this.BaseAttackSpeed = baseAttackSpeed;
    }
}
