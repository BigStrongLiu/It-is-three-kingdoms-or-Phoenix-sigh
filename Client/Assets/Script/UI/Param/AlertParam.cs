using System;

public class AlertParam
{
    public string Alert{ get; set; } 
    public AlertOptionType Option { get; set; }
    public event Action<bool> ResultEvent;

    public void ExecuteResultEvent(bool result)
    {
        if (this.ResultEvent != null) this.ResultEvent(result);
    }
}
