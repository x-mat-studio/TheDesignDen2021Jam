using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticGlobalVars
{
    public static int totalKills = 0;
    public static float secondsToKillBoss = -1.0f;

    public static float revolutionsPerMinuteDisplay = 0.0f;

    public static void ResetStaticVars()
    {
        totalKills = 0;
        secondsToKillBoss = -1.0f;
        revolutionsPerMinuteDisplay = 0.0f;
    }
}
