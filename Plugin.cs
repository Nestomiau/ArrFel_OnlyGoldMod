using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ArFe_ChaosMod
{
    [BepInPlugin(modGUID, modNAME, modVER)]
    public class Main : BaseUnityPlugin
    {
        // MOD Info
        public const string modGUID = "ArrFel.OnlyGoldMod";
        public const string modNAME = "Arroz's Only Gold Mod";
        public const string modVER = "1.0.0";
        public const string modBY = "Mewings da Galaxia";

        private readonly Harmony harmony = new Harmony(modGUID);

        // Objects / References
        internal ManualLogSource mls;
        internal static Mono mono;
        public static Main inst;

        void Awake() 
        {
            if (inst == null) { inst = this; }
            else if (inst != this) { Destroy(this); }

            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);
            mls.LogInfo("Only Gold Mod has Awoken");

            mls.LogInfo("Patching Scripts...");
            harmony.PatchAll(typeof(Main));
            harmony.PatchAll(typeof(Mono));
            mls.LogInfo("Patched Scripts.");

            mls.LogInfo("Creating MonoBehavior Object...");
            var goldHandlerObj = new GameObject("ArrFel_OGMHandler");
            DontDestroyOnLoad(goldHandlerObj);
            goldHandlerObj.hideFlags = (HideFlags)61;
            goldHandlerObj.AddComponent<Mono>();
            mono = (Mono)goldHandlerObj.GetComponent("Mono");
            mls.LogInfo("Created MonoMonoBehavior Object.");
        }


    }

    public class Mono : MonoBehaviour
    {
        SelectableLevel[] SelectableLevels = GameObject.FindObjectsOfType<SelectableLevel>();
        SpawnableItemWithRarity GoldDefault;

        // Set Up Variables
        bool setup_gold=false;
        bool setup_levels=false;
        void Update()
        {
            // SETUP GOLDEN BARS
            var GoldBar = GameObject.Find("GoldBar");
            if (GoldBar != null && !setup_gold)
            {
                GoldDefault.rarity = 100;
                GoldDefault.spawnableItem = GoldBar.GetComponent<Item>();
                setup_gold = true;
            }
            else if (GoldBar == null && setup_gold) { setup_gold = false; }

            // SETUP LEVELS
            var DineLevel = GameObject.Find("DineLevel");
            if (DineLevel != null && !setup_levels && setup_gold)
            {
                SelectableLevels = GameObject.FindObjectsOfType<SelectableLevel>();
                for (int _i = 0; _i < SelectableLevels.Length; _i += 1)
                {
                    SelectableLevels[_i].spawnableScrap.Clear();
                    SelectableLevels[_i].spawnableScrap.Add(GoldDefault);
                }
                setup_levels = true;
            }
            else if (DineLevel == null && setup_levels) { setup_gold = false; }
        }
    }
}
