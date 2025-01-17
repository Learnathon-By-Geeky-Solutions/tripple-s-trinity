using UnityEngine;

namespace TrippleTrinity.MechaMorph.Combat
{
    public class GunSystem : MonoBehaviour
    {
        [SerializeField] private float damage = 10f;
        [SerializeField] private float range = 20f;
        [SerializeField] private float fireRate = 15f;
        [SerializeField] private float impactForce = 50f;

        private float _nextTimeToFire;

        void Update()
        {
            if (Input.GetButtonDown("Fire1") && Time.time >= _nextTimeToFire)
            {
                _nextTimeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }

        void Shoot()
        {

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hitInfo, range))
            {
                Debug.Log("Hit Enemy!");

                TakeDamage takedamage = hitInfo.transform.GetComponent<TakeDamage>();
                if (takedamage != null)
                {
                    takedamage.Damage(damage);
                }

                if (hitInfo.rigidbody != null)
                {
                    hitInfo.rigidbody.AddForce(-hitInfo.normal * impactForce);
                }

                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hitInfo.distance, Color.red);

            }
            else
            {
                Debug.Log("Nothing...");
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * range, Color.green);
            }
        }
    }
}
