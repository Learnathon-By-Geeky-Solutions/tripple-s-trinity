using TrippleTrinity.MechaMorph.Ui;
using UnityEngine;

namespace TrippleTrinity.MechaMorph.Token
{
    public class UpgradeManager : MonoBehaviour
    {
        private static UpgradeManager _instance;
        private int _upgradePoints;
        private int _upgradeTokenCount;
        private const string TotalTokensKey = "TotalTokens"; // Key for PlayerPrefs
        private const string BoosterLevelKey = "BoosterUpgradeLevel";
        private const string AreaDamageLevelKey = "AreaDamageUpgradeLevel";
        private const string BoosterCostKey = "BoosterUpgradeCost";
        private const string AreaDamageCostKey = "AreaDamageUpgradeCost";
        
        //upgrade levels and costs
        private int _boosterUpgradeLevel;
        private int _areaDamageUpgradeLevel;
        private int _boosterUpgradeCost;
        private int _areaDamageUpgradeCost;
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
            // Ensure this instance persists across scenes
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
                LoadData(); // Reset the session token count
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void AddUpgradePoint()
        {
            _upgradePoints++;
            Debug.Log($"Upgrade Points: {_upgradePoints}");
        }

        public void AddUpgradeToken()
        {
            _upgradeTokenCount++;
            // Add to total tokens across all sessions
            int totalTokens = PlayerPrefs.GetInt(TotalTokensKey, 0) + 1;
            totalTokens++;
            PlayerPrefs.SetInt(TotalTokensKey, totalTokens);
            PlayerPrefs.SetInt(TotalTokensKey, _upgradeTokenCount);
            PlayerPrefs.Save();

            // Update the UI for the current session
            TokenUIManager.Instance?.UpdateTokenCount(_upgradeTokenCount);
        }

        public bool UpgradeBoosterCooldown()
        {
            if (_upgradeTokenCount >= _boosterUpgradeCost)
            {
                _upgradeTokenCount -= _boosterUpgradeCost;
                _boosterUpgradeLevel++;
                _boosterUpgradeCost += 5;

                SaveData();
                
                TokenUIManager.Instance?.UpdateTokenCount(_upgradeTokenCount);
                return true;
            }
            return false;
        }

        public bool UpgradeAreaDamage()
        {
            if (_upgradeTokenCount >= _areaDamageUpgradeCost)
            {
                _upgradeTokenCount -= _areaDamageUpgradeCost;
                _areaDamageUpgradeLevel++;
                _areaDamageUpgradeCost += 5;

                SaveData();
                TokenUIManager.Instance?.UpdateTokenCount(_upgradeTokenCount);
                return true;
            }
            return false;
        }

        public int GetUpgradeTokenCount()
        {
            return _upgradeTokenCount;
        }

        public int GetTotalUpgradeTokenCount()
        {
            return PlayerPrefs.GetInt(TotalTokensKey, 0); // Get the total from PlayerPrefs
        }
        public int GetBoosterUpgradeCost()
        {
            return _boosterUpgradeCost;
        }

        public int GetAreaDamageUpgradeCost()
        {
            return _areaDamageUpgradeCost;
        }

        private void SaveData()
        {
            PlayerPrefs.SetInt(BoosterLevelKey, _boosterUpgradeLevel);
            PlayerPrefs.SetInt(AreaDamageLevelKey, _areaDamageUpgradeLevel);
            PlayerPrefs.SetInt(BoosterCostKey, _boosterUpgradeCost);
            PlayerPrefs.SetInt(AreaDamageCostKey, _areaDamageUpgradeCost);
            PlayerPrefs.SetInt(TotalTokensKey, _upgradeTokenCount);
            PlayerPrefs.Save();
        }

        private void LoadData()
        {
            _boosterUpgradeLevel = PlayerPrefs.GetInt(BoosterLevelKey, 0);
            _areaDamageUpgradeLevel = PlayerPrefs.GetInt(AreaDamageLevelKey, 0);
            _boosterUpgradeCost = PlayerPrefs.GetInt(BoosterCostKey, 5);
            _areaDamageUpgradeCost = PlayerPrefs.GetInt(AreaDamageCostKey, 5);
            _upgradeTokenCount = PlayerPrefs.GetInt(TotalTokensKey, 0);
        }
    }
}