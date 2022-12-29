using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TimeManager
{
    public static void StopTime()
    {
        Time.timeScale = 0;
    }

    public static void StartTime()
    {
        Time.timeScale = 1;
    }
}
