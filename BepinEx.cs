using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;

namespace BetterDeepMiner
{
    [BepInPlugin(PLUGIN_GUID, PLUGIN_NAME, PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public const string PLUGIN_GUID = "modder.ihatetn931.StationeersMods.BetterDeepMiner";
        public const string PLUGIN_NAME = "BetterDeepMiner";
        public const string PLUGIN_VERSION = "1.0.2";

        internal static ConfigEntry<bool> toggleBetterDeepMiner;
        internal static ConfigEntry<bool> toggleBetterDeepMinerIce;
        internal static ConfigEntry<bool> GetOres;

        void Awake()
        {
            Logger.LogInfo($"Plugin {PLUGIN_NAME} is loading!");
            Harmony harmony = new Harmony(PLUGIN_GUID);
            harmony.PatchAll();
            AddSettings();
            Logger.LogInfo($"Plugin {PLUGIN_NAME} Verison {PLUGIN_VERSION} is loaded!");
        }

        void AddSettings()
        {
            toggleBetterDeepMiner = Config.Bind("BetterDeepMiner General", "Enable BetterDeepMiner", true, new ConfigDescription("Enable BetterDeepMiner To Use all the settings on this config", null, new ConfigurationManagerAttributes { Browsable = true }));
            toggleBetterDeepMinerIce = Config.Bind("BetterDeepMiner General", "Enable BetterDeepMiner Ice", true, new ConfigDescription("Enable BetterDeepMiner to output ice and dirty ore", null, new ConfigurationManagerAttributes { Browsable = true }));
            GetOres = Config.Bind("BetterDeepMiner General", "Enable BetterDeepMiner Ores", true, new ConfigDescription("With this enabled you will get ores and not dirty ore but takes longer", null, new ConfigurationManagerAttributes { Browsable = true }));
        }
    }
}

