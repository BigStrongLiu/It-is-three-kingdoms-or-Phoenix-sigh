using UnityEngine;

public  static class PlayerPrefsUtils
{
    private static readonly string Player = "lastplayers";
    private static readonly string LastServer = "lastserver";
    private static readonly string Music = "music";
    private static readonly string Sound= "sound";
    private static readonly string ShowReceivedAchievement = "showreceivedachievement";

    public static string GetPlayers()
    {
        if (!PlayerPrefs.HasKey(Player))
            return null;
        return PlayerPrefs.GetString(Player);
    }

    public static void SavePlayers(string playerStr)
    {
        PlayerPrefs.SetString(Player, playerStr);
    }

    public static int GetLastServer()
    {
        if (!PlayerPrefs.HasKey(LastServer)) return 0;
        return PlayerPrefs.GetInt(LastServer);
    }

    public static void SaveLastServer(int server)
    {
        PlayerPrefs.SetInt(LastServer, server);
    }

    public static bool GetMusicState()
    {
        if (!PlayerPrefs.HasKey(Music)) return false;
        return PlayerPrefs.GetInt(Music) == 1;
    }

    public static void SaveMusicState(bool opened)
    {
        PlayerPrefs.SetInt(Music,opened?1 :0);
    }

    public static bool GetSoundState()
    {
        if (!PlayerPrefs.HasKey(Sound)) return false;
        return PlayerPrefs.GetInt(Sound) == 1;
    }

    public static void SaveSoundState(bool opened)
    {
        PlayerPrefs.SetInt(Sound, opened ? 1 : 0);
    }

    public static bool GetShowReceivedAchievementState()
    {
        if (!PlayerPrefs.HasKey(ShowReceivedAchievement)) return false;
        return PlayerPrefs.GetInt(ShowReceivedAchievement) == 1;
    }

    public static void SaveShowReceivedAchievementState(bool isShow)
    {
        PlayerPrefs.SetInt(ShowReceivedAchievement, isShow ? 1 : 0);
    }
}
