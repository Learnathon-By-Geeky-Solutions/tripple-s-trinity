using TrippleTrinity.MechaMorph.Damage;
using UnityEngine;
using UnityEngine.UI;

namespace TrippleTrinity.MechaMorph.MyAsset.Scripts.Ui
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

            // Subscribe to events
            _damageable.OnDamageTaken += UpdateHealthUI;
            _damageable.OnHealed += UpdateHealthUI;
            _damageable.OnDeath += HandleDeath;

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

        // Update the health bar based on the current health
        private void UpdateHealthUI()
        {
            if (_damageable == null) return;

            float currentHealth = _damageable.CurrentHealth;
            float maxHealth = _damageable.MaxHealth;
            _targetFillAmount = currentHealth / maxHealth;

            UpdateHealthBarColor();
        }

        private void HandleDeath()
        {
            Debug.Log("Player is dead. Reset health bar.");
            _targetFillAmount = 0;
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

        private void OnDestroy()
        {
            // Unsubscribe to avoid memory leaks
            if (_damageable != null)
            {
                _damageable.OnDamageTaken -= UpdateHealthUI;
                _damageable.OnHealed -= UpdateHealthUI;
                _damageable.OnDeath -= HandleDeath;
            }
        }
    }
}
