using HarmonyLib;
using System.Reflection;
using UnityEngine;
using DMT;
using System.Collections.Generic;
using System;

public class LMod_Block
{

    //Controls which class, and which method to patch over
    [HarmonyPatch(typeof(Block))]
    [HarmonyPatch("PlaceBlock")]
    public class LMod_Block_PlaceBlock
    {

        public static int expAmount = 10;

        /**
         * Will be ran when this method is executedm
         **/

        static bool Prefix(EntityAlive _ea, BlockPlacement.Result _result)
        {

            Block block = Block.list[_result.blockValue.type];

            if (block.CanPickup)
                return true;

            EntityPlayerLocal player = _ea as EntityPlayerLocal;
            ProgressionValue masterProgression = player.Progression.GetProgressionValue("LModMaster"); //set in progression.xml
            ProgressionValue perkProgression = player.Progression.GetProgressionValue("LModPlaceBlock"); //set in progression.xml

            if (masterProgression == null || perkProgression == null||perkProgression.Level==0)
                return true;

            float multiplier = 1 + ((masterProgression.Level / 10) + ((Math.Max(1, perkProgression.Level) * 2) / 10));
            player.Progression.AddLevelExp( Mathf.FloorToInt(LMod_Block_PlaceBlock.expAmount * multiplier));

            return true;
        }
    }
}

