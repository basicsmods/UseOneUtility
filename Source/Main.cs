using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using UnityEngine;
using Verse;
using Verse.AI.Group;

/*
 * https://harmony.pardeike.net/articles/patching-injections.html#__instance
 * 
 */


namespace UseOneUtility
{
    public static class Constants
    {
        public static long threshold = 250; // in ms
    }

	[StaticConstructorOnStartup]
	public static class Main
	{
		static Main()
		{
			var harmony = new Harmony("net.basics.UseOneUtility");
			harmony.PatchAll();
		}
	}
    
    [HarmonyPatch(typeof(Verb_DeployBroadshield), nameof(Verb_DeployBroadshield.TryCastShot),
    new Type[] { })]
    class Verb_DeployBroadshield_TryCastShot
    {
        public static long last_deploy = 0;
        public static bool Prefix(Verb_DeployBroadshield __instance)
        {
            long milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            if (last_deploy == 0 || milliseconds - last_deploy > Constants.threshold)
            {
                last_deploy = milliseconds;
                return true;
            } else
            {
                return false;
            }
        }
    }

    [HarmonyPatch(typeof(Verb_FirefoamPop), nameof(Verb_FirefoamPop.TryCastShot),
    new Type[] { })]
    class Verb_FirefoamPop_TryCastShot
    {
        public static long last_deploy = 0;
        public static bool Prefix(Verb_FirefoamPop __instance)
        {
            long milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            if (last_deploy == 0 || milliseconds - last_deploy > Constants.threshold)
            {
                last_deploy = milliseconds;
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    [HarmonyPatch(typeof(Verb_DeployToxPack), nameof(Verb_DeployToxPack.TryCastShot),
    new Type[] { })]
    class Verb_DeployToxPack_TryCastShot
    {
        public static long last_deploy = 0;
        public static bool Prefix(Verb_DeployToxPack __instance)
        {
            long milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            if (last_deploy == 0 || milliseconds - last_deploy > Constants.threshold)
            {
                last_deploy = milliseconds;
                return true;
            }
            else
            {
                return false;
            }
        }
    }


    /*
     * public virtual bool Verb::TryStartCastOn(LocalTargetInfo castTarg, LocalTargetInfo destTarg, 
     *                              bool surpriseAttack = false, bool canHitNonTargetPawns = true, 
     *                              bool preventFriendlyFire = false, bool nonInterruptingSelfCast = false)
     */
    //[HarmonyPatch(typeof(Verb), nameof(Verb.TryStartCastOn),
    //new Type[] { typeof(LocalTargetInfo), typeof(LocalTargetInfo), 
    //             typeof(bool), typeof(bool), typeof(bool), typeof(bool) })]
    //class Verb_TryStartCastOn
    //{
    //    public static long last_deploy = 0;
    //    public static bool Prefix(Verb __instance)
    //    {
    //        Log.Message("Verb_TryStartCastOn");
    //        Log.Message(__instance.ReportLabel);            
    //        // deploy low-shield
    //        // assault rifle
    //        // berserk pulse
    //        // psychic shock lance
    //        // pop firefoam
    //        
    //        return true;
    //        /*
    //        long milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
    //        if (last_deploy == 0 || milliseconds - last_deploy > 250)
    //        {
    //            last_deploy = milliseconds;
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }*/
    //    }
    //}

    // This one could work, except that it would depend on the translated 
    // name of the command. Which means it may not work for different languages.
    /*
    [HarmonyPatch(typeof(Gizmo), nameof(Gizmo.InheritInteractionsFrom),
    new Type[] { typeof(Gizmo) })]
    class Gizmo_InheritInteractionsFrom
    {
        public static long last_deploy = 0;
        public static void Postfix(Gizmo __instance, ref bool __result)
        {
            Log.Message("Gizmo_InheritInteractionsFrom");
            Command cmd = __instance as Command;
            if (cmd != null && cmd.Label != null)
            {
                Log.Message(cmd.Label);
                //
                //deploy low-shield
                //psychic shock lance
                //
            }
            __result = false;
            return;
        }
    }*/
}
