using ArrFel_OnlyGoldMod.Patches;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace ArFe_ChaosMod
{
    [BepInPlugin(modGUID, modNAME, modVER)]
    public class Main : BaseUnityPlugin
    {
        // MOD Info
        public const string modGUID = "ArrFel.OnlyGoldMod";
        public const string modNAME = "Only Gold Mod";
        public const string modVER = "1.0.2";
        public const string modBY = "Mewings da Galaxia";

        private readonly Harmony harmony = new Harmony(modGUID);

        // Objects / References
        internal ManualLogSource mls;
        public static Main inst;
        public SpawnableItemWithRarity goldBar = new SpawnableItemWithRarity();
        public bool hanstCreatedGold = true;

        void Awake() 
        {
            if (inst == null) { inst = this; }
            else if (inst != this) { Destroy(this); }

            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);
            mls.LogInfo("Only Gold Mod has Awoken");

            mls.LogInfo("Patching Scripts...");
            harmony.PatchAll(typeof(Main));
            harmony.PatchAll(typeof(RoundManagerPatches));
            mls.LogInfo("Patched Scripts.");
        }
    }
}
