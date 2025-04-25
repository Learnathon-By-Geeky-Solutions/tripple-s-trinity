using UnityEngine;
using TMPro;

namespace TrippleTrinity.MechaMorph
{
    public class GunUIManager : MonoBehaviour
    {
        public static GunUIManager Instance;

        [SerializeField] private TextMeshProUGUI gunNameText;
        [SerializeField] private TextMeshProUGUI gunStatusText;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        public void UpdateGunName(string message)
        {
            gunNameText.text = message;
        }

        public void UpdateGunStatus(string message)
        {
            gunStatusText.text = message;
        }
    }
}
