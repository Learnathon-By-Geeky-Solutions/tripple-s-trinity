using TMPro;
using UnityEngine;

namespace TrippleTrinity.MechaMorph.Ui
{
    public class TokenUIManager : MonoBehaviour
    {
        public static TokenUIManager Instance;
        [SerializeField] private TextMeshProUGUI tokenCountText; // Assign in Inspector

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

        public void UpdateTokenCount(int count)
        {
            if (tokenCountText != null)
            {
                tokenCountText.text = $"Tokens: {count}";
                Debug.Log($"UI Updated: Tokens = {count}");
            }
            else
            {
                Debug.LogError("TokenUIManager: tokenCountText is not assigned!");
            }
        }
    }
}