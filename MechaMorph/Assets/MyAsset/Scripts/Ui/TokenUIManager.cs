using TMPro;
using UnityEngine;
using System.Collections;
using TrippleTrinity.MechaMorph.SaveManager; // Needed for Coroutine

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
                GameData data = SaveSystem.LoadGame();
                _totalTokens = data?.tokenCount ?? 0; // Load saved tokens or default 0

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
            else
            {
                Debug.LogError("TokenUIManager: tokenCountText is not assigned!");
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
            GameData data = SaveSystem.LoadGame();
            if (data == null)
            {
                data = new GameData();
            }

            data.tokenCount += _currentGameTokens; // Add session tokens to total

            SaveSystem.SaveGame(data);
            Debug.Log($"Game Over! Total Tokens Saved: {data.tokenCount}");

            _totalTokens = data.tokenCount; // Update local copy
            ResetTokenCount();
        }// Reset for next game}

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
