using UnityEngine;
using UnityEngine.Pool;
using TrippleTrinity.MechaMorph.InputHandling;

namespace TrippleTrinity.MechaMorph.Weapons
{
    public class GunAbility : BaseGun
    {
        [SerializeField] private float damage;
        [SerializeField] private BulletBehaviour bulletPrefab;
        [SerializeField] private BulletSpawner bulletSpawner;
        [SerializeField] private Transform bulletSpawnPoint;
        [SerializeField] private bool isAI;  // Differentiates Player & AI

        public BulletBehaviour BulletPrefab => bulletPrefab;
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
                    Shoot();  // Call the protected Shoot() method
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
            if (bulletSpawner == null) return;

            // Select the correct bullet pool based on the shooter
            ObjectPool<BulletBehaviour> selectedPool = gameObject.CompareTag("Enemy")
                ? bulletSpawner.EnemyBulletPool
                : bulletSpawner.PlayerBulletPool;

            if (selectedPool == null) return;

            BulletBehaviour bulletBehaviour = selectedPool.Get();

            if (bulletBehaviour != null)
            {
                bulletBehaviour.transform.position = bulletSpawnPoint.position;
                bulletBehaviour.transform.rotation = bulletSpawnPoint.rotation;
                bulletBehaviour.gameObject.SetActive(true);

                BulletDamageHandler bulletDamageHandler = bulletBehaviour.GetComponent<BulletDamageHandler>();

                if (bulletDamageHandler != null)
                {
                    // Ensure the bullet knows its shooter type
                    bulletDamageHandler.Initialize(damage, gameObject.CompareTag("Enemy") ? "Enemy" : "Player");
                }
            }
        }
    }
}
