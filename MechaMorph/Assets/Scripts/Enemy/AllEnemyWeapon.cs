using UnityEngine;
using UnityEngine.InputSystem;
using TrippleTrinity.MechaMorph.Weapons;

namespace TrippleTrinity.MechaMorph.Enemy
{
    public class AllEnemyWeapon : BaseGun
    {
        [SerializeField] private float damage;
        [SerializeField] private Transform bulletPrefab;
        [SerializeField] private Transform bulletSpawnPoint;

        public new void Update()
        {
            Shoot();
            if (CurrentAmmo <= 0)
            {
                TryReloading();
            }
        }

        protected override void Shoot()
        {
            Vector3 aimDirection = transform.forward.normalized;
            Transform bulletTransform = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.LookRotation(aimDirection));
            
            BulletBehaviour bulletBehaviour = bulletTransform.GetComponent<BulletBehaviour>();
            if (bulletBehaviour != null)
            {
                BulletCollisionHandler bulletHandler = bulletTransform.GetComponent<BulletCollisionHandler>();
                if (bulletHandler != null)
                {
                    bulletHandler.SetDamage(damage);

                    // Enemy bullets should have "Enemy" as the shooter tag
                    bulletHandler.SetShooter("Enemy");
                }
            }
        }
    }
}