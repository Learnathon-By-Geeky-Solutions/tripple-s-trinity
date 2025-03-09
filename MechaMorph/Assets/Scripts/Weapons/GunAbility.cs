using UnityEngine;
using TrippleTrinity.MechaMorph.InputHandling;  

namespace TrippleTrinity.MechaMorph.Weapons
{
    public class GunAbility : BaseGun
    {
        [SerializeField] private float damage;
        [SerializeField] private Transform bulletPrefab;
        [SerializeField] private Transform bulletSpawnPoint;
        [SerializeField] private bool isAI;  // New variable to differentiate Player & AI

        public override void Update()
        {
            base.Update();

            if (!isAI) // Only Player checks for input
            {
                if (InputHandler.Instance != null && InputHandler.Instance.IsFirePressed())
                {
                    TryShoot();
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

        public void AITryShoot()
        {
            if (isAI)  // Only AI can call this method
            {
                TryShoot();
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

                    // Assign the shooter based on whether it's an enemy or player
                    string shooterTag = gameObject.CompareTag("Enemy") ? "Enemy" : "Player";
                    bulletHandler.SetShooter(shooterTag);
                }
            }
        }
    }
}
