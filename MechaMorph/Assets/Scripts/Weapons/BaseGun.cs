using System.Collections;
using UnityEngine;

namespace TrippleTrinity.MechaMorph.Weapons
{
    public abstract class BaseGun : MonoBehaviour
    {
        public GunData gunData;


        [SerializeField] private float currentAmmo;
        [SerializeField] private float nextTimeToFire;

        private bool _isReloading;

        private void Start()
        {
            currentAmmo = gunData.magazineSize;

            transform.root.GetComponent<RobotController>();

        }

        public virtual void Update()
        {

        }

        protected void TryReloading()
        {
            if (!_isReloading && currentAmmo < gunData.magazineSize)
            {
                StartCoroutine(Reload());
            }
        }

        private IEnumerator Reload()
        {
            _isReloading = true;

            Debug.Log(gunData.gunName + " is reloading....");

            yield return new WaitForSeconds(gunData.realoadTime);

            currentAmmo = gunData.magazineSize;
            _isReloading = false;

            Debug.Log(gunData.gunName + " is reloaded.");
        }

        protected void TryShoot()
        {
            if (_isReloading)
            {
                Debug.Log(gunData.gunName + " is realoading...");
                return;
            }
            if(currentAmmo <= 0f)
            {
                Debug.Log(gunData.gunName + " han no bullets left. Please reload.");
                return;
            }
            if(Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + (1/gunData.fireRate);
                HandleShoot();
            }
        }

        private void HandleShoot()
        {
            currentAmmo--;
            Debug.Log(gunData.gunName + " Shoot!, Bullets left: " + currentAmmo);
            Shoot();
        }

        protected abstract void Shoot();


    }
}
