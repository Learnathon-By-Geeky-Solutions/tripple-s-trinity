using TrippleTrinity.MechaMorph.Weapons;
using UnityEngine;
using UnityEngine.Pool;

namespace TrippleTrinity.MechaMorph
{
    public class BulletSpawner : MonoBehaviour
    {
        public ObjectPool<BulletBehaviour> pool;
        private GunAbility gunAbility;
        void Start()
        {
            gunAbility = GetComponent<GunAbility>();
            pool = new ObjectPool<BulletBehaviour>(CreateBullet, OnTakeBulletFromPool, OnrturnBulletToPool, OnDestroyBullet, true, 500, 1000);
        }



        
        private BulletBehaviour CreateBullet()
        {
            //Spwan new bullet.
            //Vector3 aimDirection = transform.forward.normalized;
            BulletBehaviour bullet = Instantiate(gunAbility.BulletPrefab, gunAbility.BulletSpawnPoint.position, gunAbility.BulletSpawnPoint.rotation);

            //Assign the bullet pool
            bullet.SetPool(pool);

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
