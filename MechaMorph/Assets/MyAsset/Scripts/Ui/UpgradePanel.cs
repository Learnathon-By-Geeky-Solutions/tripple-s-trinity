using TMPro;
using TrippleTrinity.MechaMorph.Token;
using UnityEngine;

namespace TrippleTrinity.MechaMorph.MyAsset.Scripts.Ui
{
    public class UpgradePanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI totalTokenText;

        public static UpgradePanel Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            UpdateTotalTokenDisplay();
        }

        public void UpdateTotalTokenDisplay()
        {
            if (totalTokenText != null)
            {
                int totalTokens = UpgradeManager.Instance.GetTotalUpgradeTokenCount();
                totalTokenText.text = $"Tokens: {totalTokens}";
            }
            else
            {
                Debug.LogError("UpgradePanel: totalTokenText is not assigned!");
            }
        }
    }
}