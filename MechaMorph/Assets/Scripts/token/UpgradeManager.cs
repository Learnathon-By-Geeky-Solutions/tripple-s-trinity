using TrippleTrinity.MechaMorph.SaveManager;
using TrippleTrinity.MechaMorph.Ui;
using UnityEngine;

namespace TrippleTrinity.MechaMorph.Token
{
    public class UpgradeManager : MonoBehaviour
    {
        private static UpgradeManager _instance;
        private GameData _gameData;
        public static UpgradeManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("UpgradeManager instance is null.");
                }
                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
                _gameData = SaveSystem.LoadGame();
                Debug.Log("Loaded upgrade data from save system");
            }
            else
            {
                Destroy(gameObject);
            }
        }


        public void AddUpgradeToken()
        {
            _gameData.tokenCount++;
            TokenUIManager.Instance?.UpdateTokenCount(_gameData.tokenCount);
            SaveSystem.SaveGame(_gameData);
        }

        

        public bool UpgradeBoosterCooldown()
        {
            if (_gameData.tokenCount >= _gameData.boosterUpgradeCost)
            {
                _gameData.tokenCount -= _gameData.boosterUpgradeCost;
                _gameData.boosterUpgradeLevel++;
                _gameData.boosterUpgradeCost += 5;

                SaveSystem.SaveGame(_gameData);
                return true;
            }
            return false;
        }

        public bool UpgradeAreaDamage()
        {
            if (_gameData.tokenCount >= _gameData.boosterUpgradeCost)
            {
                _gameData.tokenCount -= _gameData.areaDamageUpgradeCost;
                _gameData.areaDamageUpgradeLevel++;
                _gameData.areaDamageUpgradeCost += 5;

                SaveSystem.SaveGame(_gameData);
                return true;
            }
            return false;
        }

        public int GetTotalUpgradeTokenCount()
        {
            return _gameData.tokenCount; // Now gets from JSON-saved data
        }

        public int GetUpgradeTokenCount()
        {
            return _gameData.tokenCount;
        }

        public int GetBoosterUpgradeCost()
        {
            return _gameData.boosterUpgradeCost;
        }

        public int GetAreaDamageUpgradeCost()
        {
            return _gameData.areaDamageUpgradeCost;
        }

        // New methods for UI to get upgrade levels
        public int GetBoosterUpgradeLevel()
        {
            return _gameData.boosterUpgradeLevel;
        }

        public int GetAreaDamageUpgradeLevel()
        {
            return _gameData.areaDamageUpgradeLevel;
        }

        

    }
}