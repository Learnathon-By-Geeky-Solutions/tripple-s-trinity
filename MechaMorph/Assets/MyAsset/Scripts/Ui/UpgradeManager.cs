using UnityEngine;

namespace TrippleTrinity.MechaMorph.MyAsset.Scripts.Ui
{
    public class UpgradeManager : MonoBehaviour
    {
        public static UpgradeManager Instance { get; private set; }

        public int AreaDamageLevel { get; private set; }
        public int BoosterCooldownLevel { get; private set; }
        private int _areaDamageUpgradeCost = 5;
        private int _boosterCooldownUpgradeCost = 3;

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

        public bool UpgradeAreaDamage()
        {
            if (TokenUIManager.Instance.CurrentTokenCount() >= _areaDamageUpgradeCost)
            {
                TokenUIManager.Instance.UpdateTokenCount(TokenUIManager.Instance.CurrentTokenCount() - _areaDamageUpgradeCost);
                AreaDamageLevel++;
                _areaDamageUpgradeCost += 2;
                return true;
            }
            return false;
        }

        public bool UpgradeBoosterCooldown()
        {
            if (TokenUIManager.Instance.CurrentTokenCount() >= _boosterCooldownUpgradeCost)
            {
                TokenUIManager.Instance.UpdateTokenCount(TokenUIManager.Instance.CurrentTokenCount() - _boosterCooldownUpgradeCost);
                BoosterCooldownLevel++;
                _boosterCooldownUpgradeCost += 2;
                return true;
            }
            return false;
        }

        public int GetAreaDamageUpgradeCost() => _areaDamageUpgradeCost;
        public int GetBoosterUpgradeCost() => _boosterCooldownUpgradeCost;

        public static int GetTotalUpgradeTokenCount()
        {
            throw new System.NotImplementedException();
        }

        // 🔧 Fix: Add LoadUpgradeLevels method
        public void LoadUpgradeLevels(int areaDamage, int boosterCooldown)
        {
            AreaDamageLevel = areaDamage;
            BoosterCooldownLevel = boosterCooldown;
        }
        public void SetUpgrades(int areaLevel, int cooldownLevel)
        {
            AreaDamageLevel = areaLevel;
            BoosterCooldownLevel = cooldownLevel;
        }
        public void ResetAllUpgrades()
        {
            AreaDamageLevel = 0;
            BoosterCooldownLevel = 0;

            Debug.Log("All upgrade levels reset to 0.");
        }

    }
}
