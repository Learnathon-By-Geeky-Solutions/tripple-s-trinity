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
        private int _highScore;

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

            // Check if new score is higher than previous high score
            if (_score > _highScore)
            {
                _highScore = _score;
                SaveScore(); //save highscore
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
                highScoreText.text = $"Highest Score: {_highScore:D2}</color>";
            }
        }


        

        public int CurrentScore()
        {
            return _score;
        }

        public void LoadScore()
        {
            GameData data = SaveSystem.LoadGame();
            _score = 0;

            if (data != null)
            {
                _highScore = data.highScore; //Load high score from JSON
            }
            else
            {
                
                _highScore = 0;
            }

            UpdateScoreUI();
        }

        public void SaveScore()
        {
            if(_score > _highScore)
            {
                _highScore = _score;
            }

            GameData data = new GameData
            {
                highScore = _highScore,
                tokenCount = TokenUIManager.Instance.CurrentTokenCount()
            };

            SaveSystem.SaveGame(data);
            Debug.Log("Score Saved Successfully.");
        }
    }
}
