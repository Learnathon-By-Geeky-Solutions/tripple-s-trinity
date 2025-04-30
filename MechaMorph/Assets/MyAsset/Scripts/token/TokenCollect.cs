using TrippleTrinity.MechaMorph.Damage;
using TrippleTrinity.MechaMorph.MyAsset.Scripts.Ability;
using UnityEngine;

namespace TrippleTrinity.MechaMorph.Token
{
    public class TokenCollector : MonoBehaviour
    {
        private Damageable playerHealth;
        private AreaDamageAbility areaDamageAbility;

        private void Start()
        {
            playerHealth = GetComponentInParent<Damageable>(); // Get from Parent
            areaDamageAbility = FindObjectOfType<AreaDamageAbility>();

            Debug.Log($"TokenCollector initialized. Player Health Component: {playerHealth}");

            if (playerHealth == null)
                Debug.LogWarning("TokenCollector: Damageable component missing on Player.");

            if (areaDamageAbility == null)
                Debug.LogWarning("TokenCollector: AreaDamageAbility not found in scene.");
        }

        public void CollectToken(TokenType type, float value)
        {
            Debug.Log($"Token Collected: {type} | Value: {value}");

            switch (type)
            {
                case TokenType.Health:
                    if (playerHealth != null)
                    {
                        Debug.Log($"Applying Heal: {value}");
                        playerHealth.Heal(value);
                    }
                    else
                    {
                        Debug.LogWarning("TokenCollector: Damageable component not found on Player!");
                    }
                    break;

                case TokenType.Cooldown:
                    if (areaDamageAbility != null)
                    {
                        areaDamageAbility.CollectToken(); // Calls Area Damage cooldown
                    }
                    else
                    {
                        Debug.LogWarning("TokenCollector: AreaDamageAbility instance is null!");
                    }
                    break;

                case TokenType.Upgrade:
                    if (UpgradeManager.Instance != null)
                    {
                        UpgradeManager.Instance.AddUpgradePoint();
                        UpgradeManager.Instance.AddUpgradeToken();
                    }
                    else
                    {
                        Debug.LogError("TokenCollector: UpgradeManager instance is null!");
                    }
                    break;
            }
        }
    }
}
