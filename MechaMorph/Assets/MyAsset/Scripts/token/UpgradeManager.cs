using UnityEngine;
using TrippleTrinity.MechaMorph.SaveManager;
using TrippleTrinity.MechaMorph.Ui;
using TrippleTrinity.MechaMorph.MyAsset.Scripts.Ui;

namespace TrippleTrinity.MechaMorph.Token
{
    public class UpgradeManager : MonoBehaviour
    {
        private static UpgradeManager _instance;
        public static UpgradeManager Instance => _instance ?? throw new System.Exception("UpgradeManager is null!");

        private int _upgradePoints;
        private int _upgradeTokenCount;

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
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
                LoadData();
                _upgradeTokenCount = 0; // reset session tokens
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
            _totalTokens++;
            SaveData();
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
        public int GetTotalUpgradeTokenCount() => _totalTokens;
        public int GetBoosterUpgradeCost() => _boosterUpgradeCost;
        public int GetAreaDamageUpgradeCost() => _areaDamageUpgradeCost;

        private void SaveData()
        {
            UpgradeData data = new UpgradeData
            {
                boosterUpgradeLevel = _boosterUpgradeLevel,
                areaDamageUpgradeLevel = _areaDamageUpgradeLevel,
                boosterUpgradeCost = _boosterUpgradeCost,
                areaDamageUpgradeCost = _areaDamageUpgradeCost,
                totalUpgradeTokens = _totalTokens
            };

            SaveSystem.SaveUpgrades(data);
        }

        private void LoadData()
        {
            UpgradeData data = SaveSystem.LoadUpgrades();

            if (data != null)
            {
                _boosterUpgradeLevel = data.boosterUpgradeLevel;
                _areaDamageUpgradeLevel = data.areaDamageUpgradeLevel;
                _boosterUpgradeCost = data.boosterUpgradeCost;
                _areaDamageUpgradeCost = data.areaDamageUpgradeCost;
                _totalTokens = data.totalUpgradeTokens;
            }
        }

        public void ResetAllUpgrades()
        {
            _boosterUpgradeLevel = 0;
            _areaDamageUpgradeLevel = 0;
            _boosterUpgradeCost = 5;
            _areaDamageUpgradeCost = 5;
            _upgradeTokenCount = 0;
            _totalTokens = 0;
            SaveSystem.DeleteUpgradeData();

            TokenUIManager.Instance?.UpdateTokenCount(_upgradeTokenCount);
            Debug.Log("Upgrades reset.");
        }
    }
}
