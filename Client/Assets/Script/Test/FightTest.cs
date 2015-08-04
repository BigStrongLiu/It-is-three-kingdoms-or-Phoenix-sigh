using UnityEngine;
public class FightTest : MonoBehaviour
{
    #region MonoBehaviour methods

    void Start()
    {
        LoadLevelController.Instance.LoadLevel(GameSceneType.PVEScene);
    }

    #endregion
}
