using UnityEngine;
using TrippleTrinity.MechaMorph.InputHandling;  // Ensure you are using InputHandler namespace

namespace TrippleTrinity.MechaMorph.Weapons
{
    public class GunAbility : BaseGun
    {
        [SerializeField] private float damage;
        [SerializeField] private Transform bulletPrefeb;
        [SerializeField] private Transform bulletSpawnPoint;

        public override void Update()
        {
            base.Update();

            // Use InputHandler to check for fire action without direct input dependency
            if (InputHandler.Instance != null && InputHandler.Instance.IsFirePressed())
            {
                TryShoot();
            }

            // You can still use InputHandler for reload if needed
            if (InputHandler.Instance != null && InputHandler.Instance.IsReloadPressed())
            {
                TryReloading();
            }

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