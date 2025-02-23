using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using TrippleTrinity.MechaMorph.Damage;
using TrippleTrinity.MechaMorph.Token; // Import for UpgradeManager

namespace TrippleTrinity.MechaMorph.Ui
{
    public class GameOverManager : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private TextMeshProUGUI finalScoreText; // UI text for final score display
        [SerializeField] private TextMeshProUGUI finalTokenText; // UI text for collected tokens

        private Damageable _playerDamageable;
        private ScoreManager _scoreManager;

        private void Start()
        {
            _playerDamageable = FindObjectOfType<Damageable>(); // Find the player's health system
            _scoreManager = FindObjectOfType<ScoreManager>(); // Find ScoreManager

            if (_playerDamageable != null)
            {
                _playerDamageable.OnDeath += () => StartCoroutine(ShowGameOverWithDelay()); // Subscribe with delay
            }

            gameOverPanel.SetActive(false); // Hide the panel at the start
        }

        private IEnumerator ShowGameOverWithDelay()
        {
            yield return new WaitForSecondsRealtime(0.4f); // Wait 0.4 seconds before showing Game Over
            ShowGameOverScreen();
        }

        private void ShowGameOverScreen()
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f;

            // Display the final score
            if (finalScoreText != null && _scoreManager != null)
            {
                finalScoreText.text = $"Score: {_scoreManager.CurrentScore()}";
            }

            // Display the collected upgrade tokens for the session
            if (finalTokenText != null)
            {
                finalTokenText.text = $"Tokens: {UpgradeManager.Instance.GetUpgradeTokenCount()}";
            }
        }

        public void RestartGame()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the current scene
        }

        private void OnDestroy()
        {
            if (_playerDamageable != null)
            {
                _playerDamageable.OnDeath -= ShowGameOverScreen; // Unsubscribe from event
            }
        }
    }
}
