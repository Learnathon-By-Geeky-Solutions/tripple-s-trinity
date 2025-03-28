using TMPro;
using UnityEngine;

namespace TrippleTrinity.MechaMorph.Ui
{
    public class TokenUIManager : MonoBehaviour
    {
        public static TokenUIManager Instance { get; private set; }

        [SerializeField] private TMP_Text tokenCountText; // Assign in Inspector

        private int currentGameTokens = 0; // Tokens earned in the current session
        private int totalTokens = 0; // Total saved tokens

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

        private void Start()
        {
            //LoadTotalTokens(); // Load total tokens when the UI starts
            //UpdateTokenCount(currentGameTokens); // Show session tokens
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

        private void LoadTotalTokens()
        {
            totalTokens = PlayerPrefs.GetInt("TotalTokens", 0);
            Debug.Log($"Loaded Total Tokens: {totalTokens}");

            // Make sure UI updates correctly in the menu
            if (tokenCountText != null)
            {
                tokenCountText.text = $"Tokens: {totalTokens}";
            }
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
