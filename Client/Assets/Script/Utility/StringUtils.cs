using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using System;
using System.IO;

public sealed class StringUtils
{
    public static uint[] StringToArray(string mess)
    {
        if (string.IsNullOrEmpty(mess) || mess.Equals("0"))
            return null;
        string[] arr = mess.Split(',');
        if (arr == null || arr.Length == 0)
            return null;
        int leng = arr.Length;
        if (string.IsNullOrEmpty(arr[leng - 1]))
            leng -= 1;
        uint[] array = new uint[leng];
        for (int i = 0; i < leng; i++)
        {
            array[i] = uint.Parse(arr[i].ToString());
        }
        return array;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mess">id,id,id,...</param>
    /// <returns></returns>
    public static int[] ParseString(string mess)
    {
        if (string.IsNullOrEmpty(mess) || mess.Equals("0"))
            return null;
        string[] arr = mess.Split(',');
        if (arr == null || arr.Length == 0)
            return null;
        int leng = arr.Length;
        if (string.IsNullOrEmpty(arr[leng - 1]))
            leng -= 1;
        int[] array = new int[leng];
        for (int i = 0; i < leng; i++)
        {
            array[i] = int.Parse(arr[i].ToString());
        }
        return array;
    }

    public static List<int[]> ParseString2(string mess)
    {

        if (string.IsNullOrEmpty(mess) || mess.Equals("0"))
            return null;
        string[] arr = mess.Split(';');
        if (arr == null || arr.Length == 0)
            return null;
        int leng = arr.Length;
        if (string.IsNullOrEmpty(arr[leng - 1]))
            leng -= 1;
        List<int[]> list = new List<int[]>();
        for (int i = 0; i < leng; i++)
        {
            int[] tarray = ParseString(arr[i]);
            if (tarray != null)
                list.Add(tarray);
        }
        return list;
    }

    public static Dictionary<int, int> ParseStringDouble(string mess)
    {
        if (string.IsNullOrEmpty(mess) || mess.Equals("0"))
            return null;
        string[] arr = mess.Split(';');
        if (arr == null || arr.Length == 0)
            return null;
        int leng = arr.Length;
        if (string.IsNullOrEmpty(arr[leng - 1]))
            leng -= 1;

        Dictionary<int, int> dic = new Dictionary<int, int>();
        for (int i = 0; i < leng; i++)
        {
            string[] temp = arr[i].Split(',');
            int id = int.Parse(temp[0]);
            int rate = int.Parse(temp[1]);
            dic.Add(id, rate);
        }
        return dic;
    }

    /// <summary>
    /// 是否包含中文
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsChinese(string str)
    {
        if (Regex.IsMatch(str, @"^[\u4e00-\u9fa5]+$"))
            return true;
        else return false;

    }

    /// <summary>
    /// 是否只有：数字、字母、中文，下划线组成。下划线不能在开头和结尾。
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsRegexNickName(string str)
    {

        if (Regex.IsMatch(str, @"^(?!_)(?!.*?_$)[\u4e00-\u9fa5_a-zA-Z0-9]+$"))
            return true;
        else return false;
    }

    /// <summary>
    /// 是否由字母和数字组成
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsNumberOrLetter(string str)
    {
        return Regex.IsMatch(str, @"(?i)^[0-9a-z]+$");
    }

    /// <summary>
    /// 获取字符串的字节长度 Unicode编码
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static int GetByteLengthUTF8(string str)
    {
        if (str == null)
            return -1;
        return System.Text.Encoding.Unicode.GetBytes(str).Length;
    }

    public static string FormatString<T>(T[] array, string lab)
    {
        if (array == null)
            return lab + " Length = null";
        string mess = lab + " Length = " + array.Length + ":{";
        for (int i = 0; i < array.Length; i++)
        {
            mess += array[i].ToString() + ",";
        }
        return mess + "}";
    }

    public static string FormatString<T>(List<T> list, string lab)
    {
        string mess = lab + " Count = " + list.Count + " ：{";
        for (int i = 0; i < list.Count; i++)
        {
            mess += list[i].ToString() + ",";
        }
        return mess + "}";
    }

    public static int GetStringLength(string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return -1;
        }
        ASCIIEncoding n = new ASCIIEncoding();
        byte[] b = n.GetBytes(str);
        int length = 0;
        for (int i = 0; i <= b.Length - 1; i++)
        {
            //判断是否为汉字或全角符号
            if (b[i] == 63)
            {
                length++;
            }
            length++;
        }
        return length;
    }

