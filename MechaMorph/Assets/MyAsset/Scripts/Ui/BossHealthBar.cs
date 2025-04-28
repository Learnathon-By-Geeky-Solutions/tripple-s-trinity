using TrippleTrinity.MechaMorph.Damage;
using UnityEngine;
using UnityEngine.UI;

namespace TrippleTrinity.MechaMorph.MyAsset.Scripts.Ui
{
    public class BossHealthBar : MonoBehaviour
    {
        public Transform target; // The boss to follow
        [SerializeField] private Vector3 offset = new Vector3(0, 3f, 0); // Position offset above the boss

        [Header("Health Fill Settings")]
        [SerializeField] private Image healthFillImage;
        [SerializeField] private float updateSpeed = 5f;
        [SerializeField] private float colorChangeSpeed = 5f;

        private Damageable _damageable;
        private float _targetFillAmount;
        private Color _targetColor;

        private void Start()
        {
            if (target == null)
            {
                Debug.LogError("BossHealthBar: No target assigned!");
                return;
            }

            _damageable = target.GetComponent<Damageable>();
            if (_damageable == null)
            {
                Debug.LogError("BossHealthBar: No Damageable component found on target!");
                return;
            }

            // Subscribe to boss health events
            _damageable.OnDamageTaken += UpdateHealthUI;
            _damageable.OnHealed += UpdateHealthUI;
            _damageable.OnDeath += HandleDeath;

            _targetFillAmount = 1f;
            _targetColor = Color.green;
            UpdateHealthBarColor();
        }

        private void Update()
        {
            if (target == null) return;

            // Follow the boss
            transform.position = target.position + offset;

            // Face the camera
            var cam = Camera.main;
            if (cam != null)
            {
                transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward,
                                 cam.transform.rotation * Vector3.up);
            }

            // Smooth health bar fill
            if (healthFillImage != null)
            {
                healthFillImage.fillAmount = Mathf.MoveTowards(healthFillImage.fillAmount, _targetFillAmount, Time.deltaTime * updateSpeed);
                healthFillImage.color = Color.Lerp(healthFillImage.color, _targetColor, Time.deltaTime * colorChangeSpeed);
            }
        }

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
            // Boss dead -> destroy the health bar
            Destroy(gameObject);
        }

        private void UpdateHealthBarColor()
        {
            float healthPercentage = _targetFillAmount;

            // Green -> Yellow -> Red transition
            _targetColor = healthPercentage > 0.5f
                ? Color.Lerp(Color.yellow, Color.green, (healthPercentage - 0.5f) * 2f)
                : Color.Lerp(Color.red, Color.yellow, healthPercentage * 2f);
        }

        private void OnDestroy()
        {
            if (_damageable != null)
            {
                _damageable.OnDamageTaken -= UpdateHealthUI;
                _damageable.OnHealed -= UpdateHealthUI;
                _damageable.OnDeath -= HandleDeath;
            }
        }
    }
}
