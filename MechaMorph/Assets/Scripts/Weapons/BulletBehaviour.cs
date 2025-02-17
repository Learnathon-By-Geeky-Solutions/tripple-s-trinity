using TrippleTrinity.MechaMorph.Damage;
using UnityEngine;

namespace TrippleTrinity.MechaMorph.Weapons
{
    public class BulletBehaviour : MonoBehaviour
    {
        [SerializeField] private float bulletSpeed = 20f;
        private Rigidbody _rigidbody;
        private float _damage;

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

        private void OnTriggerEnter(Collider other)
        {
            Damageable damageable = other.GetComponent<Damageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(_damage); // Apply damage using Damageable

                // Optional: Log or play effects when damage is taken
                damageable.OnDamageTaken += () => Debug.Log($"{other.name} took damage!");

                // Handle death event
                damageable.OnDeath += () =>
                {
                    Debug.Log($"{other.name} has died.");
                    Destroy(other.gameObject);
                };
            }

            Destroy(gameObject); // Destroy bullet after hit
        }
    }
}