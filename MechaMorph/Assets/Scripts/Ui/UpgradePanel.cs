using TMPro;
using TrippleTrinity.MechaMorph.Token;
using UnityEngine;

namespace TrippleTrinity.MechaMorph.Ui
{
    public class UpgradePanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI totalTokenText; // Assign in Inspector

        private void Start()
        {
            UpdateTotalTokenDisplay();
        }

        private void UpdateTotalTokenDisplay()
        {
            if (totalTokenText != null)
            {
                int totalTokens = UpgradeManager.Instance.GetTotalUpgradeTokenCount(); // Get total from PlayerPrefs
                totalTokenText.text = $"Tokens: {totalTokens}";
            }
            else
            {
                Debug.LogError("UpgradePanel: totalTokenText is not assigned!");
            }
        }
    }
}