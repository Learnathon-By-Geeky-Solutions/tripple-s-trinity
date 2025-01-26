using UnityEngine;


namespace TrippleTrinity.MechaMorph.Weapons
{
    [CreateAssetMenu (fileName = "NewGunData", menuName = "Gun/GunData")]
    public class GunData : ScriptableObject
    {
        [SerializeField] private string gunName;
        [SerializeField] private LayerMask layerMask;

        [Header("Fire Config")]
        [SerializeField] private float shootingRange;
        [SerializeField] private float fireRate;

        [Header("Reload Config")]
        [SerializeField] private float magazineSize;
        [SerializeField] private float reloadTime;

        public string GunName => gunName;
        public LayerMask LayerMask => layerMask;
        public float ShootingRange => shootingRange;
        public float FireRate => fireRate;
        public float MagazineSize => magazineSize;
        public float ReloadTime => reloadTime;
    }
}

