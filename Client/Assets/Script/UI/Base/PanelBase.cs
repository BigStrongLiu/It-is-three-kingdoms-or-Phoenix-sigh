using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
/*
Panel Open() 之前可能需要 InitPanel() 
Panel Close() 之前可能需要 Clear();
*/
public abstract class PanelBase : UIBase, IPanel
{
    public UIPanelType ThisPanel { get; private set; }
    protected UIPanelType backPanel { get; private set; }
    private PanelEffect m_PanelEffect;
    protected PanelParam panelParam;

    protected override void Awake()
    {
        this.m_PanelEffect = this.GetComponent<PanelEffect>();
        base.Awake();
    }

    protected override void AddEvent()
    {
        base.AddEvent();
        if (this.m_PanelEffect != null)
        {
            this.m_PanelEffect.EffectCompleteEvent += (isOpen) =>
            {
                if (isOpen) this.OpenComplete();
                else this.CloseComplete();
            };
        }
    }

    protected virtual void InitPanel() { }
    protected abstract void Open(PanelParam panelParam);
    protected virtual void InClose() { this.CloseThisPanel(); }
    protected virtual void OpenComplete()
    {
        UIController.Instance.EnableUICamera();
    }

    protected virtual void CloseComplete()
    {
        this.Dispose();
    }

    public override void Clear()
    {
        base.Clear();
        StopAllCoroutines();
        iTween.Stop(this.MyGameObject);
    }

    public virtual void Close()
    {
        //Debug.Log(this.name + " Close ");
        this.Clear();
        //在关闭面板时，禁用Collider,防止在播放动画的时候再次点到
        UIUtils.SetUIColliderEnable(this.MyTransform, false);
        StartCoroutine(this.PlayEffect(false));
    }


    #region private methods

    private IEnumerator PlayEffect(bool isOpen)
    {
        if (this.m_PanelEffect == null)
        {
            if (isOpen) this.OpenComplete();
            else this.CloseComplete();
        }
        else
        {
            if (isOpen)
            {
            }
            yield return null;
            this.m_PanelEffect.Play(isOpen);
        }
    }

    #endregion

    #region protected methods

    /// <summary>
    /// 设置当前面板
    /// </summary>
    /// <param name="type"></param>
    protected void SetThisPanel(UIPanelType type) { this.ThisPanel = type; }

    protected void SetBackPanel(UIPanelType type) { this.backPanel = type; }

    #endregion

    #region public methods

    public void Open(int depth, PanelParam panelParam, UIPanelType backPanel)
    {
        UIController.Instance.DisableUICamera();
        //Debug.Log("PanelBase Open");
        this.SetPanelDepth(depth);
        this.SetBackPanel(backPanel);
        this.InitPanel();
        this.Open(panelParam);
        StartCoroutine(this.PlayEffect(true));
        this.panelParam = panelParam;
    }

    /// <summary>
    /// 关闭当前面板
    /// </summary>
    public void CloseThisPanel()
    {
        if (this.ThisPanel == UIPanelType.None) { Debug.Log("this panel is none can't close!"); return; }
        UIController.Instance.ClosePanel(this.ThisPanel);
    }

  

    public void SetPanelDepth(int depth)
    {
        if (this.GetComponent<FixedPanelDepthTool>() != null)
        {
            return;
        }
    }

    public int GetTopDepth()
    {
        if (this.GetComponent<FixedPanelDepthTool>() != null)
        {
            return 0;
        }
        return 0;
    }

    public int GetDepth()
    {
        return 0;
    }

    #endregion

}
