using TrippleTrinity.MechaMorph.Ability;
using TrippleTrinity.MechaMorph.Damage;
using UnityEngine;

namespace TrippleTrinity.MechaMorph.Token
{
    public class TokenCollector : MonoBehaviour
    {
        private Damageable _playerHealth;
        private AreaDamageAbility _areaDamageAbility;

        private void Start()
        {
            _playerHealth = GetComponentInParent<Damageable>(); // Get from Parent
            _areaDamageAbility = FindObjectOfType<AreaDamageAbility>();

            Debug.Log($"TokenCollector initialized. Player Health Component: {_playerHealth}");

            if (_playerHealth == null)
                Debug.LogWarning("TokenCollector: Damageable component missing on Player.");

            if (_areaDamageAbility == null)
                Debug.LogWarning("TokenCollector: AreaDamageAbility not found in scene.");
        }

        public void CollectToken(TokenType type, float value)
        {
            Debug.Log($"Token Collected: {type} | Value: {value}");

            switch (type)
            {
                case TokenType.Health:
                    if (_playerHealth != null)
                    {
                        Debug.Log($"Applying Heal: {value}");
                        _playerHealth.Heal(value);
                    }
                    else
                    {
                        Debug.LogWarning("TokenCollector: Damageable component not found on Player!");
                    }
                    break;

                case TokenType.Cooldown:
                    if (_areaDamageAbility != null)
                    {
                        _areaDamageAbility.CollectToken(); // Calls Area Damage cooldown
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
