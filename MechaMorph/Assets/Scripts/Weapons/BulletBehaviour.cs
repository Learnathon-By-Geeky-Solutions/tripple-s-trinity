using UnityEngine;
using UnityEngine.Pool;
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
        private ObjectPool<BulletBehaviour> _pool;
        private BulletDamageHandler _damageHandler;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _damageHandler = GetComponent<BulletDamageHandler>();
        }

        private void OnEnable()
        {
            SetPhysicsVelocity();
        }

        private void SetPhysicsVelocity()
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

            if (_damageHandler != null)
            {
                _damageHandler.HandleCollision(other);
            }

            _pool?.Release(this); // Return bullet to pool instead of destroying
        }

        public void SetPool(ObjectPool<BulletBehaviour> pool)
        {
            _pool = pool;
        }
    }
}
