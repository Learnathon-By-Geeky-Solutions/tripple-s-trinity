using UnityEngine;


namespace TrippleTrinity.MechaMorph.Weapons
{
    [CreateAssetMenu (fileName = "NewGunData", menuName = "Gun/GunData")]
    public class GunData : ScriptableObject
    {
        public string gunName;
        public LayerMask layerMask;

        [Header("Fire Config")]
        public float shootingRange;
        public float fireRate;

        [Header("Reload Config")]
        public float magazineSize;
        public float realoadTime;
    }
}

