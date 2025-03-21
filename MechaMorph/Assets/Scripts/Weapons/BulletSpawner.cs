using UnityEngine;
using UnityEngine.Pool;

namespace TrippleTrinity.MechaMorph.Weapons
{
    public class BulletSpawner : MonoBehaviour
    {
        public ObjectPool<BulletBehaviour> PlayerBulletPool;
        public ObjectPool<BulletBehaviour> EnemyBulletPool;
        private GunAbility _gunAbility;
        void Start()
        {
            _gunAbility = GetComponent<GunAbility>();
            PlayerBulletPool = new ObjectPool<BulletBehaviour>(CreateBullet, OnTakeBulletFromPool, OnReturnBulletToPool, OnDestroyBullet, true, 500, 1000);
            EnemyBulletPool = new ObjectPool<BulletBehaviour>(CreateBullet, OnTakeBulletFromPool, OnReturnBulletToPool, OnDestroyBullet, true, 500, 1000);
            
            //Preload some bullets to prevent delay on first use
            PreloadBullets(PlayerBulletPool, 10);
            PreloadBullets(EnemyBulletPool, 10);
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
            //Spawn new bullet.
            
            BulletBehaviour bullet = Instantiate(_gunAbility.BulletPrefab, _gunAbility.BulletSpawnPoint.position, _gunAbility.BulletSpawnPoint.rotation);

            //Assign the bullet pool
            bullet.SetPool(PlayerBulletPool);
            bullet.SetPool(EnemyBulletPool);

            return bullet;
        }


        private void OnTakeBulletFromPool(BulletBehaviour bullet)
        {
            //set transform and rotation.
            bullet.transform.position = _gunAbility.BulletSpawnPoint.position;
            bullet.transform.right = _gunAbility.BulletSpawnPoint.transform.right; //here can use rotation instead of right.

            //Activate
            bullet.gameObject.SetActive(true);
        }

        private void OnReturnBulletToPool(BulletBehaviour bullet)
        {
            bullet.gameObject.SetActive(false);
        }

        private void OnDestroyBullet(BulletBehaviour bullet)
        {
            Destroy(bullet.gameObject);
        }
       
    }
}
