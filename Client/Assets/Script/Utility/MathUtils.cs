using UnityEngine;

public static class MathUtils
{
    /// <summary>
    /// 根据摄像机的旋转计算向量
    /// </summary>
    /// <param name="cam"></param>
    /// <param name="vect"></param>
    /// <returns></returns>
    public static Vector3 GetVectorByCamera(Camera cam, Vector2 vect)
    {
        float cosX = Mathf.Cos(Mathf.Deg2Rad * cam.transform.localEulerAngles.x);
        float sinX = Mathf.Sin(Mathf.Deg2Rad * cam.transform.localEulerAngles.x);
        float cosY = Mathf.Cos(Mathf.Deg2Rad * cam.transform.localEulerAngles.y);
        float sinY = Mathf.Sin(Mathf.Deg2Rad * cam.transform.localEulerAngles.y);
        return new Vector3(vect.x * cosY + vect.y * sinY, 0, -vect.x * cosX * sinY + vect.y * cosY * sinX);
    }

    public static bool Contains(this Rect rect, Rect otherRect)
    {
        Vector2 topLeftPoint = new Vector2(otherRect.xMin, otherRect.yMin);
        Vector2 bottomRightPoint = new Vector2(otherRect.xMax, otherRect.yMax);
        //Debug.Log("Rect " + new Vector2(rect.xMax, rect.yMax) + " topLeftPoint " + topLeftPoint + "  bottomRightPoint " + bottomRightPoint);
        return rect.Contains(topLeftPoint) && rect.Contains(bottomRightPoint);
    }

    public static bool IsStartPositionInRect(this Gesture gesture, Rect rect)
    {
        return rect.Contains(gesture.startPosition.GetGUIPosition());
    }

    public static bool IsCurrentPositionInRect(this Gesture gesture, Rect rect)
    {
        return rect.Contains(gesture.position.GetGUIPosition());
    }

    public static bool IsStartPositionAndCurrentPositionInRect(this Gesture gesture, Rect rect)
    {
        return gesture.IsStartPositionInRect(rect) && gesture.IsCurrentPositionInRect(rect);
    }

#if !UNITY_EDITOR
    private static Vector2? scaleVect = null;
#endif
    /// <summary>
    /// 获取GUI屏幕缩放比
    /// </summary>
    /// <returns></returns>
    public static Vector2 GetScreenScale()
    {
#if UNITY_EDITOR
        //Debug.Log(Screen.width + "+" + Screen.height  + "," +UIConst.ScreenWidth + "+" + UIConst.ScreenHeight);
        return new Vector2((float)Screen.width / (float)UIConst.ScreenWidth, (float)Screen.height / (float)UIConst.ScreenHeight);
#else
        if (!scaleVect.HasValue)
        {
            Vector2 vect = new Vector2((float)Screen.currentResolution.width/(float)UIConst.ScreenWidth, (float)Screen.currentResolution.height/(float)UIConst.ScreenHeight);
            scaleVect = vect;
        }
        return scaleVect.Value;
#endif
    }

    public static Rect AdjustScreen(this Rect rect)
    {
        Vector2 scaleVect = GetScreenScale();
        return new Rect(rect.x * scaleVect.x, rect.y * scaleVect.y, rect.width * scaleVect.x, rect.height * scaleVect.y);
    }

    public static Vector2 AdjustScreen(this Vector2 vect)
    {
        Vector2 scaleVect = GetScreenScale();
        return new Vector2(vect.x / scaleVect.x, vect.y / scaleVect.y);
    }

    public static Vector2 GetGUIPosition(this Vector2 position)
    {
        return new Vector2(position.x, Screen.height - position.y);
    }

    public static Rect GetTouchRect(Vector3 position, Vector2 touchCenter, Vector2 touchSize)
    {
        Vector2 screenPoint = UIController.Instance.MainCamera.WorldToScreenPoint(position);
        Vector2 GUIPoint = screenPoint.GetGUIPosition();
        Vector2 adjustPoint = GUIPoint.AdjustScreen();
        Rect rect = new Rect(adjustPoint.x + touchCenter.x - touchSize.x / 2, adjustPoint.y + touchCenter.y - touchSize.y / 2, touchSize.x, touchSize.y);
        return rect.AdjustScreen();
    }

}
