using ArFe_ChaosMod;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine.Rendering;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

namespace ArrFel_OnlyGoldMod.Patches
{
    [HarmonyPatch(typeof(RoundManager))]
    internal class RoundManagerPatches
    {
        [HarmonyPatch("LoadNewLevel")]
        [HarmonyPrefix]
        public static void Update_PreLoadNewLevel(ref int randomSeed,ref SelectableLevel newLevel) {
            bool hanstGold = true;
            foreach (SpawnableItemWithRarity _item in newLevel.spawnableScrap)
            {
                _item.rarity = 0;
                if (_item.spawnableItem.itemName == "Gold bar")
                {
                    hanstGold = false;
                    _item.rarity = 100;
                }
            }
            if (hanstGold)
            {
                if (Main.inst.hanstCreatedGold)
                {
                    foreach (Item items in StartOfRound.Instance.allItemsList.itemsList)
                    {
                        if (items.itemName == "Gold bar")
                        {
                            Main.inst.goldBar.spawnableItem = items;
                            Main.inst.goldBar.rarity = 100;
                            Main.inst.hanstCreatedGold = true;
                        }
                    }
                }
                newLevel.spawnableScrap.Add(Main.inst.goldBar);
            }
        }
    }
}
