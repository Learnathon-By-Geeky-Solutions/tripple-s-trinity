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
                bullet.transform.position = bulletSpawnPoint.position;
                bullet.transform.rotation = Quaternion.LookRotation(transform.forward.normalized);
                bullet.SetActive(true);

                // Apply bullet behavior and set damage
                BulletBehaviour bulletBehaviour = bullet.GetComponent<BulletBehaviour>();
                if (bulletBehaviour != null)
                {
                    bulletBehaviour.SetDamage(damage);
                }
            }
        }
    }
}