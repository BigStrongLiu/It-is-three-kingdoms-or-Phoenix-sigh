using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;

public sealed class LoadLevelController : MonoBehaviour
{
    public static LoadLevelController Instance { get; private set; }
    public GameSceneType CurrentScene { get; private set; }

    #region MonoBehaviour methods

    void Awake()
    {
        Instance = this;
        this.CurrentScene = GameSceneType.LoginScene;
    }

    #endregion

    #region LoadLevel

    public void LoadLevel(GameSceneType level)
    {
        UIController.Instance.RemoveAllTip();
        UIController.Instance.CloseAllOpendPanel();
        StopAllCoroutines();
        StartCoroutine(this.LoadLevelInspector(level));
    }

    private IEnumerator<AsyncOperation> LoadLevelInspector(GameSceneType level)
    {
        AsyncOperation operation = Application.LoadLevelAsync((int)level);
        yield return operation;
        //Debug.Log("LoadLevelInspector : " + operation.isDone);

        if (operation.isDone)
        {
            GameSceneType lastLevel = this.CurrentScene;
            this.CurrentScene = level;
            this.LoadLevelComplete(lastLevel);
        }
    }

    private void LoadLevelComplete(GameSceneType lastLevel)
    {
        //Debug.Log("LoadLevelComplete : ");
        StopAllCoroutines();
        StartCoroutine(this.DelayOpenPanel(lastLevel));
    }

    private IEnumerator DelayOpenPanel(GameSceneType lastLevel)
    {
        yield return new WaitForSeconds(0.2f);
        switch (this.CurrentScene)
        {
            case GameSceneType.LoginScene:
                break;
            default:
                break;
        }
    }

    #endregion
}
