using UnityEngine;
using TrippleTrinity.MechaMorph.InputHandling;

namespace TrippleTrinity.MechaMorph.Weapons
{
    public class GunAbility : BaseGun
    {
        [SerializeField] private float damage;
        [SerializeField] private Transform bulletPrefeb;
        [SerializeField] private Transform bulletSpawnPoint;
        [SerializeField] private bool isAI;  // New variable to differentiate Player & AI

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
            Transform bulletTransform = Instantiate(bulletPrefeb, bulletSpawnPoint.position, Quaternion.LookRotation(aimDirection));
    
            BulletBehaviour bulletBehaviour = bulletTransform.GetComponent<BulletBehaviour>();
    
            if (bulletBehaviour != null)
            {
                bulletBehaviour.SetDamage(damage);
        
                // Assign the shooter based on whether it's an enemy or player
                string shooterTag = gameObject.CompareTag("Enemy") ? "Enemy" : "Player";
                bulletBehaviour.SetShooter(shooterTag);
            }
        }
    }
}
