using System;

public sealed class TimeUtils
{
    /// <summary>
    /// 格式化时间
    /// </summary>
    /// <param name="seconds"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public static string FormatTime(long seconds, TimeFormatType type = TimeFormatType.Second)
    {
        switch (type)
        { 
            case TimeFormatType.Second:
                return FormatTimeToSecond(seconds);
            case TimeFormatType.Minute:
                return FormatTimeToMinute(seconds);
            case TimeFormatType.Hour:
                return FormatTimeToHour(seconds);
        }
        return seconds.ToString();
    }

    /// <summary>
    /// 格式化时间
    /// 00
    /// </summary>
    /// <param name="seconds"></param>
    /// <returns></returns>
    public static string FormatTimeToSecond(long seconds)
    {        
        TimeSpan t = TimeSpan.FromSeconds(seconds);
        string time = string.Empty;
        if (seconds > 86400 )
        {
            long hour = seconds / 3600;
            byte min = (byte)((seconds % 3600) / 60);
            byte sec = (byte)((seconds % 3600) % 60);
            time = string.Format("{0:D2}:{1:D2}:{2:D2}", hour, min, sec);
        }
        else if (seconds > 3600)
        {
            time = string.Format("{0:D2}:{1:D2}:{2:D2}", t.Hours, t.Minutes, t.Seconds);
        }
        else if (seconds > 60)
        {
            byte min = (byte)(seconds / 60);
            byte sec = (byte)(seconds % 60);
            time = string.Format("{0:D2}:{1:D2}", min, sec);
        }
        else
        {
            time = string.Format("{0:D2}", seconds);
        }
        return time;
    }

    /// <summary>
    /// 格式化时间
    /// 00：00
    /// </summary>
    /// <param name="seconds"></param>
    /// <returns></returns>
    public static string FormatTimeToMinute(long seconds)
    {
        TimeSpan t = TimeSpan.FromSeconds(seconds);
        string time = string.Empty;
        if (seconds > 86400)
        {
            long hour = seconds / 3600;
            byte min =(byte)( (seconds % 3600) / 60);
            byte sec = (byte)((seconds % 3600) % 60);
            time = string.Format("{0:D2}:{1:D2}:{2:D2}", hour, min, sec);
        }
        else if (seconds > 3600)
        {
            time = string.Format("{0:D2}:{1:D2}:{2:D2}", t.Hours, t.Minutes, t.Seconds);
        }
        else
        {
            byte min = (byte)(seconds / 60);
            byte sec = (byte)(seconds % 60);
            time = string.Format("{0:D2}:{1:D2}", min,sec );
        }
        return time;
    }

    /// <summary>
    /// 格式化时间
    /// 00：00：00
    /// </summary>
    /// <param name="seconds"></param>
    /// <returns></returns>
    public static string FormatTimeToHour(long seconds)
    {
        TimeSpan t = TimeSpan.FromSeconds(seconds);
        string time = string.Empty;
        if (seconds > 86400)
        {
            long hour = seconds / 3600;
            byte min = (byte)((seconds % 3600) / 60);
            byte sec = (byte)((seconds % 3600) % 60);
            time = string.Format("{0:D2}:{1:D2}:{2:D2}", hour, min, sec);
        }
        else 
        {
            time = string.Format("{0:D2}:{1:D2}:{2:D2}", t.Hours, t.Minutes, t.Seconds);
        }
        return time;
    }


    public static string GetTimeStamp()
    {
        TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return ts.TotalSeconds.ToString();
    }

    public static string GetNowStamp(int seconds)
    {
        DateTime daTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        DateTime newDay = daTime.AddSeconds(seconds);

        string nowTime = newDay.ToString();

        string[] timeGroup = nowTime.Split(' ');
        string[] yearGroup = timeGroup[0].Split('/');

        string time = "00";

        if (nowTime.Contains("AM") && timeGroup[1].Substring(0,2)=="12") 
            time = "00" + timeGroup[1].Substring(2);
        else if (nowTime.Contains("AM") && timeGroup[1].Substring(1, 1) == ":")
            time = "0" + timeGroup[1];
        else if (nowTime.Contains("PM"))
        {
            string[] timePM = timeGroup[1].Split(':');
            timePM[0] = (int.Parse(timePM[0]) + 12).ToString();
            time = timePM[0] + ":" + timePM[1] + ":" + timePM[2];
        }
        else 
        {
            time = timeGroup[1];
        }
        nowTime = yearGroup[2] + "-" + yearGroup[0] + "-" + yearGroup[1]+" " + time;
        return nowTime;
    }  
}
