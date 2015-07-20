using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;

public sealed class LoadLevelController : MonoBehaviour
{
    public Texture2D LoadLevelTexture;
    public static LoadLevelController Instance { get; private set; }
    public GameSceneType CurrentScene { get; private set; }

    private bool m_LoadingLevel;

    #region MonoBehaviour methods

    void Awake()
    {
        Instance = this;
        this.CurrentScene = GameSceneType.LoginScene;
    }

    void OnGUI()
    {
        if (this.m_LoadingLevel && this.LoadLevelTexture != null)
        {
            Rect rect = new Rect(0, 0, Screen.width, Screen.height);
            GUI.DrawTexture(rect, this.LoadLevelTexture);
        }
    }

    #endregion

    #region LoadLevel

    public void LoadPVEFight()
    {

    }

    public void LoadLevel(GameSceneType level)
    {
        UIController.Instance.RemoveAllTip();
        UIController.Instance.CloseAllOpendPanel();
        this.m_LoadingLevel = true;


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
        //Debug.Log("DelayOpenPanel : ");
        yield return new WaitForSeconds(0.2f);
        //Debug.Log("DelayOpenPanel  WaitForSeconds: ");

        EasyTouch.instance.touchCameras[0] = new ECamera(Camera.main, false);
        switch (this.CurrentScene)
        {
            case GameSceneType.LoginScene:
                EasyTouch.instance.enabledNGuiMode = false;
                break;
            case GameSceneType.MainScene:
                break;
            default:
                break;
        }
        this.m_LoadingLevel = false;
    }

    #endregion
}
