using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;

[HarmonyPatch(typeof(GUIWindowConsole))]
[HarmonyPatch("openConsole")]
class LMod_ConsoleFix
{

    /**
     * Really simple fix to stop exceptions automatically opening console
     **/
    static bool Prefix()
    {

        return false;
    }
}

