public interface IPanel
{
    void Open(int depth, PanelParam panelParam, UIPanelType backPanel);
    void Close();
}
