using TrippleTrinity.MechaMorph.Ability;
using TrippleTrinity.MechaMorph.Ui;
using UnityEngine;

namespace TrippleTrinity.MechaMorph.Weapons
{
    public class TakeDamage : MonoBehaviour
    {
        [SerializeField] private float enemyHealth = 50f;
        [SerializeField] private int scoreValue = 10; // Points awarded when this enemy dies
        private AreaDamageAbility _areaDamageAbility; // Reference to the AreaDamageAbility script

        private void Start()
        {
            // Find the AreaDamageAbility in the scene
            _areaDamageAbility = FindObjectOfType<AreaDamageAbility>();
        }

        public void Damage(float amount)
        {
            enemyHealth -= amount;
            if (enemyHealth <= 0f)
            {
                Die(gameObject);
            }
        }

        void Die(GameObject obj)
        {
            // Register the enemy kill to increase ability cooldown
            if (_areaDamageAbility != null)
            {
                _areaDamageAbility.RegisterEnemyKill();
            }

            // Add score (same as before)
            ScoreManager.Instance.AddScore(scoreValue);

            // Destroy the enemy object
            Destroy(obj);
        }
    }
}