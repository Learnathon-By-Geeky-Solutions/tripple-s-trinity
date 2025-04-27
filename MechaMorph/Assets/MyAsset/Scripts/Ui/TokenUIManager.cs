using TMPro;
using UnityEngine;

namespace TrippleTrinity.MechaMorph.Ui
{
    public class TokenUIManager : MonoBehaviour
    {
        public static TokenUIManager Instance { get; private set; }

        [SerializeField] private TMP_Text tokenCountText; // Assign in Inspector

        private int currentGameTokens ; // Tokens earned in the current session
        private int totalTokens ; // Total saved tokens

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                //LoadTotalTokens(); // Load previous total
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void UpdateTokenCount(int count)
        {
            currentGameTokens = count; // Ensure current session tokens are updated

            if (tokenCountText != null)
            {
                tokenCountText.text = $"Tokens: {currentGameTokens}";
                Debug.Log($"UI Updated: Tokens = {currentGameTokens}");
            }
            else
            {
                Debug.LogError("TokenUIManager: tokenCountText is not assigned!");
            }
        }


        public void ResetTokenCount()
        {
            currentGameTokens = 0; // Reset the variable to ensure session tokens start at 0
            UpdateTokenCount(0);
        }

        public void AddToken()
        {
            UpdateTokenCount(currentGameTokens + 1);
        }

        public void SaveFinalTokenCount()
        {
            totalTokens += currentGameTokens; // Add session tokens to total
            PlayerPrefs.SetInt("TotalTokens", totalTokens);
            PlayerPrefs.Save();
            Debug.Log($"Game Over! Total Tokens Saved: {totalTokens}");

            ResetTokenCount(); // Ensure new game starts from 0
        }
        
        
        public int CurrentTokenCount()
        {
            return currentGameTokens;
        }

        public int GetTotalTokens()
        {
            return totalTokens;
        }
    }
}
