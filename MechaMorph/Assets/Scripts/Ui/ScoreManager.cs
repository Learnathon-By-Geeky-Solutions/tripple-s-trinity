using TMPro;
using UnityEngine;

namespace TrippleTrinity.MechaMorph.Ui
{
    public class ScoreManager : MonoBehaviour
    {
        private static ScoreManager _instance;
        private int _score;
    
        [SerializeField] private TextMeshProUGUI scoreText; // UI text for displaying score
        
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
            UpdateScoreUI();
        }
        
        private void UpdateScoreUI()
        {
            if (scoreText == null)
            {
                Debug.LogWarning("Score text UI is not assigned.");
                return;
            }
            scoreText.text = $"Score: {_score}";
        }
        
        public int CurrentScore()
        {
            return _score;
        }
    }
}