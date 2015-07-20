using System;
using UnityEngine;

public class PanelEffect : UIEffectBase
{
    public event Action<bool> EffectCompleteEvent;
    private float iTweenTime = 0.15f;

    private bool m_IsOut;

    void Awake()
    {
    }
    public override void Play() { }

    public override void Play(object obj)
    {
        iTween.Stop(this.MyGameObject);
        //this.MyTranform.localScale = Vector3.zero;
        this.m_IsOut = (bool)obj;
        if (this.m_IsOut)
        {
            this.ScaleOut();
        }
        else
        {
            this.ScaleIn();
        }
    }

    private void ScaleOut()
    {
        //Debug.Log(this.MyGameObject.name + " ScaleOut");

        this.MyTransform.localScale = Vector3.one * 0.8f;
        iTween.ScaleTo(this.MyGameObject, iTween.Hash(iT.ScaleTo.time, this.iTweenTime, iT.ScaleTo.scale, Vector3.one * 1.05f,
            iT.ScaleTo.easetype, iTween.EaseType.linear, "ignoretimescale", true,
            iT.ScaleTo.oncomplete, "ScaleOutComplete", iT.ScaleTo.oncompletetarget, this.MyGameObject));
        iTween.ValueTo(this.MyGameObject, iTween.Hash(iT.ValueTo.time, this.iTweenTime, iT.ValueTo.from, 0, iT.ValueTo.to, 1,
           "ignoretimescale", true,
           iT.ValueTo.onupdate, "UpdateAlpha", iT.ValueTo.onupdatetarget, this.MyGameObject));
   }

   private void ScaleIn()
   {
        //Debug.Log(this.MyGameObject.name + " ScaleIn");
        iTween.ScaleTo(this.MyGameObject, iTween.Hash(iT.ScaleTo.time, this.iTweenTime*2, iT.ScaleTo.scale, Vector3.one * 0.01f,
             iT.ScaleTo.easetype, iTween.EaseType.linear, "ignoretimescale", true,
             iT.ScaleTo.oncomplete, "EffectComplete", iT.ScaleTo.oncompletetarget, this.MyGameObject));

        iTween.ValueTo(this.MyGameObject, iTween.Hash(iT.ValueTo.time, this.iTweenTime, iT.ValueTo.from, 1, iT.ValueTo.to, 0,
            "ignoretimescale", true,
            iT.ValueTo.onupdate, "UpdateAlpha", iT.ValueTo.onupdatetarget, this.MyGameObject));
    }

    private void ScaleOutComplete()
    {
        iTween.ScaleTo(this.MyGameObject, iTween.Hash(iT.ScaleTo.time, this.iTweenTime, iT.ScaleTo.scale, Vector3.one,
            iT.ScaleTo.easetype, iTween.EaseType.linear, "ignoretimescale", true,
            iT.ScaleTo.oncomplete, "EffectComplete", iT.ScaleTo.oncompletetarget, this.MyGameObject));
    }

    private void EffectComplete()
    {
        this.EffectCompleteEvent(this.m_IsOut);
    }

    private void UpdateAlpha(float alpha)
    {
    }

}
