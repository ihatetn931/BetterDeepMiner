using Assets.Scripts.Objects.Items;
using Assets.Scripts.Objects.Pipes;
using Assets.Scripts;
using HarmonyLib;
using Assets.Scripts.Objects;

namespace BetterDeepMiner
{
    internal class DeepMinerPatches
    {
        [HarmonyPatch(typeof(DeepMiner), nameof(DeepMiner.SpawnOre))]
        public static class DeepMiner_SpawnOre_Patch
        {
            [HarmonyPrefix]
            static bool Prefix(DeepMiner __instance)
            {
                __instance._oreSpawnTimeMax = 1;
                __instance._oreSpawnTimeMin = 0.1f;
                __instance._oreSpawnTime = 1;
                if (Plugin.toggleBetterDeepMiner.Value)
                {
                    System.Random rndIce = new System.Random();
                    int randIce = rndIce.Next(Ice.AllIcePrefabs.Count);
                    if (number(0, 3) == 0)
                    {
                        DirtyOre dirtyOre = Thing.Create<DirtyOre>(DeepMiner._spawnOrePrefab, __instance.ExportSlot.Location);
                        dirtyOre.SetQuantity(UnityEngine.Random.Range(__instance._oreAmountPerSpawnMin, __instance._oreAmountPerSpawnMax + 1));
                        dirtyOre.ParentSlot = null;
                        OnServer.MoveToSlot(dirtyOre, __instance.ExportSlot);
                    }
                    if (Plugin.toggleBetterDeepMinerIce.Value)
                    {
                        if (number(0, 3) == 1)
                        {
                            Ice iceOre = Thing.Create<Ice>(Ice.AllIcePrefabs[randIce], __instance.ExportSlot.Location);
                            iceOre.SetQuantity(UnityEngine.Random.Range(__instance._oreAmountPerSpawnMin, __instance._oreAmountPerSpawnMax + 1));
                            iceOre.ParentSlot = null;
                            OnServer.MoveToSlot(iceOre, __instance.ExportSlot);
                        }
                    }
                    if (Plugin.GetOres.Value)
                    {
                        if (number(0, 3) == 1)
                        {
                            Ore processedOre = Thing.Create<Ore>(Ore.AllOrePrefabs[randIce], __instance.ExportSlot.Location);
                            processedOre.SetQuantity(UnityEngine.Random.Range(__instance._oreAmountPerSpawnMin, __instance._oreAmountPerSpawnMax + 1));
                            processedOre.ParentSlot = null;
                            OnServer.MoveToSlot(processedOre, __instance.ExportSlot);
                        }
                    }
                    __instance._timeSinceLastOreSpawn = GameManager.GameTime;
                }
                else
                {
                    DirtyOre dirtyOre = Thing.Create<DirtyOre>(DeepMiner._spawnOrePrefab, __instance.ExportSlot.Location);
                    dirtyOre.SetQuantity(UnityEngine.Random.Range(__instance._oreAmountPerSpawnMin, __instance._oreAmountPerSpawnMax + 1));
                    dirtyOre.ParentSlot = null;
                    OnServer.MoveToSlot(dirtyOre, __instance.ExportSlot);
                    __instance._timeSinceLastOreSpawn = GameManager.GameTime;
                }
                return false;
            }

            static int number(int min, int max)
            {
                System.Random random = new System.Random();
                return random.Next(min, max);
            }
        }
    }
}
