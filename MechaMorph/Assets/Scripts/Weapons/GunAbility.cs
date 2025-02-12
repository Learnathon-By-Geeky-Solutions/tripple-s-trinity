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
            Vector3 aimDirection = transform.forward.normalized;
            Instantiate(bulletPrefeb, bulletSpawnPoint.position, Quaternion.LookRotation(aimDirection));
            
        }
    }
}

