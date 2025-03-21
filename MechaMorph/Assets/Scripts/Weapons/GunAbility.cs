using UnityEngine;
using UnityEngine.Pool;
using TrippleTrinity.MechaMorph.InputHandling;  

namespace TrippleTrinity.MechaMorph.Weapons
{
    public class GunAbility : BaseGun
    {
        [SerializeField] private float damage;
        [SerializeField] private BulletBehaviour bulletPrefeb;
        [SerializeField] private BulletSpawner bulletSpawner;
        [SerializeField] private Transform bulletSpawnPoint;
        [SerializeField] private bool isAI;  // New variable to differentiate Player & AI

        public BulletBehaviour BulletPrefab => bulletPrefeb;
        public Transform BulletSpawnPoint => bulletSpawnPoint;

        private void Start()
        {
            bulletSpawner = FindObjectOfType<BulletSpawner>(); 
        }
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
            if (bulletSpawner == null) return;

            // Select the correct bullet pool based on the shooter
            ObjectPool<BulletBehaviour> selectedPool = gameObject.CompareTag("Enemy")
                ? bulletSpawner.enemyBulletPool
                : bulletSpawner.playerBulletPool;

            if (selectedPool == null) return;

            BulletBehaviour bulletBehaviour = selectedPool.Get();

            if (bulletBehaviour != null)
            {
                bulletBehaviour.transform.position = bulletSpawnPoint.position;
                bulletBehaviour.transform.rotation = bulletSpawnPoint.rotation;
                bulletBehaviour.gameObject.SetActive(true);

                bulletBehaviour.SetDamage(damage);

                // Ensure the bullet knows its shooter type
                string shooterTag = gameObject.CompareTag("Enemy") ? "Enemy" : "Player";
                bulletBehaviour.SetShooter(shooterTag);
            }
        }

    }
}