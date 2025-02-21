using UnityEngine;
using TrippleTrinity.MechaMorph.Damage;
using TrippleTrinity.MechaMorph.Health;

namespace TrippleTrinity.MechaMorph.Weapons
{
    public class BulletBehaviour : MonoBehaviour
    {
        [SerializeField] private float bulletSpeed = 20f;
        private Rigidbody _rigidbody;
        private float _damage;
        private string _shooterTag;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _rigidbody.velocity = transform.forward * bulletSpeed;
        }
        
        public void SetDamage(float newDamage)
        {
            _damage = newDamage;
        }

        public void SetShooter(string shooterTag)
        {
            _shooterTag = shooterTag;
        }

        private void OnTriggerEnter(Collider other)
        {
            // Ignore collision with the shooter itself
            if (other.CompareTag(_shooterTag)) return;

            // If shooter is Enemy, bullet should damage Player
            if (_shooterTag == "Enemy" && other.CompareTag("Player"))
            {
                // Look for PlayerHealth in the parent
                Damageable playerHealth = other.GetComponentInParent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(_damage);
                    Debug.Log($"Enemy bullet hit Player! Dealt {_damage} damage.");
                }
            }
            // If shooter is Player, bullet should damage Enemy
            else if (_shooterTag == "Player" && other.CompareTag("Enemy"))
            {
                Damageable enemyHealth = other.GetComponent<Damageable>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(_damage);
                    Debug.Log($"Player bullet hit Enemy! Dealt {_damage} damage.");
                }
            }

            Destroy(gameObject); // Destroy bullet after hit
        }
    }
}