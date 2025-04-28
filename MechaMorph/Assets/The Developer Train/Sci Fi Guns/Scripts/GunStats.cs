using UnityEngine;

namespace TheDeveloperTrain.SciFiGuns
{

    public enum FireMode
    {
        Single = 0,
        Burst = 1
    }


    /// <summary>
    /// A Scriptable Object defining the shooting behaviour of a gun, storing the relavent stats.
    /// Includes data like magazine size, reload duration, burst/single fire mode, fire rate, and more
    /// </summary>
    [CreateAssetMenu(fileName = "GunData", menuName = "SciFiGuns/Gun Stats", order = 1)]
    public class GunStats : ScriptableObject
    {

        [Header("Ammo Settings")]

        /// <summary>
        /// The amount of bullets you carry entirely, this is where ammo is reduced from when reloading
        /// </summary>
        [Tooltip("The amount of bullets you carry entirely, this is where ammo is reduced from when reloading")]
        public int totalAmmo = 100;

        /// <summary>
        /// The max amount of bullets that can be loaded in the gun at any given time.
        /// </summary>
        [Tooltip("The max amount of bullets that can be loaded in the gun at any given time.")]
        public int magazineSize = 10;


        [Header("Timings & Speed")]

        /// <summary>
        /// In seconds, the time it takes for the gun to reload.
        /// </summary>
        [Tooltip("In seconds, the time it takes for the gun to reload.")]
        public float reloadDuration = 6f;

        /// <summary>
        /// In seconds, the time it takes between when the Shoot() function of a given gun is called and when a bullet actually leaves the barrel.
        /// </summary>
        [Tooltip("In seconds, the time it takes between when the Shoot() function of a given gun is called and when a bullet actually leaves the barrel.")]
        public float shootDelay = 0.1f;

        /// <summary>
        /// In meters per second, the speed of the projectile the gun shoots.
        /// </summary>
        [Tooltip("In meters per second, the speed of the projectile the gun shoots.")]
        public float bulletSpeed = 200f;

        /// <summary>
        /// In shots per second (Bursts per second if using burst mode). FireRate includes both the time it takes for a gun to charge as well as to discharge.
        /// </summary>
        [Tooltip("In shots per second (Bursts per second if using burst mode). FireRate includes both the time it takes for a gun to charge as well as to discharge.")]
        public float fireRate = 1f;

        /// <summary>
        /// The type of shooting the gun will use. Single is one shot and then cooldown, Burst is a few shots fired closely together and then cooldown
        /// </summary>
        [Header("Fire Mode Settings")]
        [Tooltip("The type of shooting the gun will use. Single is one shot and then cooldown, Burst is a few shots fired closely together and then cooldown")]
        public FireMode fireMode = FireMode.Single;

        /// <summary>
        /// The number of bullets Fired in quick succession as part of each burst
        /// </summary>
        [Tooltip("The number of bullets Fired in quick succession as part of each burst")]
        public int burstCount = 1;

        /// <summary>
        /// The interval between bullets as part of a single burst
        /// </summary>
        [Tooltip("The interval between bullets as part of a single burst")]
        public float burstInterval = 0f;

        private void OnValidate()
        {
            totalAmmo = Mathf.Max(0, totalAmmo);
            magazineSize = Mathf.Max(1, magazineSize);
            reloadDuration = Mathf.Max(0f, reloadDuration);
            shootDelay = Mathf.Max(0f, shootDelay);
            bulletSpeed = Mathf.Max(0f, bulletSpeed);
            burstCount = Mathf.Max(1, burstCount);
            burstInterval = Mathf.Max(0.0001f, burstInterval);
            fireRate = Mathf.Max(0.0001f, fireRate);

            float fireCycleTime = 1 / fireRate;
            float totalBurstTime = (burstCount - 1) * burstInterval;

            if (shootDelay >= fireCycleTime)
            {
                shootDelay = fireCycleTime - 0.0001f;
            }

            if (fireMode == FireMode.Burst)
            {
                if (shootDelay + totalBurstTime >= fireCycleTime)
                {
                    burstInterval = (fireCycleTime - shootDelay) / (burstCount - 1);
                    burstInterval = Mathf.Max(burstInterval, 0.0001f);
                }

                if (burstCount > magazineSize)
                {
                    burstCount = magazineSize;
                }
            }
        }

    }
}
