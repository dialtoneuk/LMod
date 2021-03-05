using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;


class LMod_GUIWindowConsole
{

    [HarmonyPatch(typeof(GUIWindowConsole))]
    [HarmonyPatch("openConsole")]
    public class Lmod_GUIWindowConsole_OpenConsole
    {

        /**
         * Really simple fix to stop exceptions automatically opening console
         **/

        static bool Prefix()
        {

            return false;
        }
    }
}

