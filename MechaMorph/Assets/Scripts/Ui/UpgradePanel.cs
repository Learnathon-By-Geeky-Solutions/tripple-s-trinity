using UnityEngine;
using TMPro;

namespace TrippleTrinity.MechaMorph.Ui
{
    public class UpgradePanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text tokenCountText; 

        private void Start()
        {
            UpdateTokenUI();
        }

        private void UpdateTokenUI()
        {
            int totalTokens = PlayerPrefs.GetInt("UpgradeTokenCount", 0);
            tokenCountText.text = $"Tokens: {totalTokens}";
        }
    }
}