using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

/// <summary>
/// 本地化工具
/// </summary>
public sealed class LocalizationUtils
{
    public enum ResourceType { Default }
    public static readonly LocalizationUtils Instance = new LocalizationUtils();
    private Dictionary<string, string> m_PopupwinDic;

    #region constructor

    private LocalizationUtils()
    {
        this.m_PopupwinDic = new Dictionary<string, string>();
        this.SwitchLocal(LanguageType.zh_CN);
    }

    #endregion

    #region private methods

    public void Init()
	{
        this.SwitchLocal(LanguageType.zh_CN);
    }

    private void SwitchLocal(LanguageType country)
    {
        this.m_PopupwinDic = StringUtils.GetFileStrDic("File/Local/" + country,'=');
    }

    private Dictionary<string, string> GetDicByType(ResourceType type)
    {
        switch (type)
        {
            case ResourceType.Default:
                return this.m_PopupwinDic;
        }
        return null;
    }
	
	private string GetString(string key,ResourceType type)
	{
		Dictionary<string,string> dic = GetDicByType(type);
		if(dic == null)
			throw new Exception("locale language manager can not find file!!!");
		foreach(KeyValuePair<string,string> pair in dic){
			if(pair.Key.Equals(key)){
				return pair.Value;
			}
		}
        Debug.LogError(key + " not exsit");
		return string.Empty;
	}

    private string GetString(string key, ResourceType type, params object[] args)
    {
        if (string.IsNullOrEmpty(key))
        {
            //Debug.LogWarning(" The key is null");
            return string.Empty;
        }
        string val = GetString(key, type);
        if (string.IsNullOrEmpty(val))
        {
            //Debug.LogWarning(key + " is not exist ");
            return string.Empty;
        }
        if (args == null)
        {
            return val;
        }
        else
        {
            return string.Format(val, args);
        }
    }

    #endregion

    #region static methods

    public static string GetText(string key, params object[] args)
    {
        return Instance.GetString(key, ResourceType.Default, args);
    }

    public static string GetText(string key)
    {
        return Instance.GetString(key, ResourceType.Default);
    }

    #endregion

}
