using TMPro;
using TrippleTrinity.MechaMorph.MyAsset.Scripts.Ability;
using TrippleTrinity.MechaMorph.Token;
using UnityEngine;
using UnityEngine.UI;

namespace TrippleTrinity.MechaMorph.MyAsset.Scripts.Ui
{
    public class UpgradeUIManager : MonoBehaviour
    {
        [SerializeField] private Button boosterUpgradeButton;
        [SerializeField] private Button areaDamageUpgradeButton;
        [SerializeField] private TMP_Text areaDamageUpgradeCostText;
        [SerializeField] private TMP_Text coolDownUpgradeCostText;

        private AreaDamageAbility _areaDamageAbility;

        private void Start()
        {
            boosterUpgradeButton?.onClick.AddListener(UpgradeBoosterCooldown);
            areaDamageUpgradeButton?.onClick.AddListener(UpgradeAreaDamage);

            _areaDamageAbility = AreaDamageAbility.Instance;

            UpdateUI();
        }

        private void UpgradeBoosterCooldown()
        {
            if (UpgradeManager.Instance.UpgradeBoosterCooldown())
            {
                int newUpgradeCost = UpgradeManager.Instance.GetBoosterUpgradeCost();
                Debug.Log($"Booster cooldown upgrade purchased! Next upgrade cost: {newUpgradeCost}");

                UpdateUI(); 
            }
            else
            {
                Debug.Log("Not enough tokens for booster cooldown upgrade.");
            }
        }

        private void UpgradeAreaDamage()
        {
            if (UpgradeManager.Instance.UpgradeAreaDamage())
            {
                int newUpgradeCost = UpgradeManager.Instance.GetAreaDamageUpgradeCost();
                Debug.Log($"Area damage upgrade purchased! Next upgrade cost: {newUpgradeCost}");

                _areaDamageAbility?.ApplyUpgrades(newUpgradeCost);

                UpdateUI();
            }
            else
            {
                Debug.Log("Not enough tokens for area damage upgrade.");
            }
        }

        public void UpdateUI()
        {
            areaDamageUpgradeCostText.text = $"Upgrade Cost: {UpgradeManager.Instance.GetAreaDamageUpgradeCost()}";
            coolDownUpgradeCostText.text = $"Upgrade Cost: {UpgradeManager.Instance.GetBoosterUpgradeCost()}";
        }
    }
}
