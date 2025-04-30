using TrippleTrinity.MechaMorph.MyAsset.Scripts.SaveManager;
using UnityEngine;
using TrippleTrinity.MechaMorph.SaveManager;
using TrippleTrinity.MechaMorph.MyAsset.Scripts.Ui;

namespace TrippleTrinity.MechaMorph.Token
{
    public class UpgradeManager : MonoBehaviour
    {
        public static UpgradeManager Instance { get; private set; }

        [Header("Booster Upgrade")]
        [SerializeField] private int baseBoosterCost = 15;
        [SerializeField] private float boosterCostMultiplier = 1.4f;

        [Header("Area Damage Upgrade")]
        [SerializeField] private int baseAreaDamageCost = 20;
        [SerializeField] private float areaDamageCostMultiplier = 1.5f;

        private const int maxUpgradeLevel = 10;
        private GameData gameData;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                gameData = SaveSystem.LoadGame() ?? new GameData();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Save() => SaveSystem.SaveGame(gameData);

        public int GetBoosterUpgradeCost() =>
            Mathf.RoundToInt(baseBoosterCost * Mathf.Pow(boosterCostMultiplier, gameData.boosterUpgradeLevel));

        public int GetAreaDamageUpgradeCost() =>
            Mathf.RoundToInt(baseAreaDamageCost * Mathf.Pow(areaDamageCostMultiplier, gameData.areaDamageUpgradeLevel));

        public bool CanUpgradeBooster() =>
            gameData.boosterUpgradeLevel < maxUpgradeLevel && gameData.tokenCount >= GetBoosterUpgradeCost();

        public bool CanUpgradeAreaDamage() =>
            gameData.areaDamageUpgradeLevel < maxUpgradeLevel && gameData.tokenCount >= GetAreaDamageUpgradeCost();

        public bool UpgradeBoosterCooldown()
        {
            if (CanUpgradeBooster())
            {
                gameData.tokenCount -= GetBoosterUpgradeCost();
                gameData.boosterUpgradeLevel++;
                Save();
                return true;
            }
            return false;
        }

        public bool UpgradeAreaDamage()
        {
            if (CanUpgradeAreaDamage())
            {
                gameData.tokenCount -= GetAreaDamageUpgradeCost();
                gameData.areaDamageUpgradeLevel++;
                Save();
                return true;
            }
            return false;
        }

        public void AddUpgradeToken()
        {
            gameData.tokenCount++;
            Save();
            UpgradePanel.Instance?.UpdateTotalTokenDisplay();
        }

        public void AddUpgradePoint()
        {
            // Optional: use for skill points later
        }

        public int GetTotalUpgradeTokenCount() => gameData.tokenCount;
        public int GetBoosterLevel() => gameData.boosterUpgradeLevel;
        public int GetAreaDamageLevel() => gameData.areaDamageUpgradeLevel;
    }
}
