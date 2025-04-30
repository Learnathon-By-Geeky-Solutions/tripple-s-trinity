using System.IO;
using UnityEngine;

namespace TrippleTrinity.MechaMorph.MyAsset.Scripts.SaveManager
{
    public static class SaveSystem
    {
        private static readonly string gamePath = Path.Combine(Application.persistentDataPath, "save.json");
        private static readonly string upgradePath = Path.Combine(Application.persistentDataPath, "upgrades.json");
        private static readonly string settingsPath = Path.Combine(Application.persistentDataPath, "settings.json");

        // Save and Load Game Data
        public static void SaveGame(GameData data)
        {
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(gamePath, json);
            Debug.Log("Game saved to: " + gamePath);
        }

        public static GameData LoadGame()
        {
            if (File.Exists(gamePath))
            {
                string json = File.ReadAllText(gamePath);
                return JsonUtility.FromJson<GameData>(json);
            }

            Debug.LogWarning("No game save file found at: " + gamePath);
            return new GameData(); // return default if not found
        }

        public static void DeleteGame()
        {
            if (File.Exists(gamePath))
            {
                File.Delete(gamePath);
                Debug.Log("Game data deleted from: " + gamePath);
            }
        }

        // Save and Load Upgrade Data
        public static void SaveUpgrades(GameData data)
        {
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(upgradePath, json);
            Debug.Log("Upgrade data saved to: " + upgradePath);
        }

        public static GameData LoadUpgrades()
        {
            if (File.Exists(upgradePath))
            {
                string json = File.ReadAllText(upgradePath);
                return JsonUtility.FromJson<GameData>(json);
            }

            Debug.LogWarning("No upgrade data found at: " + upgradePath);
            return new GameData(); // return default if not found
        }

        public static void DeleteUpgradeData()
        {
            if (File.Exists(upgradePath))
            {
                File.Delete(upgradePath);
                Debug.Log("Upgrade data deleted from: " + upgradePath);
            }
        }

        // Save and Load Settings
        public static void SaveSettings(SettingsData data)
        {
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(settingsPath, json);
            Debug.Log("Settings saved to: " + settingsPath);
        }

        public static SettingsData LoadSettings()
        {
            if (File.Exists(settingsPath))
            {
                string json = File.ReadAllText(settingsPath);
                return JsonUtility.FromJson<SettingsData>(json);
            }

            Debug.LogWarning("No settings file found at: " + settingsPath);
            return new SettingsData(); // return default if not found
        }

        public static void DeleteSettingsData()
        {
            if (File.Exists(settingsPath))
            {
                File.Delete(settingsPath);
                Debug.Log("Settings data deleted from: " + settingsPath);
            }
        }
    }
}
