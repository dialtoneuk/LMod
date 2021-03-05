using DMT;
using HarmonyLib;
using System;
using System.Reflection;
using UnityEngine;

public class LMod_LootContainer
{

    [HarmonyPatch(typeof(LootContainer))]
    [HarmonyPatch("SpawnItem")]
    public class LMod_LootContainer_SpawnItem
    {

        static bool Prefix(LootContainer __instance, ref int countToSpawn, EntityPlayer player)
        {

            if (player == null)
                return true;

            EntityPlayerLocal _player = player as EntityPlayerLocal;

            ProgressionValue masterProgression = _player.Progression.GetProgressionValue("LModMaster"); //set in progression.xml
            ProgressionValue perkProgression = _player.Progression.GetProgressionValue("LModLootAbundance"); //set in progression.xml

            if (masterProgression == null || perkProgression == null)
                return true;
             
            float value = 1.0f + (Math.Max(1, perkProgression.Level)/10);
            countToSpawn = Mathf.FloorToInt((float)countToSpawn * value);

            return true;
        }
    }
}
