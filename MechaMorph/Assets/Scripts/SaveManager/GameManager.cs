using UnityEngine;

namespace TrippleTrinity.MechaMorph.SaveManager
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        public static GameManager Instance => _instance;

         private int score;
        private int tokenCount;

        public int Score
        {
            get => score;
            set => score = value;
        }

        public int TokenCount
        {
            get => tokenCount;
            set => tokenCount = value;
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

        public void ResetGame()
        {
            score = 0;
            tokenCount = 0;
            Debug.Log("Game values reset.");
        }
    }
}