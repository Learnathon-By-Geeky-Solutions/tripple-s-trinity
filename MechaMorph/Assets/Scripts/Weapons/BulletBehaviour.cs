using UnityEngine;

namespace TrippleTrinity.MechaMorph.Weapons
{
    public class BulletBehaviour : MonoBehaviour
    {
        [SerializeField] private float bulletSpeed = 20f;
        private Vector3 _bulletDirection;
        private GunData _gunData;
        private readonly float _damage = 1f;

        public void SetBulletDirection(Vector3 direction)
        {
            _bulletDirection = direction.normalized;
        }
        void Update()
        {
            transform.Translate(_bulletDirection * (bulletSpeed * Time.deltaTime));
        }

        private void OnEnable()
        {
            Destroy(gameObject, 2f);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player"))
            {
                TakeDamage takeDamage = collision.gameObject.GetComponent<TakeDamage>();
                if (takeDamage != null)
                {
                    takeDamage.Damage(_damage);
                }
                
                Destroy(gameObject);
            }
        }
    }
}
