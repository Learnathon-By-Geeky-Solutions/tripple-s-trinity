using System.Collections;
using TMPro;
using UnityEngine;

namespace TrippleTrinity.MechaMorph.Ui
{
    public class GunUIManager : MonoBehaviour
    {
        private static GunUIManager _instance;

        [SerializeField] private TextMeshProUGUI gunNameText;
        [SerializeField] private TextMeshProUGUI gunStatusText;

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
                Destroy(gameObject); // Optional safety
            }
        }

        public void UpdateGunName(string message)
        {
            gunNameText.text = message;
            gunNameText.alpha = 1f;

            
        }

        public void UpdateGunStatus(string message)
        {
            gunStatusText.text = message;
        }
    }
}
