using TrippleTrinity.MechaMorph.Ability;
using TrippleTrinity.MechaMorph.Token;
using UnityEngine;
using UnityEngine.UI;

namespace TrippleTrinity.MechaMorph.Ui
{
    public class UpgradeUIManager : MonoBehaviour
    {
        [SerializeField] private Button boosterUpgradeButton;
        [SerializeField] private Button areaDamageUpgradeButton;

        private AreaDamageAbility _areaDamageAbility;

        private void Start()
        {
            boosterUpgradeButton?.onClick.AddListener(UpgradeBoosterCooldown);
            areaDamageUpgradeButton?.onClick.AddListener(UpgradeAreaDamage);

            _areaDamageAbility = FindObjectOfType<AreaDamageAbility>();
        }

        private void UpgradeBoosterCooldown()
        {
            if (UpgradeManager.Instance.UpgradeBoosterCooldown())
            {
                Debug.Log("Booster cooldown upgraded!");
                UpgradePanel.Instance.UpdateTotalTokenDisplay(); // Update UI
            }
            else
            {
                Debug.Log("Not enough tokens for booster upgrade.");
            }
        }

        private void UpgradeAreaDamage()
        {
            if (UpgradeManager.Instance.UpgradeAreaDamage())
            {
                Debug.Log("Area damage ability upgraded!");
                _areaDamageAbility?.ApplyUpgrades(1); // Apply upgrade effect
                UpgradePanel.Instance.UpdateTotalTokenDisplay(); // Update UI
            }
            else
            {
                Debug.Log("Not enough tokens for area damage upgrade.");
            }
        }
    }
}