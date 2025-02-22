using UnityEngine;
using TMPro;

namespace TrippleTrinity.MechaMorph.Ui
{
    public class TokenUIManager : MonoBehaviour
    {
        private static TokenUIManager _instance; // Singleton for easy access
        public TextMeshProUGUI tokenCounterText; // UI Element

        private int _tokenCount; // Token counter

        private void Awake()
        {
            // Singleton pattern to ensure only one instance exists
            if (_instance == null)
                _instance = this;
            else
                Destroy(gameObject);
        }

        public void UpdateTokenCount()
        {
            _tokenCount++; // Increase counter
            if (tokenCounterText != null)
            {
                tokenCounterText.text = $"Tokens: {_tokenCount}"; // Update UI
            }
        }
    }
}
