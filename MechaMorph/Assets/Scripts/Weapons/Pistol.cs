using TrippleTrinity.MechaMorph.Weapons;
using UnityEngine;

public class Pistol : BaseGun
{
    public float damage;
    private float _impactForce = 50f;
    public override void Update()
    {
        base.Update();

        if (Input.GetButtonDown("Fire1"))
        {
            tryShoot();
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            tryReloading();
        }
    }
    public override void Shoot()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, gunData.shootingRange, gunData.layerMask))
        {
            Debug.Log(gunData.gunName + " hit" + hit.collider.name);
            TakeDamage takedamage = hit.transform.GetComponent<TakeDamage>();
            if (takedamage != null)
            {
                takedamage.Damage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * _impactForce);
            }
        }
    }
}
