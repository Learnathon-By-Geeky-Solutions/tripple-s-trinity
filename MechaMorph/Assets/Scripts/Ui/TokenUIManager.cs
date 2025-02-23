using UnityEngine;
using TMPro;

namespace TrippleTrinity.MechaMorph.Ui
{
    public class TokenUIManager : MonoBehaviour
    {
        public static TokenUIManager Instance { get; private set; } // Singleton for easy access
        public TextMeshProUGUI tokenCounterText; // UI Element

        private int _tokenCount; 
        
        private void Awake()
        {
            // Ensure only one instance exists
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.LogWarning("Multiple TokenUIManager instances detected! Destroying duplicate.");
                Destroy(gameObject);
            }
        }

        public void UpdateTokenCount(int upgradeTokenCount)
        {
            _tokenCount++; // Increase counter
            if (tokenCounterText != null)
            {
                tokenCounterText.text = $"Tokens: {_tokenCount}"; // Update UI
            }
            else
            {
                Debug.LogError("TokenUIManager: TokenCounterText is not assigned in the Inspector!");
            }
        }
    }
}