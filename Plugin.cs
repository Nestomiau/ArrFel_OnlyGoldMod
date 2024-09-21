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
        public const string modNAME = "Arroz's Only Gold Mod";
        public const string modVER = "1.0.0";
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

    /*
    public class Mono : MonoBehaviour
    {
        SelectableLevel[] SelectableLevels = GameObject.FindObjectsOfType<SelectableLevel>();
        SpawnableItemWithRarity GoldDefault;
        public GameObject goldAssingment;

        // Set Up Variables
        bool setup_gold;
        bool setup_levels;
        void Awake() 
        {
            setup_gold = false;
            setup_levels = false;
            goldAssingment = GameObject.Find("GoldBar");
        }
        void Update()
        {
            goldAssingment = GameObject.Find("GoldBar");
            if (goldAssingment) { Main.inst.mls.LogInfo("FUCK"); }

            // SETUP GOLDEN BARS
            /*var GoldBar = GameObject.Find("GoldBar");
            if (GoldBar && !setup_gold)
            {
                GoldDefault.rarity = 100;
                GoldDefault.spawnableItem = GoldBar.GetComponent<Item>();
                setup_gold = true;
                Main.inst.mls.LogInfo("Setted Up Gold Object");
            }
            else if (!GoldBar && setup_gold) { setup_gold = false; }

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
                Main.inst.mls.LogInfo("Setted Up Levels");
            }
            else if (DineLevel == null && setup_levels) { setup_levels = false; }
        }
    }
    */
}
