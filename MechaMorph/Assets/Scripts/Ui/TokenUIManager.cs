using TMPro;
using UnityEngine;

namespace TrippleTrinity.MechaMorph.Ui
{
    public class TokenUIManager : MonoBehaviour
    {
        public static TokenUIManager Instance { get; private set; }

        [SerializeField] private TMP_Text tokenCountText; // Assign in Inspector

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