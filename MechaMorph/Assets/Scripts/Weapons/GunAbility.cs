using UnityEngine;
using UnityEngine.InputSystem;

namespace TrippleTrinity.MechaMorph.Weapons
{
    public class GunAbility : BaseGun
    {
        [SerializeField] private float damage;
        [SerializeField] private GameObject bulletPrefeb;
        [SerializeField] private Transform bulletSpawnPoint;
        private const float ImpactForce = 50f;

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
            if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out var hit, Gundata.ShootingRange, Gundata.LayerMask))
            {
                Debug.Log(Gundata.GunName + " hit" + hit.collider.name);
                TakeDamage takedamage = hit.transform.GetComponent<TakeDamage>();
                if (takedamage != null)
                {
                    takedamage.Damage(damage);
                }

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * ImpactForce);
                }
            }
            GameObject bullet = Instantiate(bulletPrefeb, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
    
            BulletBehaviour bulletBehaviour = bullet.GetComponent<BulletBehaviour>();
            if (bulletBehaviour != null)
            {
                bulletBehaviour.SetBulletDirection(bulletSpawnPoint.forward);
            }
        }
    }
}

