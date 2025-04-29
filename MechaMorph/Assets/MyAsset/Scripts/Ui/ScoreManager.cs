using TMPro;
using TrippleTrinity.MechaMorph.MyAsset.Scripts.SaveManager;
using TrippleTrinity.MechaMorph.Ui;
using UnityEngine;

namespace TrippleTrinity.MechaMorph.MyAsset.Scripts.Ui
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance { get; private set; }

        private int _score;
        private int _highscore;

        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI highScoreText;

        public int CurrentScore => _score;
        public int HighScore => _highscore;

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
            LoadScoreFromSave();
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
            if (_score > _highscore)
                _highscore = _score;

            UpdateScoreUI();
        }

        private void UpdateScoreUI()
        {
            if (scoreText != null)
                scoreText.text = $"<color=#004DFF>Score: {_score:D2}</color>";

            if (highScoreText != null)
                highScoreText.text = $"Highest Score: {_highscore:D2}";
        }

        public void ResetScore()
        {
            _score = 0;
            UpdateScoreUI();
        }

        public void LoadScoreFromSave()
        {
            GameData data = SaveSystem.LoadGame();
            _score = data?.score ?? 0;
            _highscore = data?.highScore ?? 0;
        }

        public void SaveScoreToFile()
        {
            SaveSystem.SaveGame();
        }

        // 🔧 Fix: Add SetScore method
        public void SetScore(int score)
        {
            _score = score;
            if (_score > _highscore)
                _highscore = _score;
            UpdateScoreUI();
        }
        public void LoadScore(int score, int highScore)
        {
            _score = score;
            _highscore = highScore;
            UpdateScoreUI();
        }

    }
}
