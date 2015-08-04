using System.Collections.Generic;
using UnityEngine;
using System;

/*
UI资源池
    简单工厂模式
    享元模式

    优先执行 script order -10
*/
public sealed class PoolMgr : MonoBehaviour
{
    public static PoolMgr Instance { get; private set; }
    private struct ModelInfo
    {
        public string Path;
        public GameObject GO;
    }
    private Dictionary<string, UnityEngine.Object> m_AssetDic = new Dictionary<string, UnityEngine.Object>();
    private Dictionary<UIPanelType, string> m_PanelAssetDic = new Dictionary<UIPanelType, string>();
    private Transform m_Transform;

    #region MonoBehaviour methods

    void Awake()
    {
        Instance = this;
        this.m_Transform = this.transform;
        this.RegisterPanel();
    }


#endregion

#region private methods

    private void RegisterPanel()
    {
        //Login
        //Main
        //Fight
        //Common
    }

    private void RegisterPanelForLogin(UIPanelType type)
    {
        this.ReigsterPanelAsset(type, AssetPathConst.Panel_Login + type);
    }

    private void RegisterPanelForMain(UIPanelType type)
    {
        this.ReigsterPanelAsset(type, AssetPathConst.Panel_Main + type);
    }

    private void RegisterPanelForFight(UIPanelType type)
    {
        this.ReigsterPanelAsset(type, AssetPathConst.Panel_Fight + type);
    }

    private void RegisterPanelForCommon(UIPanelType type)
    {
        this.ReigsterPanelAsset(type, AssetPathConst.Panel_Common + type);
    }

    private void ReigsterPanelAsset(UIPanelType type, string assetName)
    {
        if (this.m_PanelAssetDic.ContainsKey(type)) this.m_PanelAssetDic[type] = assetName;
        else this.m_PanelAssetDic.Add(type, assetName);
    }

    /// <summary>
    /// 获取资源
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private UnityEngine.Object Get(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            Debug.LogWarning("Path is empty");
            return null;
        }
        try
        {
            //Debug.Log(path);
            UnityEngine.Object obj;
            this.m_AssetDic.TryGetValue(path, out obj);
            if (obj == null)
            {
                obj = this.Load(path);
                if (obj != null)
                {
                    this.Add(path, obj);
                }
            }
            return obj;
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            return null;
        }
    }

    /// <summary>
    /// 添加资源
    /// </summary>
    /// <param name="path"></param>
    /// <param name="obj"></param>
    private void Add(string path, UnityEngine.Object obj)
    {
        if (this.m_AssetDic.ContainsKey(path)) this.m_AssetDic.Remove(path);
        this.m_AssetDic.Add(path, obj);
    }

    /// <summary>
    /// 加载资源
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private UnityEngine.Object Load(string path)
    {
        UnityEngine.Object obj = Resources.Load(path);
        if (obj == null) Debug.Log("Can't find the asset " + path);
        return obj;
    }

#endregion

#region public methods

    /// <summary>
    /// 获取图片
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public Texture2D GetTexture2D(string path)
    {
        return this.Get(path) as Texture2D;
    }

    /// <summary>
    /// 获取预物体
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public GameObject GetPrefab(string path)
    {
        UnityEngine.Object o = this.Get(path);
        if (o != null)
        {
            return o as GameObject;
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// 获取模型 
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public GameObject GetModel(string path)
    {
        GameObject model = TransUtils.InstantiateTransform(this.GetPrefab(path).transform, this.m_Transform).gameObject;
        Collider collider = model.GetComponent<Collider>();
        collider.enabled = true;
        return model;
    }

    /// <summary>
    /// 获取面板
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public PanelBase GetPanel(UIPanelType type)
    {
        if (type == UIPanelType.None || !this.m_PanelAssetDic.ContainsKey(type))
        {
            return null;
        }
        return TransUtils.InstantiateTransform(this.GetPrefab(this.m_PanelAssetDic[type]).transform, this.m_Transform).GetComponent<PanelBase>();
    }

    public T GetComponent<T>(string path) where T : ComponentBase
    {
        GameObject prefab = this.GetPrefab(path);
        return TransUtils.InstantiateTransform(prefab.transform, this.m_Transform, LayerMask.LayerToName(prefab.layer)).GetComponent<T>();
    }

    public Transform GetUIEffect(string effectName, Transform parent = null)
    {
        GameObject prefab = this.GetPrefab(AssetPathConst.Effect_UI + effectName);
        if (prefab == null) return null;
        UnityEngine.Object obj = Instantiate(prefab);
        Transform effect = (obj as GameObject).transform;
        if (parent != null)
        {
            effect.parent = parent;
            effect.localPosition = Vector3.zero;
            effect.localScale = Vector3.one;
        }
        return effect;
    }


    //********************************************割割割割-无耻的分割线-割割割割**************************************

    public void Recover(GameObject go)
    {
        if (go != null)
        {
            go.SetActive(false);
            go.transform.parent = this.m_Transform;
        }
    }
  

    #endregion

    private void PreInstantiatePanel()
    {
        //PanelBase panel = this.GetPanel(UIPanelType.ExamplePanel);
        //DestroyImmediate(panel.MyGameObject);
    }
}
