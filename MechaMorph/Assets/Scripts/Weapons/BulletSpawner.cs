using UnityEngine;
using UnityEngine.Pool;

namespace TrippleTrinity.MechaMorph.Weapons
{
    public class BulletSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform bulletSpawnPoint;
        public ObjectPool<BulletBehaviour> bulletPool;
        
        private void Awake()
        {
            bulletPool = new ObjectPool<BulletBehaviour>(
                CreateBullet,
                OnGetFromPool,
                OnReleaseToPool,
                OnDestroyBullet,
                true,
                250,
                1000
            );

        }

        private BulletBehaviour CreateBullet()
        {
            GameObject bulletObject = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            BulletBehaviour bullet = bulletObject.GetComponent<BulletBehaviour>();
            bullet.SetPool(bulletPool); // Assign the pool reference to the bullet
            return bullet;
        }

        private void OnGetFromPool(BulletBehaviour bullet)
        {
            bullet.transform.position = bulletSpawnPoint.position; // Ensure this is the correct spawn point
            bullet.transform.rotation = Quaternion.LookRotation(transform.forward);
            bullet.gameObject.SetActive(true);
        }

        private void OnReleaseToPool(BulletBehaviour bullet)
        {
            bullet.gameObject.SetActive(false);
        }

        private void OnDestroyBullet(BulletBehaviour bullet)
        {
            Destroy(bullet.gameObject);
        }
        
    }
}
