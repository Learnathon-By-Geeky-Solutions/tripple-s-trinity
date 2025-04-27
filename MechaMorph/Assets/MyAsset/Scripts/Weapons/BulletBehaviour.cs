using UnityEngine;

namespace TrippleTrinity.MechaMorph.Weapons
{
    public class BulletBehaviour : MonoBehaviour
    {
        [SerializeField] private float bulletSpeed = 20f;
        private Rigidbody _rigidbody;
        private BulletDamageHandler _damageHandler;
        [SerializeField] private GameObject _bulletParticle;
        [SerializeField] private AudioClip _bulletParticleSound;
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
            GameObject particle = Instantiate(_bulletParticle, transform.position, Quaternion.identity);
            Destroy(particle,particle.GetComponent<ParticleSystem>().main.duration);
            if(_bulletParticleSound!=null) AudioSource.PlayClipAtPoint(_bulletParticleSound, transform.position,0.7f);
            Destroy(gameObject); // Destroy bullet after hit
        }
    }
}