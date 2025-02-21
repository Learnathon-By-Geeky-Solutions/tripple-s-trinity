using UnityEngine;

namespace TrippleTrinity.MechaMorph.Token
{
    public enum TokenType { Health, Cooldown, Upgrade }

    public class Token : MonoBehaviour
    {
        [SerializeField] private TokenType tokenType;
        [SerializeReference] private float tokenValue; // Amount of health/cooldown/upgrade points

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) // Ensure Player has the correct tag
            {
                TokenCollector collector = other.GetComponent<TokenCollector>();
                if (collector != null)
                {
                    Debug.Log($"Player collected {tokenType} token with value {tokenValue}");
                    collector.CollectToken(tokenType, tokenValue);
                    Destroy(gameObject); // Remove token after collection
                }
            }
        }
    }
}