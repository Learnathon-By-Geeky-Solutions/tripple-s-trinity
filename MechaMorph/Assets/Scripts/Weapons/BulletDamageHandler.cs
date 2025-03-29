using UnityEngine;
using TrippleTrinity.MechaMorph.Damage;
using TrippleTrinity.MechaMorph.Health;

namespace TrippleTrinity.MechaMorph.Weapons
{
    public class BulletDamageHandler : MonoBehaviour
    {
        private float _damage;
        private string _shooterTag;

        public void Initialize(float damage, string shooterTag)
        {
            _damage = damage;
            _shooterTag = shooterTag;
        }

        public void HandleCollision(Collider other)
        {
            if (other.CompareTag(_shooterTag)) return; // Ignore shooter itself

            if (_shooterTag == "Enemy" && other.CompareTag("Player"))
            {
                Damageable playerHealth = other.GetComponentInParent<PlayerHealth>();
                playerHealth?.TakeDamage(_damage);
                Debug.Log($"Enemy bullet hit Player! Dealt {_damage} damage.");
            }
            else if (_shooterTag == "Player" && other.CompareTag("Enemy"))
            {
                Damageable enemyHealth = other.GetComponent<Damageable>();
                enemyHealth?.TakeDamage(_damage);
                Debug.Log($"Player bullet hit Enemy! Dealt {_damage} damage.");
            }
        }
    }
}