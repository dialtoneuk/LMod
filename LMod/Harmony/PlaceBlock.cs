using HarmonyLib;
using System.Reflection;
using UnityEngine;
using DMT;
using System.Collections.Generic;


//Controls which class, and which method to patch over
[HarmonyPatch(typeof(Block))]
[HarmonyPatch("PlaceBlock")]
public class LMod_Placeblock
{

    /**
     * Will be ran when this method is executedm
     **/

    static bool Prefix(EntityAlive _ea, BlockPlacement.Result _result)
    {

        Block block = Block.list[_result.blockValue.type];

        if(block.CanPickup)
            return true;

        EntityPlayerLocal player = _ea as EntityPlayerLocal;
        ProgressionValue masterProgression = player.Progression.GetProgressionValue("LModMaster"); //set in progression.xml
        ProgressionValue perkProgression = player.Progression.GetProgressionValue("LModPlaceBlock"); //set in progression.xml

        if (masterProgression == null || perkProgression == null)
            return true;

        int multiplier = 1 * (masterProgression.Level*(perkProgression.Level*2));
        player.Progression.AddLevelExp( Mathf.Min( 50,  1 * multiplier ));
 
        return true;
    }
}