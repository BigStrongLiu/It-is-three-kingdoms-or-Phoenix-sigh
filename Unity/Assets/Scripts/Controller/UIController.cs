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
    private bool m_Dispose;

    #region MonoBehaviour methods

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AlertParam param = new AlertParam() { OpenPanel = UIPanelType.AlertPopupPanel, Alert = LocalizationUtils.GetText("Alert.Label.ExitGame"),Option = AlertOptionType.Sure_Cancel };
            param.ResultEvent += (result) => { if (result) Application.Quit(); };
            this.OpenPanel(param);
        }
    }

    void OnDestroy()
    {
        this.m_Dispose = true;
    }

    #endregion

    #region private methods

    private short GetTopDepth()
    {
        short topDepth = short.MinValue;
        foreach (KeyValuePair<UIPanelType,PanelBase> kv in this.m_OpenedPanelDic)
        {
            short depth = kv.Value.GetTopDepth();
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

    public void ShowTip(string tip)
    {
        if (string.IsNullOrEmpty(tip)) return;
        this.RemoveAllTip();
        // 获取浮动提示框
        TipComponent tipComp = UIPoolMgr.Instance.LoadComponent<TipComponent>(AssetPath.Component_Common + AssetName.Tip);
        tipComp.MyTransform.SetParent(this.TopPanel);
        tipComp.Show(tip);
    }

    public void ShowRMBInsufficient(UIPanelType backPanel)
    {
        this.RemoveAllTip();
        this.HideLoading();
        AlertParam alertParam = new AlertParam() { Alert = LocalizationUtils.GetText("Alert.Label.IngotInsufficient"), Option = AlertOptionType.Sure_Cancel };
        alertParam.ResultEvent += (result)=>
        {
            if (result)
            {
                this.ClosePanel(backPanel);
                this.OpenPanel(new MallParam { OpenPanel = UIPanelType.MallPanel,BackPanel = backPanel});
            }
        };
        this.OpenPanel(alertParam);
    }

    public void ShowReloginAlert(string alert)
    {
        if (this.m_Dispose) return;
        AlertParam param = new AlertParam() { OpenPanel = UIPanelType.AlertPopupPanel, Alert = alert, Option = AlertOptionType.Sure };
        param.ResultEvent += (result) => LoadLevelController.Instance.LoadLevel(GameSceneType.LoginScene);
        this.OpenPanel(param);
    }

    public void ShowReconnectAlert(string alert)
    {
        if (this.m_Dispose) return;
        AlertParam param = new AlertParam() { OpenPanel = UIPanelType.AlertPopupPanel, Alert = alert, Option = AlertOptionType.Sure };
        param.ResultEvent += (result) => LoadLevelController.Instance.LoadLevel(GameSceneType.LoginScene);
        this.OpenPanel(param);
    }

    public void RemoveAllTip()
    {
        TipComponent[] alerts = this.GetComponentsInChildren<TipComponent>(true);
        if (alerts != null)
        {
            for (int i = 0; i < alerts.Length; i++)
            {
                alerts[i].Hide();
            }
        }
    }

    #endregion

    #region OpenPanel

  

    public void OpenPanel(PanelParamBase param)
    {
        PanelBase panel = null;
        short depth = 0;
        if (param.DepthType == PanelDepthType.Fixed)
        {
            depth = param.Depth.Value;
        }
        if (this.m_OpenedPanelDic.ContainsKey(param.OpenPanel))
        {
            panel = this.m_OpenedPanelDic[param.OpenPanel];
            if (param.DepthType == PanelDepthType.Dynamic)
            {
                depth = panel.GetDepth();
            }
        }
        else
        {
            panel= UIPoolMgr.Instance.LoadPanel(param.OpenPanel);
            if (panel == null)
            {
                Debug.Log("Panel is null " + param.OpenPanel);
                return;
            }
            this.m_OpenedPanelDic.Add(param.OpenPanel, panel);
            if (param.DepthType == PanelDepthType.Dynamic)
            {
                depth = (short)(this.GetTopDepth() + 1);
            }
        }
        panel.Open(depth, param);
        StartCoroutine(this.CheckPanel());
    }

    #endregion

    #region ClosePanel

    public void ClosePanel(UIPanelType type)
    {
        this.m_OpenedPanelDic[type].Close();
        this.m_OpenedPanelDic.Remove(type);
        //this.CheckPanel();
        StartCoroutine(this.CheckPanel());
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
            this.m_BackupUICameraStatus = true;// this.UICam.enabled;
            this.DisableUICamera();
            // 获取加载图标
            this.m_LoadingComp = UIPoolMgr.Instance.LoadComponent<LoadingComponent>(AssetPath.Component_Common + AssetName.Loading);
           // this.m_LoadingComp.MyTransform.SetParent(this.TopPanel);
            this.m_LoadingComp.MyTransform.parent =this.TopPanel;
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

    #region MainCity

    private IEnumerator CheckPanel()
    {
        yield return null;
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

