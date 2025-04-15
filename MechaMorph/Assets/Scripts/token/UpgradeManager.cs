using TrippleTrinity.MechaMorph.Ui;
using UnityEngine;

namespace TrippleTrinity.MechaMorph.Token
{
    public class UpgradeManager : MonoBehaviour
    {
        public static UpgradeManager Instance { get; private set; }

        private int _upgradePoints;
        private int _upgradeTokenCount;

        private const string TotalTokensKey = "TotalTokens";
        private const string BoosterLevelKey = "BoosterUpgradeLevel";
        private const string AreaDamageLevelKey = "AreaDamageUpgradeLevel";
        private const string BoosterCostKey = "BoosterUpgradeCost";
        private const string AreaDamageCostKey = "AreaDamageUpgradeCost";

        private int _boosterUpgradeLevel;
        private int _areaDamageUpgradeLevel;
        private int _boosterUpgradeCost;
        private int _areaDamageUpgradeCost;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                LoadData();
                _upgradeTokenCount = 0;
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

            int totalTokens = PlayerPrefs.GetInt(TotalTokensKey, 0) + 1;
            PlayerPrefs.SetInt(TotalTokensKey, totalTokens);
            PlayerPrefs.Save();

            Debug.Log($"Token Added! Session Tokens: {_upgradeTokenCount}, Total Tokens: {totalTokens}");
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

        public int GetUpgradeTokenCount() => _upgradeTokenCount;

        public int GetTotalUpgradeTokenCount()
        {
            return PlayerPrefs.GetInt(TotalTokensKey, 0);
        }

        public int GetBoosterUpgradeCost() => _boosterUpgradeCost;

        public int GetAreaDamageUpgradeCost() => _areaDamageUpgradeCost;

        private void SaveData()
        {
            PlayerPrefs.SetInt(BoosterLevelKey, _boosterUpgradeLevel);
            PlayerPrefs.SetInt(AreaDamageLevelKey, _areaDamageUpgradeLevel);
            PlayerPrefs.SetInt(BoosterCostKey, _boosterUpgradeCost);
            PlayerPrefs.SetInt(AreaDamageCostKey, _areaDamageUpgradeCost);
            PlayerPrefs.Save();
        }

        private void LoadData()
        {
            _boosterUpgradeLevel = PlayerPrefs.GetInt(BoosterLevelKey, 0);
            _areaDamageUpgradeLevel = PlayerPrefs.GetInt(AreaDamageLevelKey, 0);
            _boosterUpgradeCost = PlayerPrefs.GetInt(BoosterCostKey, 5);
            _areaDamageUpgradeCost = PlayerPrefs.GetInt(AreaDamageCostKey, 5);

            _upgradeTokenCount = 0; // Always reset session tokens
            Debug.Log($"Loaded upgrades. Session Tokens Reset to: {_upgradeTokenCount}");
        }
    }
}
