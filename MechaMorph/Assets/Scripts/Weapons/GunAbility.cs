using UnityEngine;
using TrippleTrinity.MechaMorph.InputHandling;  

namespace TrippleTrinity.MechaMorph.Weapons
{
    public class GunAbility : BaseGun
    {
        [SerializeField] private float damage;
        [SerializeField] private GameObject bulletPrefeb;
        [SerializeField] private Transform bulletSpawnPoint;
        [SerializeField] private bool isAI;  // New variable to differentiate Player & AI
        private BulletSpawner _bulletSpawner;

        private void Start()
        {
            _bulletSpawner = FindObjectOfType<BulletSpawner>();
            
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
            if (_bulletSpawner == null || _bulletSpawner.bulletPool == null)
            {
                Debug.LogError($"BulletSpawner or bulletPool is not initialized for {gameObject.name}!");
                return;
            }

            // Get a bullet from the pool
            BulletBehaviour bulletBehaviour = _bulletSpawner.bulletPool.Get();

            if (bulletBehaviour != null)
            {
                bulletBehaviour.transform.position = bulletSpawnPoint.position;
                bulletBehaviour.transform.rotation = Quaternion.LookRotation(transform.forward);
                bulletBehaviour.SetDamage(damage);
                bulletBehaviour.SetShooter(gameObject.tag);

                Debug.Log($"{gameObject.name} successfully shot a bullet.");
            }
            else
            {
                Debug.LogError($"BulletBehaviour is null when shooting from {gameObject.name}!");
            }
        }

    }
}