using UnityEngine;
using UnityEngine.InputSystem;

namespace TrippleTrinity.MechaMorph.Weapons
{
    public class GunAbility : BaseGun
    {
        [SerializeField] private float damage;
        public GameObject bulletPrefeb;
        public Transform bulletSpawnPoint;
        private const float ImpactForce = 50f;

        [SerializeField] private InputActionAsset playerInput;
        private InputAction fireAction;
        private InputAction reloadAction;

        private void Awake()
        {
            fireAction = playerInput.FindAction("Fire");
            reloadAction = playerInput.FindAction("Reloading");

            fireAction.Enable();
            reloadAction.Enable();
        }

        public override void Update()
        {
            base.Update();

            if (fireAction.triggered)
            {
                TryShoot();
            }
            if(reloadAction.triggered)
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
            Instantiate(bulletPrefeb, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        }
    }
}

