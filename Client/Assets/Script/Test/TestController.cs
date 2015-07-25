using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 单元测试用
/// </summary>
public class TestController : MonoBehaviour
{
    #region static members

    public static TestController Instance { get; private set; }

    #endregion

    //private int m_LoopLimitTiems = 1000;

    void Awake()
    {
        Instance = this;
    }

    #region public methods

    #endregion
}
