public class ActorLogicData
{
    public long UID { get; private set; }
    public int ID { get { return this.m_ActorData.ID; } }
    public string Name { get { return this.m_ActorData.Name; } }
    public string Description { get { return this.m_ActorData.Description; } }
    public string Icon { get { return this.m_ActorData.Icon; } }
    public string AssetName { get { return this.m_ActorData.AssetName; } }
    public float BaseAttackSpeed { get { return this.m_ActorData.BaseAttackSpeed; } }

    private ActorData m_ActorData;

    public ActorLogicData(long uid, ActorData heroData)
    {
        this.UID = uid;
        this.m_ActorData = heroData;
    }

    #region public methods

    public float GetAttackSpeed()
    {
        return this.BaseAttackSpeed;
    }

    public float GetFightingPower()
    {
        return 1;
    }

    #endregion
}