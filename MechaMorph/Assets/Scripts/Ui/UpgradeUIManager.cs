using TrippleTrinity.MechaMorph.Ability;
using TrippleTrinity.MechaMorph.Token;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TrippleTrinity.MechaMorph.Ui
{
    public class UpgradeUIManager : MonoBehaviour
    {
        [SerializeField] private Button boosterUpgradeButton;
        [SerializeField] private Button areaDamageUpgradeButton;
        [SerializeField] private TMP_Text areaDamageUpgradeCostText;
        [SerializeField] private TMP_Text coolDownUpgradeCostText;

        [SerializeField] private AreaDamageAbility _areaDamageAbility;

        private void Start()
        {
            if (UpgradeManager.Instance == null)
            {
                Debug.LogError("UpgradeManager instance not found!");
                return;
            }

            if (_areaDamageAbility == null)
            {
                Debug.LogError("AreaDamageAbility missing! Disabling upgrade functionality.");
                /*_areaDamageAbility = FindObjectOfType<AreaDamageAbility>();
                if (_areaDamageAbility == null)
                {
                    Debug.LogError("AreaDamageAbility missing! Disabling upgrade functionality.");
                    boosterUpgradeButton.interactable = false;
                    areaDamageUpgradeButton.interactable = false;
                    return;
                }*/
            }

            UpdateUI();
        }

        private void UpgradeBoosterCooldown()
        {
            if (UpgradeManager.Instance == null || _areaDamageAbility == null)
            {
                Debug.LogError("Required components missing!");
                return;
            }

            if (UpgradeManager.Instance.UpgradeBoosterCooldown())
            {
                // Use GetBoosterUpgradeLevel() instead of GetBoosterUpgradeCost()
                int newUpgradeLevel = UpgradeManager.Instance.GetBoosterUpgradeLevel();
                Debug.Log($"Booster upgraded to level {newUpgradeLevel}!");

                _areaDamageAbility.ApplyUpgrades(newUpgradeLevel);
                UpdateUI();
            }
            else
            {
                Debug.Log("Not enough tokens for booster upgrade.");
            }
        }

        private void UpgradeAreaDamage()
        {
            if (UpgradeManager.Instance == null || _areaDamageAbility == null)
            {
                Debug.LogError("Required components missing!");
                return;
            }

            if (UpgradeManager.Instance.UpgradeAreaDamage())
            {
                // Use GetAreaDamageUpgradeLevel() instead of GetAreaDamageUpgradeCost()
                int newUpgradeLevel = UpgradeManager.Instance.GetAreaDamageUpgradeLevel();
                Debug.Log($"Area damage upgraded to level {newUpgradeLevel}!");

                _areaDamageAbility.ApplyUpgrades(newUpgradeLevel);
                UpdateUI();
            }
            else
            {
                Debug.Log("Not enough tokens for area damage upgrade.");
            }
        }
        private void UpdateUI()
        {
            if (UpgradeManager.Instance == null) return;

            if (areaDamageUpgradeCostText != null)
                areaDamageUpgradeCostText.text = $"Upgrade Cost: {UpgradeManager.Instance.GetAreaDamageUpgradeCost()}";

            if (coolDownUpgradeCostText != null)
                coolDownUpgradeCostText.text = $"Upgrade Cost: {UpgradeManager.Instance.GetBoosterUpgradeCost()}";
        }
        
    }
}