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
            int totalTokens = PlayerPrefs.GetInt(TotalTokensKey, 0);
            totalTokens++;
            PlayerPrefs.SetInt(TotalTokensKey, totalTokens);

            // Update the UI for the current session
            TokenUIManager.Instance?.UpdateTokenCount(_upgradeTokenCount);
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