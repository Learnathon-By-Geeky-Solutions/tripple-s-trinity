using TMPro;
using TrippleTrinity.MechaMorph.MyAsset.Scripts.SaveManager;
using UnityEngine;

namespace TrippleTrinity.MechaMorph.MyAsset.Scripts.Ui
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