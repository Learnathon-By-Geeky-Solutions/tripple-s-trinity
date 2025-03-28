using TMPro;
using UnityEngine;
using TrippleTrinity.MechaMorph.SaveManager;

namespace TrippleTrinity.MechaMorph.Ui
{
    public class ScoreManager : MonoBehaviour
    {
        private static ScoreManager _instance;
        private int _score;
        private const string HighScoreKey = "HighScore";

        [SerializeField] private TextMeshProUGUI scoreText; 
        [SerializeField] private TextMeshProUGUI highScoreText; // UI for highest score
        
        public static ScoreManager Instance 
        { 
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("ScoreManager instance is null.");
                }
                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
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
            if (_score > PlayerPrefs.GetInt(HighScoreKey, 0))
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
            _score = 0; // Start fresh for new gameplay
            UpdateScoreUI();
        }

        public void SaveScore()
        {
            SaveSystem.SaveGame(_score, TokenUIManager.Instance.CurrentTokenCount());
        }
    }
}
