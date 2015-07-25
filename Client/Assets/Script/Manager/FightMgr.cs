using UnityEngine;

public class FightMgr : MonoBehaviour
{

    private FightGridComponent m_GridComp;

    #region MonoBehaviour methods

    void Awake()
    {
        this.m_GridComp = this.GetComponent<FightGridComponent>();
    }

    #endregion
}
