using UnityEngine;
using TrippleTrinity.MechaMorph.Damage;
using TrippleTrinity.MechaMorph.Health;

namespace TrippleTrinity.MechaMorph.Weapons
{
    public class BulletCollisionHandler : MonoBehaviour
    {
        private float _damage;
        private string _shooterTag;

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
            if (other.CompareTag(_shooterTag)) return;

            if (_shooterTag == "Enemy" && other.CompareTag("Player"))
            {
                Damageable playerHealth = other.GetComponentInParent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(_damage);
                    Debug.Log($"Enemy bullet hit Player! Dealt {_damage} damage.");
                }
            }
            else if (_shooterTag == "Player" && other.CompareTag("Enemy"))
            {
                Damageable enemyHealth = other.GetComponent<Damageable>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(_damage);
                    Debug.Log($"Player bullet hit Enemy! Dealt {_damage} damage.");
                }
            }

            Destroy(gameObject);
        }
    }
}