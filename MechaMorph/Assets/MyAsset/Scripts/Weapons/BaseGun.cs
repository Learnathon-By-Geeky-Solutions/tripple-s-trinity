using System.Collections;
using TrippleTrinity.MechaMorph.MyAsset.Scripts.Control;
using TrippleTrinity.MechaMorph.MyAsset.Scripts.Ui;
using UnityEngine;

namespace TrippleTrinity.MechaMorph.Weapons
{
    public abstract class BaseGun : MonoBehaviour
    {
        [SerializeField] private GunData gunData;
        protected GunData Gundata => gunData;


        [SerializeField] private float currentAmmo; 
        protected float CurrentAmmo
        {
            get => currentAmmo;
            set => currentAmmo = value;
        }

        [SerializeField] private float nextTimeToFire;

        private GunUIManager gunUIManager;

        private bool _isReloading;

        private void Start()
        {
            currentAmmo = Gundata.MagazineSize;
            gunUIManager = GunUIManager.Instance;

            transform.root.GetComponent<NewRobotController>();

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

            gunUIManager?.UpdateGunStatus(Gundata.GunName + " is reloading....");

            yield return new WaitForSeconds(Gundata.ReloadTime);

            currentAmmo = Gundata.MagazineSize;
            _isReloading = false;

            Debug.Log(Gundata.GunName + " is reloaded.");
        }

        public void TryShoot()
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
