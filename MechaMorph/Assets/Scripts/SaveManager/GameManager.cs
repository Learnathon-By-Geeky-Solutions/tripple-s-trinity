using UnityEngine;

namespace TrippleTrinity.MechaMorph.SaveManager
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        public static GameManager Instance => _instance;

        public int Score { get; set; }
        public int TokenCount { get; set; }

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
            Score = 0;
            TokenCount = 0;
            Debug.Log("Game values reset.");
        }
    }
}