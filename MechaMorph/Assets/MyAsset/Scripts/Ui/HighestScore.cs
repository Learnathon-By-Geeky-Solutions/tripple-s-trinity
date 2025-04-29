using UnityEngine;
using TMPro;
using TrippleTrinity.MechaMorph.SaveManager;

namespace TrippleTrinity.MechaMorph.Ui
{
    public class HighestScore : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI highScoreText;

        private void Start()
        {
            GameData gameData = SaveSystem.LoadGame();
            if (gameData != null)
            {
                if (highScoreText != null)
                {
                    highScoreText.text = $"Highest Score: {gameData.highScore}";

                }
                
            }
        }
    }
}