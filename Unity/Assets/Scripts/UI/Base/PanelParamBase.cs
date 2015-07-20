public abstract class PanelParamBase
{
    public UIPanelType OpenPanel = UIPanelType.None;
    public UIPanelType BackPanel = UIPanelType.None;
    public PanelDepthType DepthType = PanelDepthType.Dynamic;
    public short? Depth { get; set; }
}
