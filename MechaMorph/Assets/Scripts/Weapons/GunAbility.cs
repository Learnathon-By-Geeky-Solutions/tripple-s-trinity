using UnityEngine;
using UnityEngine.InputSystem;

namespace TrippleTrinity.MechaMorph.Weapons
{
    public class GunAbility : BaseGun
    {
        [SerializeField] private float damage;
        [SerializeField] private Transform bulletPrefeb;
        [SerializeField] private Transform bulletSpawnPoint;
        [SerializeField] private InputActionAsset playerInput;
        private InputAction _fireAction;
        private InputAction _reloadAction;

        private void Awake()
        {
            _fireAction = playerInput.FindAction("Fire");
            _reloadAction = playerInput.FindAction("Reloading");

            _fireAction.Enable();
            _reloadAction.Enable();
        }

        public override void Update()
        {
            base.Update();

            if (_fireAction.triggered)
            {
                TryShoot();
            }
            if(_reloadAction.triggered)
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
            GameObject bullet = ObjectPool.instance.GetPooledObjects();

            if (bullet != null)
            {
                // Reset position and rotation
                bullet.transform.position = bulletSpawnPoint.position;
                bullet.transform.rotation = Quaternion.LookRotation(bulletSpawnPoint.forward);

                // Reset velocity if using Rigidbody
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.velocity = Vector3.zero;  // Stop previous movement
                    rb.angularVelocity = Vector3.zero; // Reset rotation movement
                }

                // Reset other properties (if needed)
                BulletBehaviour bulletBehaviour = bullet.GetComponent<BulletBehaviour>();
                if (bulletBehaviour != null)
                {
                    bulletBehaviour.ResetBullet(); // If you have a reset function inside BulletBehaviour
                    bulletBehaviour.SetDamage(damage);
                }

                // Activate bullet
                bullet.SetActive(true);
            }


        }
    }
}

