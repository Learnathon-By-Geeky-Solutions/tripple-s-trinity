using UnityEngine;
using UnityEngine.UI;
using TrippleTrinity.MechaMorph.Damage; // Import Damageable

namespace TrippleTrinity.MechaMorph.Ui
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image healthBarFill;
        [SerializeField] private float fillSpeed = 5f;
        [SerializeField] private float colorChangeSpeed = 10f;

        private Damageable _damageable;
        private float _targetFillAmount;
        private Color _targetColor;

        void Start()
        {
            _damageable = FindObjectOfType<Damageable>(); // Find the health system
            if (_damageable == null)
            {
                Debug.LogError("HealthBar: No Damageable script found in scene!");
                return;
            }

            _targetFillAmount = 1f;
            _targetColor = Color.green;
            UpdateHealthBarColor();
        }

        void Update()
        {
            if (healthBarFill != null)
            {
                // Smoothly update health bar fill amount
                healthBarFill.fillAmount = Mathf.MoveTowards(healthBarFill.fillAmount, _targetFillAmount, Time.deltaTime * fillSpeed);
                
                // Smoothly update health bar color
                healthBarFill.color = Color.Lerp(healthBarFill.color, _targetColor, Time.deltaTime * colorChangeSpeed);
            }
        }

        public void UpdateHealthUI()
        {
            if (_damageable == null) return;

            float currentHealth = _damageable.CurrentHealth;
            float maxHealth = _damageable.MaxHealth;
            _targetFillAmount = currentHealth / maxHealth;

            UpdateHealthBarColor();
        }

        private void UpdateHealthBarColor()
        {
            float healthPercentage = _targetFillAmount;

            // Smooth transition between Green -> Yellow -> Red
            _targetColor = healthPercentage > 0.5f 
                ? Color.Lerp(Color.yellow, Color.green, (healthPercentage - 0.5f) * 2) 
                : Color.Lerp(Color.red, Color.yellow, healthPercentage * 2);
        }
    }
}
