using UnityEngine;

public class GunSystem : MonoBehaviour
{
    public float damage = 10f;
    public float range = 20f;
    public float fireRate = 15f;
    public float impactForce = 50f;

    private float nextTimeToFire = 0;

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1 / fireRate;
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
