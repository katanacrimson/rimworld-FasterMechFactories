using System;
using System.Collections.Generic;
using HugsLib;
using HugsLib.Settings;
using RimWorld;
using UnityEngine;
using Verse;

namespace FasterMechFactories
{
    public class FasterMechFactoriesMod : ModBase
    {
        public static int factoryMultiplier;

        private SettingHandle<int> factoryMultiplierSetting;

        public override string ModIdentifier
        {
            get
            {
                return "FasterMechFactories";
            }
        }

        public override void DefsLoaded()
        {
            this.factoryMultiplierSetting = base.Settings.GetHandle<int>("fmf_factorymultiplier", "Factory Multiplier Amount", "How much faster should factories operate?", 1, null, null);
            this.SettingsChanged();
        }

        public override void SettingsChanged()
        {
            FasterMechFactoriesMod.factoryMultiplier = Math.Max(1, Math.Min(50, this.factoryMultiplierSetting.Value));
        }
    }
}
