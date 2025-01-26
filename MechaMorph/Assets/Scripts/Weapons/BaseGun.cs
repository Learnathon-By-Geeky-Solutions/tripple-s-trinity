using System.Collections;
using TrippleTrinity.MechaMorph.Control;
using UnityEngine;

namespace TrippleTrinity.MechaMorph.Weapons
{
    public abstract class BaseGun : MonoBehaviour
    {
        [SerializeField] private GunData gunData;
        protected GunData Gundata => gunData;


        [SerializeField] private float currentAmmo; 
        protected float CurrentAmmo => currentAmmo;
        [SerializeField] private float nextTimeToFire;

        private bool _isReloading;

        private void Start()
        {
            currentAmmo = Gundata.MagazineSize;

            transform.root.GetComponent<RobotController>();

        }

        public virtual void Update()
        {

        }

        protected void TryReloading()
        {
            if (!_isReloading && currentAmmo < Gundata.MagazineSize)
            {
                StartCoroutine(Reload());
            }
        }

        private IEnumerator Reload()
        {
            _isReloading = true;

            Debug.Log(Gundata.GunName + " is reloading....");

            yield return new WaitForSeconds(Gundata.ReloadTime);

            currentAmmo = Gundata.MagazineSize;
            _isReloading = false;

            Debug.Log(Gundata.GunName + " is reloaded.");
        }

        protected void TryShoot()
        {
            if (_isReloading)
            {
                Debug.Log(Gundata.GunName + " is realoading...");
                return;
            }
            if(currentAmmo <= 0f)
            {
                Debug.Log(Gundata.GunName + " han no bullets left. Please reload.");
                return;
            }
            if(Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + (1/Gundata.FireRate);
                HandleShoot();
            }
        }

        private void HandleShoot()
        {
            currentAmmo--;
            Debug.Log(Gundata.GunName + " Shoot!, Bullets left: " + currentAmmo);
            Shoot();
        }

        protected abstract void Shoot();


    }
}
