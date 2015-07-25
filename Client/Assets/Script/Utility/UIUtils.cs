using UnityEngine;
using System;
using System.Collections.Generic;

public static class UIUtils
{
    public static void SetUIColliderEnable(Transform trans, bool isEnable)
    {
        if (trans != null)
        {
            Transform[] childrens = trans.GetComponentsInChildren<Transform>(true);
            foreach (Transform children in childrens)
            {
                TransUtils.EnableCollider(children, isEnable);
            }
        }
    }


    /// <summary>
    /// 适配碰撞器
    /// </summary>
    public static void AdjustCollider(Transform trans, Vector2 colliderScale)
    {
        BoxCollider2D col = trans.GetComponent<BoxCollider2D>();
        if (col == null)
        {
            col = trans.gameObject.AddComponent<BoxCollider2D>();
            if (col == null)
            {
                Debug.LogWarning("Add Collider Fail!!!");
                return;
            }
        }
    }

    public static void SetGray(Transform trans, bool isSetGray)
    {

    }
}
