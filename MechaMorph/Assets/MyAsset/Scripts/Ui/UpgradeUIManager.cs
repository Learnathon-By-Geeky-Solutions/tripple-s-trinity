using TrippleTrinity.MechaMorph.Ability;
using TrippleTrinity.MechaMorph.Token;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UpgradeManager = TrippleTrinity.MechaMorph.MyAsset.Scripts.Ui.UpgradeManager;

namespace TrippleTrinity.MechaMorph.Ui
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
            // Register button click listeners
            boosterUpgradeButton?.onClick.AddListener(UpgradeBoosterCooldown);
            areaDamageUpgradeButton?.onClick.AddListener(UpgradeAreaDamage);

            // Get reference to the AreaDamageAbility for applying upgrades
            _areaDamageAbility = FindObjectOfType<AreaDamageAbility>();

            // Initialize UI with current upgrade costs
            UpdateUI();
        }

        private void UpgradeBoosterCooldown()
        {
            // Attempt to upgrade the booster cooldown
            if (UpgradeManager.Instance.UpgradeBoosterCooldown())
            {
                int newUpgradeLevel = UpgradeManager.Instance.GetBoosterUpgradeCost();
                Debug.Log($"Booster cooldown upgraded to level {newUpgradeLevel}!");

                // Apply the new upgrade (adjust game logic accordingly)
                _areaDamageAbility.ApplyUpgrades(newUpgradeLevel);

                // Update UI after upgrade
                UpdateUI(); 
            }
            else
            {
                Debug.Log("Not enough tokens for booster cooldown upgrade.");
            }
        }

        private void UpgradeAreaDamage()
        {
            // Attempt to upgrade the area damage
            if (UpgradeManager.Instance.UpgradeAreaDamage())
            {
                int newUpgradeLevel = UpgradeManager.Instance.GetAreaDamageUpgradeCost();
                Debug.Log($"Area damage ability upgraded to level {newUpgradeLevel}!");

                // Apply the new upgrade (adjust game logic accordingly)
                _areaDamageAbility.ApplyUpgrades(newUpgradeLevel);

                // Update UI after upgrade
                UpdateUI();
            }
            else
            {
                Debug.Log("Not enough tokens for area damage upgrade.");
            }
        }

        // Update the UI with the current upgrade costs
        public void UpdateUI()
        {
            areaDamageUpgradeCostText.text = $"Upgrade Cost: {UpgradeManager.Instance.GetAreaDamageUpgradeCost()}";
            coolDownUpgradeCostText.text = $"Upgrade Cost: {UpgradeManager.Instance.GetBoosterUpgradeCost()}";
        }
    }
}
