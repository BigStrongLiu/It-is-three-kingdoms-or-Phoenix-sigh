using UnityEngine;

public class TipComponent : ComponentBase
{
    #region public members

    #endregion

    #region private members

    private UIEffectBase m_Effect;

    #endregion

    #region override methods

    protected override void Awake()
    {
        base.Awake();
        this.m_Effect = this.GetComponent<UIEffectBase>();
    }

    public override void Show()
    {
        this.MyTransform.localPosition = Vector3.zero;
        this.MyTransform.localEulerAngles = Vector3.zero;
        this.MyTransform.localScale = Vector3.one * 0.5f;
        base.Show();
        this.m_Effect.Play();
    }

    public override void Hide()
    {
        this.m_Effect.Stop();
    }

    #endregion

    #region public methods

    public void Show(string tip)
    {
        this.Show();
    }

    #endregion

}
