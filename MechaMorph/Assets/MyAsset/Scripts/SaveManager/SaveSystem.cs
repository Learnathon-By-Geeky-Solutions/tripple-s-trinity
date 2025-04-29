using System.IO;
using TrippleTrinity.MechaMorph.MyAsset.Scripts.Ui;
using UnityEngine;

namespace TrippleTrinity.MechaMorph.MyAsset.Scripts.SaveManager
{
    public static class SaveSystem
    {
        private static readonly string SavePath = Application.persistentDataPath + "/save.json";

        // Default SaveGame method (auto-generates data)
        public static void SaveGame()
        {
            GameData data = new GameData
            {
                score = ScoreManager.Instance.CurrentScore,
                highScore = ScoreManager.Instance.HighScore,
                tokenCount = TokenUIManager.Instance.GetTotalTokens(),
                areaDamageLevel = UpgradeManager.Instance.AreaDamageLevel,
                boosterCooldownLevel = UpgradeManager.Instance.BoosterCooldownLevel,
                masterVol = SettingsData.MasterVol,
                musicVol = SettingsData.MusicVol,
                sfxVol = SettingsData.SfxVol,
                lobbyVol = SettingsData.LobbyVol,
                graphicsQuality = QualitySettings.GetQualityLevel(),
                gameMode = GameModeManager.Instance.CurrentMode
            };

            SaveGame(data); // Reuse the below method
        }

        // Overloaded SaveGame method that accepts custom GameData
        private static void SaveGame(GameData data)
        {
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(SavePath, json);
            Debug.Log("Game saved (with provided data).");
        }

        public static GameData LoadGame()
        {
            if (!File.Exists(SavePath))
            {
                Debug.LogWarning("No save file found.");
                return null;
            }

            string json = File.ReadAllText(SavePath);
            GameData data = JsonUtility.FromJson<GameData>(json);

            GameModeManager.Instance.SetMode(data.gameMode);
            SettingsData.LoadFromGameData(data);
            ScoreManager.Instance.LoadScore(data.score, data.highScore);
            TokenUIManager.Instance.UpdateTokenCount(data.tokenCount);
            UpgradeManager.Instance.SetUpgrades(data.areaDamageLevel, data.boosterCooldownLevel);

            return data;
        }

        public static void DeleteGame()
        {
            if (File.Exists(SavePath))
            {
                File.Delete(SavePath);
                Debug.Log("Save deleted.");
            }
        }
    }
}
