using TrippleTrinity.MechaMorph.MyAsset.Scripts.Ui;
using UnityEngine;

namespace TrippleTrinity.MechaMorph.MyAsset.Scripts.SaveManager
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

                LoadGameData(); // 👈 Load saved data when the game starts
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        private void LoadGameData()
        {
            GameData data = SaveSystem.LoadGame();

            if (data != null)
            {
                Score = data.score;
                TokenCount = data.tokenCount;

                ScoreManager.Instance?.SetScore(data.score);
                TokenUIManager.Instance?.LoadTokenCount(data.tokenCount);
                UpgradeManager.Instance?.LoadUpgradeLevels(data.areaDamageLevel, data.boosterCooldownLevel);
            }
            else
            {
                Debug.Log("No saved game data found.");
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