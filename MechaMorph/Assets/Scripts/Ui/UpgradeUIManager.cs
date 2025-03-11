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

        private AreaDamageAbility _areaDamageAbility;

        private void Start()
        {
            boosterUpgradeButton?.onClick.AddListener(UpgradeBoosterCooldown);
            areaDamageUpgradeButton?.onClick.AddListener(UpgradeAreaDamage);

            _areaDamageAbility = FindObjectOfType<AreaDamageAbility>();

            UpdateUI();
        }

        private void UpgradeBoosterCooldown()
        {
            if (UpgradeManager.Instance.UpgradeBoosterCooldown())
            {
                int newUpgradeLevel = UpgradeManager.Instance.GetBoosterUpgradeCost();
                Debug.Log($"Area damage ability upgraded to level {newUpgradeLevel}!");

                _areaDamageAbility.ApplyUpgrades(newUpgradeLevel);

                UpdateUI(); // Refresh UI after upgrade
            }
            else
            {
                Debug.Log("Not enough tokens for area damage upgrade.");
            }
        }

        private void UpgradeAreaDamage()
        {
            if (UpgradeManager.Instance.UpgradeAreaDamage())
            {
                int newUpgradeLevel = UpgradeManager.Instance.GetAreaDamageUpgradeCost();
                Debug.Log($"Area damage ability upgraded to level {newUpgradeLevel}!");

                _areaDamageAbility.ApplyUpgrades(newUpgradeLevel);

                UpdateUI(); // Refresh UI after upgrade
            }
            else
            {
                Debug.Log("Not enough tokens for area damage upgrade.");
            }
        }
        private void UpdateUI()
        {
            areaDamageUpgradeCostText.text = $"Upgrade Cost: {UpgradeManager.Instance.GetAreaDamageUpgradeCost()}";
            coolDownUpgradeCostText.text = $"Upgrade Cost:{UpgradeManager.Instance.GetBoosterUpgradeCost()}";
        }
        
    }
}