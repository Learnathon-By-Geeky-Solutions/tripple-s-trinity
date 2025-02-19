using UnityEngine;
using UnityEngine.InputSystem;
using  TrippleTrinity.MechaMorph.Weapons;
namespace TrippleTrinity.MechaMorph.Enemy
{
    public class AllEnemyWeapon : BaseGun
    {
        [SerializeField] private float damage;
        [SerializeField] private Transform bulletPrefeb;
        [SerializeField] private Transform bulletSpawnPoint;
        [SerializeField] private InputActionAsset playerInput;
   

 

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
            Transform bulletTransform = Instantiate(bulletPrefeb, bulletSpawnPoint.position, Quaternion.LookRotation(aimDirection));
            
            BulletBehaviour bulletBehaviour = bulletTransform.GetComponent<BulletBehaviour>();
            if (bulletBehaviour != null)
            {
                bulletBehaviour.SetDamage(damage);
            }
        }
    }
}