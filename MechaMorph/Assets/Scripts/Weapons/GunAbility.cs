using UnityEngine;
using TrippleTrinity.MechaMorph.InputHandling;

namespace TrippleTrinity.MechaMorph.Weapons
{
    public class GunAbility : BaseGun
    {
        [SerializeField] private float damage;
        [SerializeField] private Transform bulletPrefab; // Fixed typo in variable name
        [SerializeField] private Transform bulletSpawnPoint;
        [SerializeField] private bool isAI;  // Differentiates Player & AI

        public override void Update()
        {
            base.Update();

            if (!isAI) // Only Player checks for input
            {
                if (InputHandler.Instance != null && InputHandler.Instance.IsFirePressed())
                {
                    TriggerShoot();  // Call the public TriggerShoot() method
                }

                if (InputHandler.Instance != null && InputHandler.Instance.IsReloadPressed())
                {
                    TryReloading();
                }
            }

            if (CurrentAmmo <= 0)
            {
                TryReloading();
            }
        }

        public void TriggerShoot()
        {
            Shoot(); // Calls the protected Shoot() method
        }

        // Protected Shoot() method for actual shooting logic
        protected override void Shoot()
        {
            Vector3 aimDirection = transform.forward.normalized;
            Transform bulletTransform = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.LookRotation(aimDirection));
    
            BulletDamageHandler bulletDamageHandler = bulletTransform.GetComponent<BulletDamageHandler>();

            if (bulletDamageHandler != null)
            {
                bulletDamageHandler.Initialize(damage, gameObject.CompareTag("Enemy") ? "Enemy" : "Player");
            }
        }
    }
}