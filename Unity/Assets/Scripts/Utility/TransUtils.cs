using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public sealed class TransUtils
{
    public static T FindChild<T>(T root, string name) where T : Component
    {
        foreach (T t in root.GetComponentsInChildren<T>())
        {
            if (t.name.Equals(name))
            {
                return t;
            }
        }
        return null;
    }

    public static void ChangeLayer(Transform trans, string layer)
    {
        foreach (Transform t in trans.GetComponentsInChildren<Transform>())
        {
            t.gameObject.layer = LayerMask.NameToLayer(layer);
        }
    }

    public static void ChangeLayer(Transform trans, int layer)
    {
        foreach (Transform t in trans.GetComponentsInChildren<Transform>())
        {
            t.gameObject.layer = layer;
        }
    }

    public static Transform InstantiateTransform(Transform prefab, Transform parent, string layer)
    {
        Transform clone = InstantiateTransform(prefab, parent);
        ChangeLayer(clone, layer);
        return clone;
    }

    public static Transform InstantiateTransform(Transform prefab, Transform parent)
    {
        if (prefab != null)
        {
            return InstantiateTransform(prefab, parent, prefab.localScale);
        }
        return null;
    }

    public static Transform InstantiateTransform(Transform prefab, Transform parent, Vector3 scale)
    {
        if (prefab != null)
        {
            Transform clone = UnityEngine.Object.Instantiate(prefab) as Transform;
            if (parent != null)
            {
                clone.parent = parent;
                clone.localPosition = Vector3.zero;
                clone.localRotation = Quaternion.identity;
                clone.localScale = scale;
                ChangeLayer(clone, parent.gameObject.layer);
            }
            return clone;
        }
        return null;
    }

    public static void DestroyChildren(Transform root)
    {
        if (root != null)
        {
            if (root.childCount > 0)
            {
                for (int i = 0; i < root.childCount; i++)
                {
                    UnityEngine.Object.DestroyImmediate(root.GetChild(i).gameObject);
                }
            }
        }
    }

    public static T[] GetComponentsInShallowChilden<T>(Transform parent, bool includeInactive = true) where T : Component
    {
        if (parent == null || parent.childCount == 0)
        {
            return null;
        }
        List<T> compList = new List<T>();
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (child.gameObject.activeSelf)
            {
                T comp = child.GetComponent<T>();
                if (comp != null)
                {
                    compList.Add(comp);
                }
            }
        }
        return compList.ToArray();
    }

    private static Transform InstantiateAndRemoveBehaviour<T>(Transform prefab) where T : Behaviour
    {
        T prefabT = prefab.GetComponent<T>();
        bool backup = false;
        if (prefabT != null && prefabT.enabled)
        {
            backup = true;
            prefabT.enabled = false;
        }
        Transform clone = UnityEngine.Object.Instantiate(prefab) as Transform;
        if (prefabT != null)
        {
            UnityEngine.Object.DestroyImmediate(clone.GetComponent<T>());
            if (backup)
            {
                prefabT.enabled = true;
            }
        }
        return clone;
    }

    public static Bounds GetTransformBounds(Transform root,bool includeInactive)
    {
        Renderer[] renderers = root.GetComponentsInChildren<Renderer>(includeInactive);
        if (renderers == null || renderers.Length == 0)
        {
            return new Bounds();
        }
        Bounds combinedBounds = renderers[0].bounds;
        for (int i = 1; i < renderers.Length; i++)
        {
            combinedBounds.Encapsulate(renderers[i].bounds);

        }
        return combinedBounds;
    }

    public static void EnableCollider(Transform trans, bool isEnable)
    {
        if (trans != null)
        {
            BoxCollider2D boxCollider2D = trans.GetComponent<BoxCollider2D>();
            if (boxCollider2D != null)
            {
                boxCollider2D.enabled = isEnable;
            }
            Collider collider = trans.GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = isEnable;
            }
        }
    }
}
