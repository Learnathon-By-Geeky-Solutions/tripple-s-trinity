using UnityEngine;

namespace TheDeveloperTrain.SciFiGuns
{

    public class GunDynamicAnimationSpeedAdjustment : MonoBehaviour
    {
        private Gun gun;
        private GunGlowManager[] gunGlowManagers;
        private GunStats stats;

        void Start()
        {
            gun = GetComponent<Gun>();
            gunGlowManagers = GetComponentsInChildren<GunGlowManager>();
            stats = gun.stats;
            foreach (GunGlowManager glowManager in gunGlowManagers)
            {

                if (stats.fireMode == FireMode.Single)
                {
                    float chargingTime = stats.shootDelay;
                    float dischargingTime = (1 / stats.fireRate) - chargingTime;

                    glowManager.chargingDischargingSpeedRatio = chargingTime / dischargingTime;

                    glowManager.speed = 1 / stats.shootDelay;
                }
                else if (stats.fireMode == FireMode.Burst)
                {
                    float delayBetweenFirstShotInBurstAndLast = (stats.burstCount - 1) * stats.burstInterval;
                    float chargingTime = stats.shootDelay;
                    float dischargingTime = ((1 / stats.fireRate) - chargingTime) - (delayBetweenFirstShotInBurstAndLast);

                    glowManager.chargingDischargingSpeedRatio = (chargingTime + delayBetweenFirstShotInBurstAndLast) / dischargingTime;

                    glowManager.speed = 1 / (stats.shootDelay + delayBetweenFirstShotInBurstAndLast);
                }
            }

            foreach (ParticleSystem particleSystem in gun.gunParticleSystems)
            {
                var main = particleSystem.main;
                if (gun.stats.fireMode == FireMode.Single)
                {
                    main.duration = stats.shootDelay;
                }
                else if (gun.stats.fireMode == FireMode.Burst)
                {
                    main.duration = stats.shootDelay + ((stats.burstCount - 1) * stats.burstInterval);
                }
            }
        }
    }
}