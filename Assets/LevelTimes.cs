using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelTimes
{
    public static void setLevelTime(string level, string time)
    {
        PlayerPrefs.SetString(level, time);
    }

    public static string getLevelTime(string level)
    {
        return PlayerPrefs.GetString(level, "NONE");
    }
}
