using UnityEngine;
using UnityEngine.UI;

namespace TrippleTrinity.MechaMorph.MyAsset.Scripts.Ui
{
    public class GunUIManager : MonoBehaviour
    {
        private static GunUIManager _instance;

        [SerializeField] private Image pistolImage;
        [SerializeField] private Image shotgunImage;
        [SerializeField] private Image machineGunImage;

        public static GunUIManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("GunUIManager instance is null.");
                }
                return _instance;
            }
            private set => _instance = value;
        }

        private void Awake()
        {
            if (_instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void UpdateGun(string gunName)
        {
            pistolImage.enabled = false;
            shotgunImage.enabled = false;
            machineGunImage.enabled = false;

            switch (gunName.ToLower())
            {
                case "pistol":
                    pistolImage.enabled = true;
                    break;
                case "shotgun":
                    shotgunImage.enabled = true;
                    break;
                case "machinegun":
                    machineGunImage.enabled = true;
                    break;
                default:
                    Debug.LogWarning($"Unknown gun name: {gunName}");
                    break;
            }
        }
    }
}