using TMPro;
using UnityEngine;
using System.Collections; // Needed for Coroutine

namespace TrippleTrinity.MechaMorph.MyAsset.Scripts.Ui
{
    public class TokenUIManager : MonoBehaviour
    {
        public static TokenUIManager Instance { get; private set; }

        [SerializeField] private TMP_Text tokenCountText; // Assign in Inspector

        private int _currentGameTokens; // Tokens earned in the current session
        private int _totalTokens; // Total saved tokens

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                // LoadTotalTokens(); // If you plan to load saved tokens later
                
                // Initialize the UI with 0 tokens
                _currentGameTokens = 0;
                UpdateTokenCount(_currentGameTokens);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void UpdateTokenCount(int count)
        {
            _currentGameTokens = count; // Ensure current session tokens are updated

            if (tokenCountText != null)
            {
                // Update text color and format
                tokenCountText.text = $"<color=#004DFF>Tokens: {_currentGameTokens:D2}</color>";
                Debug.Log($"UI Updated: Tokens = {_currentGameTokens}");

                // Start the pop animation
                StopAllCoroutines(); // In case previous animation still running
                StartCoroutine(AnimateTokenText());
            }

        }

        public void ResetTokenCount()
        {
            _currentGameTokens = 0; // Reset the variable to ensure session tokens start at 0
            UpdateTokenCount(0);
        }

        public void AddToken()
        {
            UpdateTokenCount(_currentGameTokens + 1);
        }

        public void SaveFinalTokenCount()
        {
            _totalTokens += _currentGameTokens; // Add session tokens to total
            PlayerPrefs.SetInt("TotalTokens", _totalTokens);
            PlayerPrefs.Save();
            Debug.Log($"Game Over! Total Tokens Saved: {_totalTokens}");

            ResetTokenCount(); // Ensure new game starts from 0
        }

        public int CurrentTokenCount()
        {
            return _currentGameTokens;
        }

        public int GetTotalTokens()
        {
            return _totalTokens;
        }

        // ===== Animation Coroutine =====
        private IEnumerator AnimateTokenText()
        {
            Vector3 originalScale = tokenCountText.transform.localScale;

            // Scale up a little
            tokenCountText.transform.localScale = originalScale * 1.2f;

            // Wait for a short time
            yield return new WaitForSeconds(0.1f);

            // Scale back to normal
            tokenCountText.transform.localScale = originalScale;
        }
    }
}
