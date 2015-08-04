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

    public ActorModule Actor { get; private set; }
    public LevelModule Level { get; private set; }

    public LogicController()
    {
        this.Actor = new ActorModule();
        this.Level = new LevelModule();
    }
}
