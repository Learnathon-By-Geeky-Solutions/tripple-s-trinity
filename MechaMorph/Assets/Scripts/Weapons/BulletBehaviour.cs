using UnityEngine;

namespace TrippleTrinity.MechaMorph.Weapons
{
    public class BulletBehaviour : MonoBehaviour
    {
        [SerializeField] private float bulletSpeed = 20f;
        private Rigidbody _rigidbody;
        private BulletDamageHandler _damageHandler;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _damageHandler = GetComponent<BulletDamageHandler>();
        }

        private void Start()
        {
            _rigidbody.velocity = transform.forward * bulletSpeed;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_damageHandler == null) return;
            _damageHandler.HandleCollision(other);
            Destroy(gameObject); // Destroy bullet after hit
        }
    }
}