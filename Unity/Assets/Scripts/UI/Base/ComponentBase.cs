using System.Collections;
using UnityEngine;

public abstract class ComponentBase : UIBase,IComponent
{
    private bool m_IsUpdated;
    public bool IsUpdated
    {
        get
        {
            return this.m_IsUpdated;
        }
        set
        {
            //Debug.Log(this.name +" set isupdated "+value);
            this.m_IsUpdated = value;
        }
    }
    public bool IsOpen { get { return this.MyGameObject.activeInHierarchy; } }//是否打开

    #region virtual methods

    public virtual void Show()
    {
        if (!this.IsInit)
            this.Init();
        this.MyGameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        this.MyGameObject.SetActive(false);
    }

    /// <summary>
    /// 调整碰撞器
    /// </summary>
    [ContextMenu("Adjust Collider")]
    public virtual void AdjustCollider()
    {
        this.AdjustCollider(Vector2.one);
    }

    #endregion

    public override void Clear()
    {
        this.IsUpdated = false;
        base.Clear();
    }

    #region public methods

    /// <summary>
    ///  根据指定比例调整碰撞器
    /// </summary>
    /// <param name="colliderScale"></param>
    public void AdjustCollider(Vector2 colliderScale)
    {
        UIUtils.AdjustCollider(this.MyTransform, colliderScale);
    }

    #endregion
}