    public static bool CheckUserName(string name)
    {
        int min = 3;
        int max = 30;
        //Regex reg = new Regex(@"^[A-Za-z0-9]{3,30}$");
        Regex reg = new Regex(@"^[A-Za-z0-9]{" + min + "," + max + "}$");
        return reg.IsMatch(name);
    }

    public static bool CheckPassword(string psd)
    {
        int min = 6;
        int max = 15;
        //Regex reg = new Regex(@"^([A-Za-z0-9]|[._]){6,25}$");
        Regex reg = new Regex(@"^([A-Za-z0-9]|[._]){" + min + "," + max + "}$");
        return reg.IsMatch(psd);
    }

    public static bool CheckNikeName(string name)
    {
        int min = 2;
        int max = 12;
        int length = GetStringLength(name);
        return (length >= min && length <= max);
    }

    public static string GetNumberStr(int number)
    {
        if (number < 10000) return number.ToString();
        if (number < 100000000) return LocalizationUtils.GetText("Common.Label.TenThousand", number / 10000);
        return LocalizationUtils.GetText("Common.Label.HundredMillion", number / 100000000);
    }

    public static string GetTimeStr(long time)
    {
        if (time / TimeSpan.TicksPerDay > 0)
        {
            return LocalizationUtils.GetText("Common.Time.DayAgo", time / TimeSpan.TicksPerDay);
        }
        else if (time / TimeSpan.TicksPerHour > 0)
        {
            return LocalizationUtils.GetText("Common.Time.HourAgo", time / TimeSpan.TicksPerHour);
        }
        else if (time / TimeSpan.TicksPerMinute > 0)
        {
            return LocalizationUtils.GetText("Common.Time.MinuteAgo", time / TimeSpan.TicksPerMinute);
        }
        else if (time / TimeSpan.TicksPerSecond > 0)
        {
            return LocalizationUtils.GetText("Common.Time.SecondAgo", time / TimeSpan.TicksPerSecond);
        }
        return LocalizationUtils.GetText("Common.Time.Now");
    }

    public static Regex GetDirtyRegex()
    {
        string dirtyDic = GetFileStr(@"File/DirtyDictionary");
        string regexPattern = @"^((?!" + dirtyDic + ").(?<!" + dirtyDic + "))*$";
        //return new Regex(regexPattern, RegexOptions.Compiled | RegexOptions.ExplicitCapture);
        return new Regex(regexPattern, RegexOptions.ExplicitCapture);
    }

    public static string[] GetFileStrArr(string path)
    {
        return GetFileStr(path).Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
    }

    public static Dictionary<string, string> GetFileStrDic(string path, char splitChar)
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();
        string[] strArr = GetFileStr(path).Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (string str in strArr)
        {
            if (string.IsNullOrEmpty(str))
            {
                //Debug.Log("str is IsNullOrEmpty");
                continue;
            }
            string[] keyAndValue = str.Split(new char[] { splitChar }, 2);
            if (keyAndValue == null || keyAndValue.Length != 2)
            {
                Debug.LogError(str + "  is error");
                continue;
            }
            string key = keyAndValue[0].Trim();
            string value = keyAndValue[1].TrimStart().TrimEnd();
            if (dic.ContainsKey(key))
            {
                Debug.LogError(key + "  has  echo key ");
                continue;
            }
            dic.Add(key, value);
        }
        return dic;
    }

    public static bool CheckGiftKey(string key)
    {
        string pattern = @"^([a-zA-Z0-9]{16})$";
        return Regex.IsMatch(key, pattern);
    }

    public static string GetFileStr(string path)
    {
        TextAsset textAsset = Resources.Load(path) as TextAsset;
        if (textAsset == null)
        {
            Debug.LogError(path + " is not exist");
        }
        return textAsset.text;
    }

    public static string[] GetFileStrArr(string path, char splitChar)
    {
        return GetFileStr(path).Split(splitChar);
    }

}
