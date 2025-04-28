using TMPro;
using TrippleTrinity.MechaMorph.SaveManager;
using TrippleTrinity.MechaMorph.Ui;
using UnityEngine;

namespace TrippleTrinity.MechaMorph.MyAsset.Scripts.Ui
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
                scoreText.text = $"<color=#004DFF>Score: {_score:D2}</color>";
            }

            if (highScoreText != null)
            {
                int highScore = PlayerPrefs.GetInt(HighScoreKey, 0);
                highScoreText.text = $"Highest Score: {highScore:D2}</color>";
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
            _score = 0; // Reset or load saved score if needed
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
