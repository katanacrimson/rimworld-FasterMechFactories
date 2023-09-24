using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;
using VanillaMemesExpanded;
using ItemProcessor;

namespace FasterMechFactories
{
    [HarmonyPatch]
    public static class FactoryModifier_Patch
    {
        [HarmonyPatch(typeof(GameComponent_FactoryModifier), "GameComponentTick")]
        static bool Prefix(GameComponent_FactoryModifier __instance, int ___tickInterval, ref int ___tickCounter)
        {
            ___tickCounter++;
            bool shouldReset = ___tickCounter > ___tickInterval;
            if (shouldReset)
            {
                ___tickCounter = 0;
            }

            // separate from shouldReset check so that we force the update on initial tick
            if (___tickCounter == 0)
            {
                float efficiency = 1f;
                bool hasIncreasedEfficiency = Current.Game.World.factionManager.OfPlayer.ideos.GetPrecept(InternalDefOf.VME_AutomationEfficiency_Increased) != null;
                if (hasIncreasedEfficiency)
                {
                    efficiency -= 0.25f;
                }
                bool hasDecreasedEfficiency = Current.Game.World.factionManager.OfPlayer.ideos.GetPrecept(InternalDefOf.VME_AutomationEfficiency_Decreased) != null;
                if (hasDecreasedEfficiency)
                {
                    efficiency += 0.50f;
                }

                efficiency /= FasterMechFactoriesMod.factoryMultiplier;
                FactoryMultiplierClass.FactoryPreceptMultiplier = Math.Max(efficiency, 0f);
            }

            return false;
        }
    }
}

