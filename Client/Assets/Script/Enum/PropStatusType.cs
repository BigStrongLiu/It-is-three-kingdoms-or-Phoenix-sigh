//注意：为了通用所有状态并不互斥
public enum PropStatusType : byte
{
    /// <summary>
    /// 正常
    /// </summary>
    Normal,

    /// <summary>
    /// 已占用
    /// </summary>
    Occupied,

    /// <summary>
    /// 锁定（未解锁）
    /// </summary>
    Locked,

    /// <summary>
    /// 选中
    /// </summary>
    Selected,

    /// <summary>
    /// 禁用
    /// </summary>
    Disable,

}

