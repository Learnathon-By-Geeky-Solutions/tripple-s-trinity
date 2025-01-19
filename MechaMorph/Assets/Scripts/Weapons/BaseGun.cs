using System.Collections;
using UnityEngine;

namespace TrippleTrinity.MechaMorph.Combat
{
    public abstract class BaseGun : MonoBehaviour
    {
        public GunData gunData;
        public RobotController robotController;
        //public Transform cameraController;

        [SerializeField] private float currentAmmo = 0f;
        [SerializeField] private float nextTimeToFire = 0f;

        private bool isReloading = false;

        private void Start()
        {
            currentAmmo = gunData.magazineSize;

            robotController = transform.root.GetComponent<RobotController>();
            //cameraTransform = robotController.virtualCaremra.transform;

        }

        public virtual void Update()
        {

        }

        public void tryReloading()
        {
            if (!isReloading && currentAmmo < gunData.magazineSize)
            {
                StartCoroutine(Reload());
            }
        }

        private IEnumerator Reload()
        {
            isReloading = true;

            Debug.Log(gunData.gunName + " is reloading....");

            yield return new WaitForSeconds(gunData.realoadTime);

            currentAmmo = gunData.magazineSize;
            isReloading = false;

            Debug.Log(gunData.gunName + " is reloaded.");
        }

        public void tryShoot()
        {
            if (isReloading)
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

        public abstract void Shoot();


    }
}
