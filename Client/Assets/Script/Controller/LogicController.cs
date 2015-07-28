public sealed class LogicController
    {
    public static LogicController Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = new LogicController();
            }
            return m_Instance;
        }
    }
    private static  LogicController m_Instance;

    public ActorMoudle Actor { get; private set; }
    public LevelModule Level { get; private set; }

    public LogicController()
    {
        this.Actor = new ActorMoudle();
        this.Level = new LevelModule();
    }
}
