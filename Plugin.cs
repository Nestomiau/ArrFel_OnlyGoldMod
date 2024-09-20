using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System;
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
        public const string modVER = "0.0.0";
        public const string modBY = "Mewings da Galaxia";

        private readonly Harmony harmony = new Harmony(modGUID);

        // Objects / References
        internal ManualLogSource mls;
        internal static SkibidiMewing sdmw;
        public static Main inst;

        void Awake() 
        {
            if (inst == null) { inst = this; }
            else if (inst != this) { Destroy(this); }

            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);
            mls.LogInfo("Only Gold Mod has Awoken");

            mls.LogInfo("Patching Scripts...");
            harmony.PatchAll(typeof(Main));
            mls.LogInfo("Patched Scripts.");

            mls.LogInfo("Creating SkibidiMewing Object...");
            var chaosHandlerObj = new GameObject("ArrFel_CMHandler");
            DontDestroyOnLoad(chaosHandlerObj);
            chaosHandlerObj.hideFlags = (HideFlags)61;
            chaosHandlerObj.AddComponent<SkibidiMewing>();
            sdmw = (SkibidiMewing)chaosHandlerObj.GetComponent("SkibidiMewing");
            mls.LogInfo("Created SkibidiMewing Object.");
        }


    }

    public class SkibidiMewing : MonoBehaviour
    {

        string[] levelList;

        void Start()
        {
            levelList = new string[1];
        }

        void Update()
        {
            var _level = GameObject.Find("ExperimentationLevel");
            if (_level == null) { return; }
            var _component = (SkibidiMewing)chaosHandlerObj.GetComponent("SkibidiMewing");
        }
    }
}
