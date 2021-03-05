
using DMT;
using System.Reflection;
using HarmonyLib;
using UnityEngine;

public class LMod_Init : IHarmony
{

    /**
     * This essentially is where it all begins, and starts the process that patches in our edits
     **/

    public void Start()
    {
        // Reduce extra logging stuff
        Application.SetStackTraceLogType(UnityEngine.LogType.Log, StackTraceLogType.None);
        Application.SetStackTraceLogType(UnityEngine.LogType.Warning, StackTraceLogType.None);

        var harmony = new Harmony("com.llydia.lmod");
        Harmony.DEBUG = true;

        //automatically patches everything
        harmony.PatchAll();
    }
}
