using System.IO;
using TrippleTrinity.MechaMorph.MyAsset.Scripts.SaveManager;
using UnityEngine;

namespace TrippleTrinity.MechaMorph.SaveManager
{
    public static class SaveSystem
    {
        private static string _savePath = Application.persistentDataPath + "/save.json";
        private static readonly string upgradePath = Application.persistentDataPath + "/upgrades.json"; 
        private static readonly string settingsPath = Application.persistentDataPath + "/settings.json";




        public static void SaveGame(GameData data)
        {
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(_savePath, json);
            Debug.Log("Game Saved to: " + _savePath);
        }

        public static GameData LoadGame()
        {
            if (File.Exists(_savePath))
            {
                string json = File.ReadAllText(_savePath);
                GameData data = JsonUtility.FromJson<GameData>(json);
                Debug.Log("Game Loaded from: " + _savePath);
                return data;
            }
            else
            {
                Debug.LogWarning("Save file not found");
                return null;
            }
        }

        public static void DeleteGame()
        {
            if (File.Exists(_savePath))
            {
                File.Delete(_savePath);
                Debug.Log("Game Deleted from: " + _savePath);
            }
        }

        public static void SaveUpgrades(UpgradeData data)
        {
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(upgradePath, json);
        }

        public static UpgradeData LoadUpgrades()
        {
            if (File.Exists(upgradePath))
            {
                string json = File.ReadAllText(upgradePath);
                return JsonUtility.FromJson<UpgradeData>(json);
            }

            return null;
        }

        public static void DeleteUpgradeData()
        {
            if (File.Exists(upgradePath))
            {
                File.Delete(upgradePath);
            }
        }

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

            return new SettingsData(); // default values
        }
    }
}