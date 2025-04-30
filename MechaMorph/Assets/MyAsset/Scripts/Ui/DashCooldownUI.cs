using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace TrippleTrinity.MechaMorph.MyAsset.Scripts.Ui
{
    public class DashCooldownUI : MonoBehaviour
    {
        private static DashCooldownUI _instance; // Private backing field for the singleton instance

        public static DashCooldownUI Instance { get; private set; }

        [Header("Cooldown UI")]
        [SerializeField] private Image cooldownBar; // Reference to the cooldown bar UI

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

            // Initialize the cooldown bar to empty
            if (cooldownBar != null)
            {
                cooldownBar.fillAmount = 0f;
            }
        }

        public void StartCooldown(float cooldownTime)
        {
            StartCoroutine(UpdateCooldownBar(cooldownTime));
        }

        private IEnumerator UpdateCooldownBar(float cooldownTime)
        {
            // Set the bar to full (1) at the start of cooldown
            if (cooldownBar != null)
            {
                cooldownBar.fillAmount = 1f;
            }

            float elapsedTime = 0f;

            while (elapsedTime < cooldownTime)
            {
                elapsedTime += Time.deltaTime;
                if (cooldownBar != null)
                {
                    // Gradually decrease fill amount based on elapsed time
                    cooldownBar.fillAmount = 1 - (elapsedTime / cooldownTime);
                }
                yield return null;
            }

            // Ensure bar is empty at the end
            if (cooldownBar != null)
            {
                cooldownBar.fillAmount = 0f;
            }
        }
    }
    
}
