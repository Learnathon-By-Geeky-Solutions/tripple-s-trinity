using UnityEngine;

namespace TrippleTrinity.MechaMorph.SaveManager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public int Score { get; set; }
        public int TokenCount { get; set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (Instance != this)
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