using System.Collections.Generic;

/// <summary>
/// 观察者模式
/// </summary>
public sealed class TouchController/*考虑要不要删*//* 考虑好了，不删。要使用TurnOn功能 、2015.01.16*/
{
    #region Instance

    private static TouchController m_Instance;
    public static TouchController Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = new TouchController();
            }
            return m_Instance;
        }
    }

    #endregion

    #region HandlerList

    private List<EasyTouch.SwipeStartHandler> m_SwipeStartList;
    private List<EasyTouch.SwipeHandler> m_SwipeList;
    private List<EasyTouch.SwipeEndHandler> m_SwipeEndList;
    private List<EasyTouch.SimpleTapHandler> m_SimpleTapList;
    private List<EasyTouch.LongTapStartHandler> m_LongTapStartList;
    private List<EasyTouch.LongTapHandler> m_LongTapList;
    private List<EasyTouch.LongTapEndHandler> m_LongTapEndList;
    private List<EasyTouch.TouchDownHandler> m_TouchDownList;
    private List<EasyTouch.TouchUpHandler> m_TouchUpList;
    private List<EasyTouch.TwistHandler> m_TwistList;
    private List<EasyTouch.PinchInHandler> m_PinchInList;
    private List<EasyTouch.PinchOutHandler> m_PinchOutList;
    private List<EasyTouch.PinchEndHandler> m_PinchEndList;
    private List<EasyTouch.DragStartHandler> m_DragStartList;
    private List<EasyTouch.DragHandler> m_DragList;
    private List<EasyTouch.DragEndHandler> m_DragEndList;
    private List<EasyTouch.TouchStart2FingersHandler> m_TouchStart2FingersList;
    private List<EasyTouch.TouchStartHandler> m_TouchStartList;
    private List<EasyTouch.TouchUp2FingersHandler> m_TouchUp2FingersList;

    #endregion

    /// <summary>
    /// 是否开启
    /// </summary>
    private bool m_IsTurnOn = true;

    public TouchController()
    {
        this.m_SwipeStartList = new List<EasyTouch.SwipeStartHandler>();
        this.m_SwipeList = new List<EasyTouch.SwipeHandler>();
        this.m_SwipeEndList = new List<EasyTouch.SwipeEndHandler>();
        this.m_SimpleTapList = new List<EasyTouch.SimpleTapHandler>();
        this.m_LongTapStartList = new List<EasyTouch.LongTapStartHandler>();
        this.m_LongTapList = new List<EasyTouch.LongTapHandler>();
        this.m_LongTapEndList = new List<EasyTouch.LongTapEndHandler>();
        this.m_TouchDownList = new List<EasyTouch.TouchDownHandler>();
        this.m_TouchUpList = new List<EasyTouch.TouchUpHandler>();
        this.m_TwistList = new List<EasyTouch.TwistHandler>();
        this.m_PinchInList = new List<EasyTouch.PinchInHandler>();
        this.m_PinchOutList = new List<EasyTouch.PinchOutHandler>();
        this.m_DragStartList = new List<EasyTouch.DragStartHandler>();
        this.m_DragList = new List<EasyTouch.DragHandler>();
        this.m_DragEndList = new List<EasyTouch.DragEndHandler>();
        this.m_TouchStart2FingersList = new List<EasyTouch.TouchStart2FingersHandler>();
        this.m_TouchStartList = new List<EasyTouch.TouchStartHandler>();
        this.m_PinchEndList = new List<EasyTouch.PinchEndHandler>();
        this.m_TouchUp2FingersList = new List<EasyTouch.TouchUp2FingersHandler>();
        this.RegisterTouch();
    }

    #region private methods

    /// <summary>
    /// 注册事件
    /// </summary>
    private void RegisterTouch()
    {
        EasyTouch.On_SwipeStart += this.On_SwipeStart;
        EasyTouch.On_Swipe += this.On_Swipe;
        EasyTouch.On_SwipeEnd += this.On_SwipeEnd;
        EasyTouch.On_SimpleTap += this.On_SimpleTap;
        EasyTouch.On_LongTapStart += this.On_LongTapStart;
        EasyTouch.On_LongTap += this.On_LongTap;
        EasyTouch.On_LongTapEnd += this.On_LongTapEnd;
        EasyTouch.On_TouchDown += this.On_TouchDown;
        EasyTouch.On_TouchUp += this.On_TouchUp;
        EasyTouch.On_Twist += this.On_Twist;
        EasyTouch.On_PinchIn += this.On_PinchIn;
        EasyTouch.On_PinchOut += this.On_PinchOut;
        EasyTouch.On_DragStart += this.On_DragStart;
        EasyTouch.On_Drag += this.On_Drag;
        EasyTouch.On_DragEnd += this.On_DragEnd;
        EasyTouch.On_TouchStart2Fingers += this.On_TouchStart2Fingers;
        EasyTouch.On_TouchStart += this.On_TouchStart;
        EasyTouch.On_PinchEnd += this.On_PinchEnd;
        EasyTouch.On_TouchUp2Fingers += this.On_TouchUp2Fingers;
    }

    /// <summary>
    /// 移除事件
    /// </summary>
    private void RemoveTouch()
    {
        EasyTouch.On_SwipeStart -= this.On_SwipeStart;
        EasyTouch.On_Swipe -= this.On_Swipe;
        EasyTouch.On_SwipeEnd -= this.On_SwipeEnd;
        EasyTouch.On_SimpleTap -= this.On_SimpleTap;
        EasyTouch.On_LongTapStart -= this.On_LongTapStart;
        EasyTouch.On_LongTap -= this.On_LongTap;
        EasyTouch.On_LongTapEnd -= this.On_LongTapEnd;
        EasyTouch.On_TouchDown -= this.On_TouchDown;
        EasyTouch.On_TouchUp -= this.On_TouchUp;
        EasyTouch.On_Twist -= this.On_Twist;
        EasyTouch.On_PinchIn -= this.On_PinchIn;
        EasyTouch.On_PinchOut -= this.On_PinchOut;
        EasyTouch.On_DragStart -= this.On_DragStart;
        EasyTouch.On_Drag -= this.On_Drag;
        EasyTouch.On_DragEnd -= this.On_DragEnd;
        EasyTouch.On_TouchStart2Fingers -= this.On_TouchStart2Fingers;
        EasyTouch.On_TouchStart -= this.On_TouchStart;
        EasyTouch.On_TouchUp2Fingers -= this.On_TouchUp2Fingers;
    }

    #endregion

    #region EasyTouch

    private void On_SwipeStart(Gesture gesture)
    {
        if (this.m_IsTurnOn)
            this.m_SwipeStartList.ForEach(a => { if (a != null) a(gesture); });
    }

    private void On_Swipe(Gesture gesture)
    {
        if (this.m_IsTurnOn)
            this.m_SwipeList.ForEach(a => { if (a != null) a(gesture); });
    }

    private void On_SwipeEnd(Gesture gesture)
    {
        if (this.m_IsTurnOn)
            this.m_SwipeEndList.ForEach(a => { if (a != null) a(gesture); });
    }

    private void On_SimpleTap(Gesture gesture)
    {
        if (this.m_IsTurnOn)
            this.m_SimpleTapList.ForEach(a => { if (a != null) a(gesture); });
    }

    private void On_LongTapStart(Gesture gesture)
    {
        if (this.m_IsTurnOn)
            this.m_LongTapStartList.ForEach(a => { if (a != null) a(gesture); });
    }

    private void On_LongTap(Gesture gesture)
    {
        if (this.m_IsTurnOn)
            this.m_LongTapList.ForEach(a => { if (a != null) a(gesture); });
    }

    private void On_LongTapEnd(Gesture gesture)
    {
        if (this.m_IsTurnOn)
            this.m_LongTapEndList.ForEach(a => { if (a != null) a(gesture); });
    }

    private void On_TouchDown(Gesture gesture)
    {
        if (this.m_IsTurnOn)
            this.m_TouchDownList.ForEach(a => { if (a != null) a(gesture); });
    }

    private void On_TouchUp(Gesture gesture)
    {
        if (this.m_IsTurnOn)
            this.m_TouchUpList.ForEach(a => { if (a != null) a(gesture); });
    }

    private void On_Twist(Gesture gesture)
    {
        if (this.m_IsTurnOn)
            this.m_TwistList.ForEach(a => { if (a != null) a(gesture); });
    }

    private void On_PinchIn(Gesture gesture)
    {
        if (this.m_IsTurnOn)
            this.m_PinchInList.ForEach(a => { if (a != null) a(gesture); });
    }

    private void On_PinchOut(Gesture gesture)
    {
        if (this.m_IsTurnOn)
            this.m_PinchOutList.ForEach(a => { if (a != null) a(gesture); });
    }

    private void On_PinchEnd(Gesture gesture)
    {
        if (this.m_IsTurnOn)
            this.m_PinchEndList.ForEach(a => { if (a != null) a(gesture); });
    }

    private void On_DragStart(Gesture gesture)
    {
        if (this.m_IsTurnOn)
            this.m_DragStartList.ForEach(a => { if (a != null) a(gesture); });
    }

    private void On_Drag(Gesture gesture)
    {
        if (this.m_IsTurnOn)
            this.m_DragList.ForEach(a => { if (a != null) a(gesture); });
    }

    private void On_DragEnd(Gesture gesture)
    {
        if (this.m_IsTurnOn)
            this.m_DragEndList.ForEach(a => { if (a != null) a(gesture); });
    }

    private void On_TouchStart2Fingers(Gesture gesture)
    {
        if (this.m_IsTurnOn)
            this.m_TouchStart2FingersList.ForEach(a => { if (a != null) a(gesture); });
    }

    private void On_TouchStart(Gesture gesture)
    {
        if (this.m_IsTurnOn)
            this.m_TouchStartList.ForEach(a => { if (a != null) a(gesture); });
    }

    private void On_TouchUp2Fingers(Gesture gesture)
    {
        if (this.m_IsTurnOn)
            this.m_TouchUp2FingersList.ForEach(a => { if (a != null) a(gesture); });
    }

    #endregion

    #region public methods

    public void RegisterSwipeStart(EasyTouch.SwipeStartHandler handler)
    {
        if (handler != null && !this.m_SwipeStartList.Contains(handler))
        {
            this.m_SwipeStartList.Add(handler);
        }
    }

    public void RegisterSwipe(EasyTouch.SwipeHandler handler)
    {
        if (handler != null && !this.m_SwipeList.Contains(handler))
        {
            this.m_SwipeList.Add(handler);
        }
    }

    public void RegisterSwipeEnd(EasyTouch.SwipeEndHandler handler)
    {
        if (handler != null && !this.m_SwipeEndList.Contains(handler))
        {
            this.m_SwipeEndList.Add(handler);
        }
    }

    public void RegisterSimpleTap(EasyTouch.SimpleTapHandler handler)
    {
        if (handler != null && !this.m_SimpleTapList.Contains(handler))
        {
            this.m_SimpleTapList.Add(handler);
        }
    }

    public void RegisterLongTapStart(EasyTouch.LongTapStartHandler handler)
    {
        if (handler != null && !this.m_LongTapStartList.Contains(handler))
        {
            this.m_LongTapStartList.Add(handler);
        }
    }

    public void RegisterLongTap(EasyTouch.LongTapHandler handler)
    {
        if (handler != null && !this.m_LongTapList.Contains(handler))
        {
            this.m_LongTapList.Add(handler);
        }
    }

    public void RegisterLongTapEnd(EasyTouch.LongTapEndHandler handler)
    {
        if (handler != null && !this.m_LongTapEndList.Contains(handler))
        {
            this.m_LongTapEndList.Add(handler);
        }
    }


    public void RegisterTouchDown(EasyTouch.TouchDownHandler handler)
    {
        if (handler != null && !this.m_TouchDownList.Contains(handler))
        {
            this.m_TouchDownList.Add(handler);
        }
    }

    public void RegisterTouchUp(EasyTouch.TouchUpHandler handler)
    {
        if (handler != null && !this.m_TouchUpList.Contains(handler))
        {
            this.m_TouchUpList.Add(handler);
        }
    }

    public void RegisterTwist(EasyTouch.TwistHandler handler)
    {
        if (handler != null && !this.m_TwistList.Contains(handler))
        {
            this.m_TwistList.Add(handler);
        }
    }

    public void RegisterPinchIn(EasyTouch.PinchInHandler handler)
    {
        if (handler != null && !this.m_PinchInList.Contains(handler))
        {
            this.m_PinchInList.Add(handler);
        }
    }

    public void RegisterPinchOut(EasyTouch.PinchOutHandler handler)
    {
        if (handler != null && !this.m_PinchOutList.Contains(handler))
        {
            this.m_PinchOutList.Add(handler);
        }
    }

    public void RegisterPinchEnd(EasyTouch.PinchEndHandler handler)
    {
        if (handler != null && !this.m_PinchEndList.Contains(handler))
        {
            this.m_PinchEndList.Add(handler);
        }
    }

    public void RegisterDragStart(EasyTouch.DragStartHandler handler)
    {
        if (handler != null && !this.m_DragStartList.Contains(handler))
        {
            this.m_DragStartList.Add(handler);
        }
    }

    public void RegisterDrag(EasyTouch.DragHandler handler)
    {
        if (handler != null && !this.m_DragList.Contains(handler))
        {
            this.m_DragList.Add(handler);
        }
    }

    public void RegisterDragEnd(EasyTouch.DragEndHandler handler)
    {
        if (handler != null && !this.m_DragEndList.Contains(handler))
        {
            this.m_DragEndList.Add(handler);
        }
    }

    public void RegisterTouchStart2Fingers(EasyTouch.TouchStart2FingersHandler handler)
    {
        if (handler != null && !this.m_TouchStart2FingersList.Contains(handler))
        {
            this.m_TouchStart2FingersList.Add(handler);
        }
    }

    public void RegisterTouchStart(EasyTouch.TouchStartHandler handler)
    {
        if (handler != null && !this.m_TouchStartList.Contains(handler))
        {
            this.m_TouchStartList.Add(handler);
        }
    }

    public void RegisterTouchUp2Fingers(EasyTouch.TouchUp2FingersHandler handler)
    {
        if (handler != null && !this.m_TouchUp2FingersList.Contains(handler))
        {
            this.m_TouchUp2FingersList.Add(handler);
        }
    }

    public void RemoveSwipeStart(EasyTouch.SwipeStartHandler handler)
    {
        if (handler != null && this.m_SwipeStartList.Contains(handler))
        {
            this.m_SwipeStartList.Remove(handler);
        }
    }

    public void RemoveSwipe(EasyTouch.SwipeHandler handler)
    {
        if (handler != null && this.m_SwipeList.Contains(handler))
        {
            this.m_SwipeList.Remove(handler);
        }
    }

    public void RemoveSwipeEnd(EasyTouch.SwipeEndHandler handler)
    {
        if (handler != null && this.m_SwipeEndList.Contains(handler))
        {
            this.m_SwipeEndList.Remove(handler);
        }
    }

    public void RemoveSimpleTap(EasyTouch.SimpleTapHandler handler)
    {
        if (handler != null && this.m_SimpleTapList.Contains(handler))
        {
            this.m_SimpleTapList.Remove(handler);
        }
    }

    public void RemoveLongTapStart(EasyTouch.LongTapStartHandler handler)
    {
        if (handler != null && this.m_LongTapStartList.Contains(handler))
        {
            this.m_LongTapStartList.Remove(handler);
        }
    }

    public void RemoveLongTap(EasyTouch.LongTapHandler handler)
    {
        if (handler != null && this.m_LongTapList.Contains(handler))
        {
            this.m_LongTapList.Remove(handler);
        }
    }

    public void RemoveLongTapEnd(EasyTouch.LongTapEndHandler handler)
    {
        if (handler != null && this.m_LongTapEndList.Contains(handler))
        {
            this.m_LongTapEndList.Remove(handler);
        }
    }

    public void RemoveTouchDown(EasyTouch.TouchDownHandler handler)
    {
        if (handler != null && this.m_TouchDownList.Contains(handler))
        {
            this.m_TouchDownList.Remove(handler);
        }
    }

    public void RemoveTouchUp(EasyTouch.TouchUpHandler handler)
    {
        if (handler != null && this.m_TouchUpList.Contains(handler))
        {
            this.m_TouchUpList.Remove(handler);
        }
    }

    public void RemoveTwist(EasyTouch.TwistHandler handler)
    {
        if (handler != null && this.m_TwistList.Contains(handler))
        {
            this.m_TwistList.Remove(handler);
        }
    }

    public void RemovePinchIn(EasyTouch.PinchInHandler handler)
    {
        if (handler != null && this.m_PinchInList.Contains(handler))
        {
            this.m_PinchInList.Remove(handler);
        }
    }

    public void RemovePinchOut(EasyTouch.PinchOutHandler handler)
    {
        if (handler != null && this.m_PinchOutList.Contains(handler))
        {
            this.m_PinchOutList.Remove(handler);
        }
    }

    public void RemovePinchEnd(EasyTouch.PinchEndHandler handler)
    {
        if (handler != null && this.m_PinchEndList.Contains(handler))
        {
            this.m_PinchEndList.Remove(handler);
        }
    }

    public void RemoveDragStart(EasyTouch.DragStartHandler handler)
    {
        if (handler != null && this.m_DragStartList.Contains(handler))
        {
            this.m_DragStartList.Remove(handler);
        }
    }

    public void RemoveDrag(EasyTouch.DragHandler handler)
    {
        if (handler != null && this.m_DragList.Contains(handler))
        {
            this.m_DragList.Remove(handler);
        }
    }

    public void RemoveDragEnd(EasyTouch.DragEndHandler handler)
    {
        if (handler != null && this.m_DragEndList.Contains(handler))
        {
            this.m_DragEndList.Remove(handler);
        }
    }

    public void RemoveTouchStart2Fingers(EasyTouch.TouchStart2FingersHandler handler)
    {
        if (handler != null && this.m_TouchStart2FingersList.Contains(handler))
        {
            this.m_TouchStart2FingersList.Remove(handler);
        }
    }

    public void RemoveTouchStart(EasyTouch.TouchStartHandler handler)
    {
        if (handler != null && this.m_TouchStartList.Contains(handler))
        {
            this.m_TouchStartList.Remove(handler);
        }
    }

    public void RemoveTouchUp2Fingers(EasyTouch.TouchUp2FingersHandler handler)
    {
        if (handler != null && this.m_TouchUp2FingersList.Contains(handler))
        {
            this.m_TouchUp2FingersList.Remove(handler);
        }
    }

    #endregion

    #region public methods

    public void TurnOn()
    {
        this.m_IsTurnOn = true;
    }

    public void TurnOff()
    {
        this.m_IsTurnOn = false;
    }

    public bool IsTurnOn()
    {
        return this.m_IsTurnOn;
    }

    public void Dispose()
    {
        this.RemoveTouch();
        m_Instance = null;
    }

    #endregion
}
