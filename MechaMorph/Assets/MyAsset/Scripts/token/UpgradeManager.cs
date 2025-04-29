using TrippleTrinity.MechaMorph.MyAsset.Scripts.Ui;
using TrippleTrinity.MechaMorph.MyAsset.Scripts.SaveManager;
using UnityEngine;

namespace TrippleTrinity.MechaMorph.MyAsset.Scripts.Token
{
    public class UpgradeManager : MonoBehaviour
    {
        public static UpgradeManager Instance { get; private set; }

        private int _upgradeTokenCount;
        private int _boosterUpgradeLevel;
        private int _areaDamageUpgradeLevel;
        private int _boosterUpgradeCost = 5;
        private int _areaDamageUpgradeCost = 5;

        public int BoosterCooldownLevel => _boosterUpgradeLevel;
        public int AreaDamageLevel => _areaDamageUpgradeLevel;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            LoadFromSave(); // Load upgrade levels and token count from SaveSystem
        }

        public void AddUpgradeToken()
        {
            _upgradeTokenCount++;
            TokenUIManager.Instance?.UpdateTokenCount(_upgradeTokenCount);
        }

        public bool UpgradeBoosterCooldown()
        {
            if (_upgradeTokenCount >= _boosterUpgradeCost)
            {
                _upgradeTokenCount -= _boosterUpgradeCost;
                _boosterUpgradeLevel++;
                _boosterUpgradeCost += 5;
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
                TokenUIManager.Instance?.UpdateTokenCount(_upgradeTokenCount);
                return true;
            }
            return false;
        }

        public int GetUpgradeTokenCount() => _upgradeTokenCount;
        public int GetBoosterUpgradeCost() => _boosterUpgradeCost;
        public int GetAreaDamageUpgradeCost() => _areaDamageUpgradeCost;

        // Called by SaveSystem when loading game data
        public void LoadFromSave()
        {
            GameData data = SaveSystem.LoadGame();
            if (data == null) return;

            _boosterUpgradeLevel = data.boosterCooldownLevel;
            _areaDamageUpgradeLevel = data.areaDamageLevel;
            _upgradeTokenCount = data.tokenCount;

            _boosterUpgradeCost = 5 + _boosterUpgradeLevel * 5;
            _areaDamageUpgradeCost = 5 + _areaDamageUpgradeLevel * 5;

            TokenUIManager.Instance?.UpdateTokenCount(_upgradeTokenCount);
        }

        public void SetUpgrades(int areaLevel, int boosterLevel)
        {
            _areaDamageUpgradeLevel = areaLevel;
            _boosterUpgradeLevel = boosterLevel;

            _areaDamageUpgradeCost = 5 + areaLevel * 5;
            _boosterUpgradeCost = 5 + boosterLevel * 5;

            Debug.Log($"SetUpgrades called: Area={areaLevel}, Booster={boosterLevel}");
        }

        public void ResetAllUpgrades()
        {
            _upgradeTokenCount = 0;
            _boosterUpgradeLevel = 0;
            _areaDamageUpgradeLevel = 0;
            _boosterUpgradeCost = 5;
            _areaDamageUpgradeCost = 5;

            TokenUIManager.Instance?.UpdateTokenCount(_upgradeTokenCount);
            Debug.Log("Upgrades reset.");
        }
    }
}
