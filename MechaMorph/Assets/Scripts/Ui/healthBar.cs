using UnityEngine;
using UnityEngine.UI;

namespace TrippleTrinity.MechaMorph.Ui
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private float maxHealth = 100f;
        [SerializeField] private Image healthBarFill;
        [SerializeField] private float fillSpeed = 5f;
        [SerializeField] private float colorChangeSpeed = 10f; // Faster color change
        [SerializeField] private float damageAmount = 10f;
        [SerializeField] private float healAmount = 10f;
        
        private float _currentHealth;
        private float _targetFillAmount;
        private Color _targetColor;

        void Start()
        {
            _currentHealth = maxHealth;
            _targetFillAmount = 1f; // Full health
            _targetColor = Color.green;
            UpdateHealthBarColor();
        }

        void Update()
        {
            if (healthBarFill != null)
            {
                // Smooth fill transition
                healthBarFill.fillAmount = Mathf.MoveTowards(healthBarFill.fillAmount, _targetFillAmount, Time.deltaTime * fillSpeed);
                
                // Faster smooth color transition
                healthBarFill.color = Color.Lerp(healthBarFill.color, _targetColor, Time.deltaTime * colorChangeSpeed);
            }

            // Test Damage: Press 'P' to decrease health
            if (Input.GetKeyDown(KeyCode.P))
            {
                UpdateHealth(-damageAmount);
            }

            // Test Heal: Press 'O' to increase health
            if (Input.GetKeyDown(KeyCode.O))
            {
                UpdateHealth(healAmount);
            }
        }

        private void UpdateHealth(float amount)
        {
            _currentHealth += amount;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, maxHealth);
            _targetFillAmount = _currentHealth / maxHealth;
            UpdateHealthBarColor();
        }

        private void UpdateHealthBarColor()
        {
            float healthPercentage = _currentHealth / maxHealth;

            // Faster transition between Green -> Yellow -> Red
            _targetColor = healthPercentage > 0.5f ? Color.Lerp(Color.yellow, Color.green, (healthPercentage - 0.5f) * 3) : // Faster shift
                Color.Lerp(Color.red, Color.yellow, healthPercentage * 3); // Faster shift
        }
    }
}
