using UnityEngine;

namespace TrippleTrinity.MechaMorph.Token
{
    public class UpgradeManager : MonoBehaviour
    {
        public static UpgradeManager Instance;
        private int _upgradePoints;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void AddUpgradePoint()
        {
            _upgradePoints++;
            Debug.Log($"Upgrade Points: {_upgradePoints}");
            // Implement upgrade system here
        }
    }
}