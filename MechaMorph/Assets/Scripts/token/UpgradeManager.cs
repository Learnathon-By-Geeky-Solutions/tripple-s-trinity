using TrippleTrinity.MechaMorph.Ui;
using UnityEngine;

namespace TrippleTrinity.MechaMorph.Token
{
    public class UpgradeManager : MonoBehaviour
    {
        private static UpgradeManager _instance;
        private int _upgradePoints;
        private int _upgradeTokenCount;
        
        public static UpgradeManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("UpgradeManager instance is null.");
                }
                return _instance;
            }
        }
        
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
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

        public void AddUpgradeToken()
        {
            _upgradeTokenCount++;
            
            TokenUIManager.Instance?.UpdateTokenCount(_upgradeTokenCount);
        }

    }
}