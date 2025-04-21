using TMPro;
using UnityEngine;
using TrippleTrinity.MechaMorph.SaveManager;

namespace TrippleTrinity.MechaMorph.Ui
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance { get; private set; }

        private int _score;
        private const string HighScoreKey = "HighScore";

        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI highScoreText;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            LoadScore();
            UpdateScoreUI();
        }

        public void AddScore(int points)
        {
            if (points < 0)
            {
                Debug.LogWarning("Cannot add negative points.");
                return;
            }

            _score += points;

            // Update the high score if the current score is greater
            int currentHigh = PlayerPrefs.GetInt(HighScoreKey, 0);
            if (_score > currentHigh)
            {
                PlayerPrefs.SetInt(HighScoreKey, _score);
                PlayerPrefs.Save();
            }

            UpdateScoreUI();
        }

        private void UpdateScoreUI()
        {
            if (scoreText != null)
            {
                scoreText.text = $"Score: {_score}";
            }

            if (highScoreText != null)
            {
                int highScore = PlayerPrefs.GetInt(HighScoreKey, 0);
                highScoreText.text = $"Highest Score: {highScore}";
            }
        }

        public void ResetScore()
        {
            _score = 0;
            UpdateScoreUI();
        }

        public int CurrentScore()
        {
            return _score;
        }

        public void LoadScore()
        {
            _score = PlayerPrefs.GetInt("LastScore", 0); // Load last saved score or default to 0
            UpdateScoreUI();
        }


        public void SaveScore()
        {
            PlayerPrefs.SetInt("LastScore", _score); // Save current score
            PlayerPrefs.Save();
            SaveSystem.SaveGame(_score, TokenUIManager.Instance.CurrentTokenCount());
        }

    }
}
