using UnityEngine;

/*
 *游戏UI的基类

 *使用：
    *模板方法模式
    *组合模式
    *外观模式
    *中介者模式
 */

//尽量保持基类作为一个"轻"类，不要把太多的逻辑和状态放在类里。
public abstract class UIBase : MonoBehaviour,IUI
{
    public bool IsDisposed { get; private set; }

    //Lazy Initial
    private Transform m_Transform;
    public Transform MyTransform { get { if (this.m_Transform == null) this.m_Transform = this.transform; return this.m_Transform; } }

    private GameObject m_GameObject;
    public GameObject MyGameObject { get { if (this.m_GameObject == null) this.m_GameObject = this.gameObject; return this.m_GameObject; } }

    #region virtual methods

    protected virtual void Awake()
    {
        this.AddEvent();
    }
    /// <summary>
    /// 添加监听器
    /// </summary>
    protected virtual void AddEvent()
    {
        //为按钮添加监听器的代码
        //Example : UIEventListener.Get(ExampleButton.gameObject).onClick += ExampleButtonClick;
        //      or: ExampleButton.ClickEvent += ExampleButtonClick;
    }

    /// <summary>
    /// 清理
    /// </summary>
    public virtual void Clear()
    {
        //清空数据的代码
        //Example：i=0;str = "";
    }

    public virtual void Dispose()
    {
        //Debug.Log(this.name + "  Dispose");
        this.IsDisposed = true;
        Destroy(this.MyGameObject);
    }


    #endregion
}
