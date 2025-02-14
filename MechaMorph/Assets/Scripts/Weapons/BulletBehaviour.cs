using TrippleTrinity.MechaMorph.Damage;
using UnityEngine;

namespace TrippleTrinity.MechaMorph.Weapons
{
    public class BulletBehaviour : MonoBehaviour
    {
        [SerializeField] private float bulletSpeed = 20f;
        private Rigidbody _rigidbody;
        private Vector3 _bulletDirection;
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
            _damage = newDamage; // Assign damage value from GunAbility
        }

        private void OnTriggerEnter(Collider other)
        {
            TakeDamage takedamage = other.GetComponent<TakeDamage>();
            if (takedamage != null && (other.CompareTag("Player") || other.CompareTag("Enemy")))
            {
                takedamage.Damage(_damage);
            }
            Destroy(gameObject);
        }
    }
}
