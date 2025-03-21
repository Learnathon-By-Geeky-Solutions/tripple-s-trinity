using TrippleTrinity.MechaMorph.Weapons;
using UnityEngine;
using UnityEngine.Pool;

namespace TrippleTrinity.MechaMorph
{
    public class BulletSpawner : MonoBehaviour
    {
        public ObjectPool<BulletBehaviour> playerBulletPool;
        public ObjectPool<BulletBehaviour> enemyBulletPool;
        private GunAbility gunAbility;
        void Start()
        {
            gunAbility = GetComponent<GunAbility>();
            playerBulletPool = new ObjectPool<BulletBehaviour>(CreateBullet, OnTakeBulletFromPool, OnrturnBulletToPool, OnDestroyBullet, true, 500, 1000);
            enemyBulletPool = new ObjectPool<BulletBehaviour>(CreateBullet, OnTakeBulletFromPool, OnrturnBulletToPool, OnDestroyBullet, true, 500, 1000);
            
            // 🔥 Preload some bullets to prevent delay on first use
            PreloadBullets(playerBulletPool, 10);
            PreloadBullets(enemyBulletPool, 10);
        }

        // Function to preload bullets
        private void PreloadBullets(ObjectPool<BulletBehaviour> pool, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                BulletBehaviour bullet = pool.Get();
                pool.Release(bullet); // Immediately return to pool so it's ready for use
            }
        }


        private BulletBehaviour CreateBullet()
        {
            //Spwan new bullet.
            //Vector3 aimDirection = transform.forward.normalized;
            BulletBehaviour bullet = Instantiate(gunAbility.BulletPrefab, gunAbility.BulletSpawnPoint.position, gunAbility.BulletSpawnPoint.rotation);

            //Assign the bullet pool
            bullet.SetPool(playerBulletPool);
            bullet.SetPool(enemyBulletPool);

            return bullet;
        }


        private void OnTakeBulletFromPool(BulletBehaviour bullet)
        {
            //set transform and rotation.
            bullet.transform.position = gunAbility.BulletSpawnPoint.position;
            bullet.transform.right = gunAbility.BulletSpawnPoint.transform.right; //here can use rotation instade of right.

            //Activate
            bullet.gameObject.SetActive(true);
        }

        private void OnrturnBulletToPool(BulletBehaviour bullet)
        {
            bullet.gameObject.SetActive(false);
        }

        private void OnDestroyBullet(BulletBehaviour bullet)
        {
            Destroy(bullet.gameObject);
        }
       
    }
}
