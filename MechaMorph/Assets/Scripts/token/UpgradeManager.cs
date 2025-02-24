using TrippleTrinity.MechaMorph.Ui;
using UnityEngine;

namespace TrippleTrinity.MechaMorph.Token
{
    public class UpgradeManager : MonoBehaviour
    {
        private static UpgradeManager _instance;
        private int _upgradePoints;
        private int _upgradeTokenCount;
        private const string TotalTokensKey = "TotalTokens"; // Key for PlayerPrefs

        //upgrade levels and costs
        private int _boosterUpgradeLevel;
        private int _areaDamageUpgradeLevel;
        private int _boosterUpgradeCost = 5;
        private int _areaDamageUpgradeCost = 5;
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
            // Ensure this instance persists across scenes
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
                _upgradeTokenCount = 0; // Reset the session token count
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
        }

        public void AddUpgradeToken()
        {
            _upgradeTokenCount++;
            // Add to total tokens across all sessions
            int totalTokens = PlayerPrefs.GetInt(TotalTokensKey, 0) + 1;
            totalTokens++;
            PlayerPrefs.SetInt(TotalTokensKey, totalTokens);
            PlayerPrefs.Save();

            // Update the UI for the current session
            TokenUIManager.Instance?.UpdateTokenCount(_upgradeTokenCount);
        }

        public bool UpgradeBoosterCooldown()
        {
            if (_upgradeTokenCount >= _boosterUpgradeCost)
            {
                _upgradeTokenCount -= _boosterUpgradeCost;
                _boosterUpgradeLevel++;
                _boosterUpgradeCost += 5;
                
                PlayerPrefs.SetInt(TotalTokensKey, _upgradeTokenCount);
                PlayerPrefs.Save();
                
                TokenUIManager.Instance?.UpdateTokenCount(_boosterUpgradeCost);
                return true;
            }
            return false;
        }

        public bool UpgradeAreaDamage()
        {
            if (_upgradeTokenCount >= _areaDamageUpgradeCost)
            {
                _upgradeTokenCount -= _areaDamageUpgradeCost;
                _areaDamageUpgradeLevel++;
                _areaDamageUpgradeCost += 5;
                
                PlayerPrefs.SetInt(TotalTokensKey, _upgradeTokenCount);
                PlayerPrefs.Save();
                TokenUIManager.Instance?.UpdateTokenCount(_areaDamageUpgradeCost);
                return true;
            }
            return false;
        }

        public int GetUpgradeTokenCount()
        {
            return _upgradeTokenCount;
        }

        public int GetTotalUpgradeTokenCount()
        {
            return PlayerPrefs.GetInt(TotalTokensKey, 0); // Get the total from PlayerPrefs
        }
    }
}