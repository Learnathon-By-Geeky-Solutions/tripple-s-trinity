using TrippleTrinity.MechaMorph.InputHandling;
using TrippleTrinity.MechaMorph.Weapons;
using UnityEngine;

namespace TrippleTrinity.MechaMorph.MyAsset.Scripts.Weapons
{
    public class GunAbility : BaseGun
    {
        [SerializeField] private float damage;
        [SerializeField] private Transform bulletPrefab;
        [SerializeField] private Transform bulletSpawnPoint;
        [SerializeField] private bool isAI;  // Differentiates Player & AI

        [Header("Fire Settings")]
        [SerializeField] private float fireDelay = 0.2f; // Time between shots
        private float lastFireTime;

        [Header("Effects")]
        [SerializeField] private AudioSource fireAudioSource;    // Fire sound
        [SerializeField] private AudioClip fireSoundClip;

        [Header("Muzzle Flash")]
        [SerializeField] private GameObject muzzleFlashPrefab;    // Muzzle flash prefab from BigRookGames
        [SerializeField] private Transform muzzleFlashSpawnPoint; // Where to spawn muzzle flash
        [SerializeField] private float muzzleFlashDuration = 0.5f; // Time before destroying flash

        public override void Update()
        {
            base.Update();

            if (!isAI)
            {
                if (InputHandler.Instance != null && InputHandler.Instance.IsFirePressed())
                {
                    TriggerShoot();
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
            if (Time.time >= lastFireTime + fireDelay)
            {
                Shoot();
                lastFireTime = Time.time;
            }
        }

        protected override void Shoot()
        {
            if (CurrentAmmo <= 0)
                return;

            // Instantiate bullet
            Vector3 aimDirection = transform.forward.normalized;
            Transform bulletTransform = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.LookRotation(aimDirection));

            BulletDamageHandler bulletDamageHandler = bulletTransform.GetComponent<BulletDamageHandler>();
            if (bulletDamageHandler != null)
            {
                bulletDamageHandler.Initialize(damage, gameObject.CompareTag("Enemy") ? "Enemy" : "Player");
            }

            PlayFireSound();
            PlayMuzzleFlash();

            CurrentAmmo--;
        }

        private void PlayFireSound()
        {
            if (fireAudioSource != null && fireSoundClip != null)
            {
                fireAudioSource.PlayOneShot(fireSoundClip);
            }
        }

        private void PlayMuzzleFlash()
        {
            if (muzzleFlashPrefab != null && muzzleFlashSpawnPoint != null)
            {
                Quaternion spawnRotation = muzzleFlashSpawnPoint.rotation * Quaternion.Euler(0f, -90f, 0f); // Add -90° on Y-axis
                GameObject flashInstance = Instantiate(muzzleFlashPrefab, muzzleFlashSpawnPoint.position, spawnRotation, muzzleFlashSpawnPoint);
                Destroy(flashInstance, muzzleFlashDuration);
            }
        }
    }
}
