using TMPro;
using TrippleTrinity.MechaMorph.MyAsset.Scripts.SaveManager;
using UnityEngine;

// Needed to access SaveSystem

namespace TrippleTrinity.MechaMorph.MyAsset.Scripts.Ui
{
    public class HighestScore : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI highScoreText;

        private void Start()
        {
            int highScore = SaveSystem.LoadGame()?.highScore ?? 0;
            if (highScoreText != null)
            {
                highScoreText.text = $"Highest Score: {highScore}";
            }
        }
    }
}