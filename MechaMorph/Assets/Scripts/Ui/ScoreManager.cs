using TMPro;
using UnityEngine;
using TrippleTrinity.MechaMorph.SaveManager;

namespace TrippleTrinity.MechaMorph.Ui
{
    public class ScoreManager : MonoBehaviour
    {
        private static ScoreManager _instance;
        private GameData _gameData;
        private int _score;

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
                _gameData = SaveSystem.LoadGame();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            UpdateScoreUI();
        }

        public void SaveCurrentScore()
        {
            _gameData.score = _score;
            _gameData.highScore = Mathf.Max(_score, _gameData.highScore);
            SaveSystem.SaveGame(_gameData);
        }

        public void AddScore(int points)
        {
            if (points < 0)
            {
                Debug.LogWarning("Cannot add negative points.");
                return;
            }
            _gameData.score += points;

            // Update the high score if the current score is greater
            if (_gameData.score > _gameData.highScore)
            {
                _gameData.highScore = _gameData.score;
            }

            SaveSystem.SaveGame(_gameData); // Save all progress
            UpdateScoreUI();
        }

        private void UpdateScoreUI()
        {
            if (scoreText != null)
            {
                scoreText.text = $"Score: {_gameData.score}";
            }

            if (highScoreText != null)
            {
                highScoreText.text = $"Highest Score: {_gameData.highScore}";
            }
        }

        public void ResetScore()
        {
            _gameData.score = 0;
            UpdateScoreUI();
            SaveSystem.SaveGame(_gameData);
        }

        public int CurrentScore()
        {
            return _gameData.score;
        }

        
    }
}
