using UnityEngine;

namespace TrippleTrinity.MechaMorph.Weapons
{
    public class GunAbility : BaseGun
    {
        public float damage;
        private const float ImpactForce = 50f;

        public override void Update()
        {
            base.Update();

            if (Input.GetButtonDown("Fire1"))
            {
                TryShoot();
            }
            if(Input.GetKeyDown(KeyCode.R))
            {
                TryReloading();
            }

            if (CurrentAmmo == 0)
            {
                TryReloading();
            }
        }

        protected override void Shoot()
        {
            if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out var hit, gunData.shootingRange, gunData.layerMask))
            {
                Debug.Log(gunData.gunName + " hit" + hit.collider.name);
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
        }
    }
}

