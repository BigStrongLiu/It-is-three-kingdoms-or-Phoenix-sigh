using System.Collections;
using UnityEngine;

public class LoadingComponent : ComponentBase
{
    public Transform LoadingTrans;
    [Range(1, 10)]
    public int RotateSpeed;

    #region MonoBehaviour methods

    void LateUpdate()
    {
            this.LoadingTrans.localEulerAngles += new Vector3(0, 0, -this.RotateSpeed);
    }

    #endregion

}