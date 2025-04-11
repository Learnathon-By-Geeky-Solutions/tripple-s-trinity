using UnityEngine;
using TMPro;

namespace TrippleTrinity.MechaMorph
{
    public class HighestScore : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI highScoreText;

        private void Start()
        {
            int highScore = PlayerPrefs.GetInt("HighScore", 0);

            if (highScoreText != null)
            {
                highScoreText.text = $"Highest Score: {highScore}";
            }
        }
    }
}