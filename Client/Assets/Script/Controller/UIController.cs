using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Linq;
using System.IO;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }
    public Camera MainCamera { get { return null; } }
    public Transform TopPanel;

    private Dictionary<UIPanelType,PanelBase> m_OpenedPanelDic = new Dictionary<UIPanelType, PanelBase>();
    private bool m_BackupUICameraStatus;
    private LoadingComponent m_LoadingComp;

    #region MonoBehaviour methods

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            this.ShowAlert(LocalizationUtils.GetText("Alert.Label.ExitGame"), AlertOptionType.Sure_Cancel, (result) => { if (result) Application.Quit(); });
        }
    }

    #endregion

    #region private methods

  
    private int GetTopDepth()
    {
        int topDepth = int.MinValue;
        foreach (KeyValuePair<UIPanelType,PanelBase> kv in this.m_OpenedPanelDic)
        {
            int depth = kv.Value.GetTopDepth();
            if (depth > topDepth)
            {
                topDepth = depth;
            }
        }
        return topDepth;
    }

    #endregion

    public PanelBase GetOpenedPanelByType(UIPanelType type)
    {
        return (this.m_OpenedPanelDic.ContainsKey(type))? this.m_OpenedPanelDic[type] : null;
    }

    #region Alert

    public void ShowCoding()
    {
        this.ShowTip(LocalizationUtils.GetText("Alert.Label.Coding"));
    }

    public void ShowDebug(string debug)
    {
        this.ShowTip(debug);
    }

    public void ShowAlert(string alert, AlertOptionType options, Action<bool> resultEvent)
    {
        this.RemoveAllTip();
        this.HideLoading();
        AlertParam alertParam = new AlertParam() { Alert = alert,Option = options };
        alertParam.ResultEvent += resultEvent;
        this.OpenPanel(UIPanelType.AlertPopupPanel, new PanelParam() { Alert = alertParam });
    }

    public void ShowIngotInsufficientAlert(UIPanelType backPanel)
    {
        this.RemoveAllTip();
        this.HideLoading();
        AlertParam alertParam = new AlertParam() { Alert = LocalizationUtils.GetText("Alert.Label.IngotInsufficient"), Option = AlertOptionType.Sure_Cancel };
        alertParam.ResultEvent += (result)=>
        {
            if (result)
            {
                this.TryClosePanel(backPanel);
                this.OpenPanel(UIPanelType.MallPanel);
            }
        };
        this.OpenPanel(UIPanelType.AlertPopupPanel, new PanelParam() { Alert = alertParam });
    }

    private void ShowTip(string tip)
    {
        this.RemoveAllTip();
        // 获取浮动提示框
        TipComponent tipComp = PoolMgr.Instance.GetComponent<TipComponent>(AssetPathConst.Component_Common + AssetNameConst.Tip);
        if (tipComp != null)
        {
            tipComp.MyTransform.SetParent(this.TopPanel);
            tipComp.Show(tip);
        }
    }

    public void RemoveAllTip()
    {
        TipComponent[] tips = this.GetComponentsInChildren<TipComponent>(true);
        if (tips != null)
        {
            foreach (TipComponent tip in tips)
            {
                tip.Hide();
            }
        }
    }

    #endregion

    #region OpenPanel

    public void OpenPanel(UIPanelType type)
    {
        this.OpenPanel(type, null, UIPanelType.None);
    }

    public void OpenPanel(UIPanelType type, PanelParam panelParam)
    {
        this.OpenPanel(type, panelParam, UIPanelType.None);
    }

    public void OpenPanel(UIPanelType type, UIPanelType backPanel)
    {
        this.OpenPanel(type, null, backPanel);
    }

    public void OpenPanel(UIPanelType type, PanelParam panelParam, UIPanelType backPanel)
    {
        PanelBase panel = null;
        int newDepth = 0;
        if (this.m_OpenedPanelDic.ContainsKey(type))
        {
            panel = this.m_OpenedPanelDic[type];
            newDepth = panel.GetDepth();
        }
        else
        {
            panel= PoolMgr.Instance.GetPanel(type);
            if (panel == null)
            {
                Debug.Log("Panel is null " + type);
                return;
            }
            this.m_OpenedPanelDic.Add(type, panel);
            newDepth = this.GetTopDepth() + 2;//加2的原因是为了保险起见，因为有的时候只加1可能会出现面板重叠
        }
        panel.Open(newDepth, panelParam, backPanel);
    }

    #endregion

    #region ClosePanel

    public void ClosePanel(UIPanelType type)
    {
        this.m_OpenedPanelDic[type].Close();
        this.m_OpenedPanelDic.Remove(type);
    }

    public void TryClosePanel(UIPanelType type)
    {
        if (this.m_OpenedPanelDic.ContainsKey(type))
        {
            this.ClosePanel(type);
        }
        else
        {
            Debug.Log("这个提示可以不用管 Close panel failed  " + type + " is not exist");
        }
    }

    public void CloseAllOpendPanel()
    {
        if (this.m_OpenedPanelDic != null && this.m_OpenedPanelDic.Count > 0)
        {
            foreach (KeyValuePair<UIPanelType, PanelBase> kv in this.m_OpenedPanelDic)
            {
                kv.Value.Dispose();
            }
            this.m_OpenedPanelDic.Clear();
        }
    }


    #endregion

    #region Loading 

    public void ShowLoading()
    {
        if (this.m_LoadingComp == null)
        {
            this.EnableLoading(true);
        }
    }

    public void HideLoading()
    {
        if (this.m_LoadingComp != null)
        {
            this.EnableLoading(false);
        }
    }

    private void EnableLoading(bool isEnable)
    {
        if (isEnable)
        {
            this.m_BackupUICameraStatus = true;
            this.DisableUICamera();
            // 获取加载图标
            this.m_LoadingComp = PoolMgr.Instance.GetComponent<LoadingComponent>(AssetPathConst.Component_Common + AssetNameConst.Loading);
           this.m_LoadingComp.MyTransform.SetParent(this.TopPanel);
            this.m_LoadingComp.MyTransform.localPosition = Vector3.zero;
        }
        else
        {
            this.SetUICamera(this.m_BackupUICameraStatus);
            this.m_LoadingComp.Dispose();
            this.m_LoadingComp = null;
        }
    }

    #endregion

    #region UICamera

    public void EnableUICamera()
    {
        this.SetUICamera(true);
    }

    public void DisableUICamera()
    {
        this.SetUICamera(false);
    }

    private void SetUICamera(bool isEnable)
    {
    }

    #endregion

    #region NotifyPanel 

    public void NotifyPanel(UIPanelType type, string methodName, object param = null)
    {
        PanelBase panel = this.GetOpenedPanelByType(type);
        if (panel != null)
        {
            panel.SendMessage(methodName, param,SendMessageOptions.DontRequireReceiver);
        }
    }

    #endregion
}

