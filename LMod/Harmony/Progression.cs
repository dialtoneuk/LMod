using DMT;
using HarmonyLib;
using System;
using System.Reflection;
using UnityEngine;

public class LMod_Progression
{

    [HarmonyPatch(typeof(Progression))]
    [HarmonyPatch("AddLevelExpRecursive")]
    public class LMod_Progression_AddLevelExpRecursive
    {


        //chance of said xp bonus
        public static int chance = 200;

        /**
         * Simple script to essentially add a skill point if its ready to level
         **/

        static bool Prefix(Progression __instance, ref int exp)
        {

            System.Random r = new System.Random();
            ProgressionValue skillPoints = __instance.GetProgressionValue("LModSkillPoints");
            ProgressionValue masterProgression = __instance.GetProgressionValue("LModMaster");

            if (skillPoints == null||masterProgression==null)
                return true;


            if(skillPoints.Level!=0)
                Progression.SkillPointsPerLevel = 1 + Math.Max(1, Math.Min(5, skillPoints.Level));

            if (__instance.ExpToNextLevel - exp <= 0)
            {
                //chance is moderate depending on your level
                if (r.Next(1, LMod_Progression_AddLevelExpRecursive.chance) < (Math.Min(90, Math.Max(10, masterProgression.Level * 10))))
                {

                    Progression.SkillPointsPerLevel += 1;
                    GameManager.ShowTooltip(__instance.parent as EntityPlayerLocal, "You've just got a bonus skill point!");
                }
            }
            
            return true;
        }
    }

    [HarmonyPatch(typeof(Progression))]
    [HarmonyPatch("AddLevelExp")]
    public class LMod_Progression_AddLevelExp
    {

        //chance of said xp bonus
        public static int chance = 500;
        //wont do it on exp gains larger than this
        public static int maxExp = 1500;

        /**
         * Simple script to essentially add our precentage increase
         **/

        static bool Prefix(Progression __instance, ref int _exp)
        {

            System.Random r = new System.Random();
            ProgressionValue masterProgression = __instance.GetProgressionValue("LModMaster");

            if (masterProgression == null)
                return true;

            //chance is actually pretty low
            if (_exp <= LMod_Progression_AddLevelExp.maxExp && r.Next(1, LMod_Progression_AddLevelExp.chance)<(Math.Min(90, Math.Max(10, masterProgression.Level*10))))
            {

                if (masterProgression == null)
                    return true;

                _exp = _exp * masterProgression.Level;
                GameManager.ShowTooltip(__instance.parent as EntityPlayerLocal, "Yay! You've just got some bonus xp from that");
                return true;
            }

            return true;
        }
    }
}