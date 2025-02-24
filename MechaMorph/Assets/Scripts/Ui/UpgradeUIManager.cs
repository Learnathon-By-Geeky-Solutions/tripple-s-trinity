using TrippleTrinity.MechaMorph.Ability;
using TrippleTrinity.MechaMorph.Token;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TrippleTrinity.MechaMorph.Ui
{
    public class UpgradeUIManager : MonoBehaviour
    {
        public TMP_Text tokenCountText;
        public Button boosterUpgradeButton;
        public Button areaDamageUpgradeButton;
    
        private AreaDamageAbility _areaDamageAbility;

        private void Start()
        {
            UpdateTokenUI();
            boosterUpgradeButton.onClick.AddListener(UpgradeBoosterCooldown);
            areaDamageUpgradeButton.onClick.AddListener(UpgradeAreaDamage);
        
            _areaDamageAbility = FindObjectOfType<AreaDamageAbility>();
        }

        private void UpdateTokenUI()
        {
            tokenCountText.text = "Tokens: " + UpgradeManager.Instance.GetUpgradeTokenCount();
        }

        private void UpgradeBoosterCooldown()
        {
            if (UpgradeManager.Instance.UpgradeBoosterCooldown())
            {
                Debug.Log("Booster cooldown upgraded!");
                UpdateTokenUI();
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
                _areaDamageAbility.ApplyUpgrades(1); // Apply upgrade
                UpdateTokenUI();
            }
            else
            {
                Debug.Log("Not enough tokens for area damage upgrade.");
            }
        }
    }
}