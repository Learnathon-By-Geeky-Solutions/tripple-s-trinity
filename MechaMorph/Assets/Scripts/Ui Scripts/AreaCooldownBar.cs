using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Ui_Scripts
{
    public class AreaCooldownBar : MonoBehaviour
    {
        [Header("UI Components")]
        [SerializeField] private Image cooldownBarImage; // Reference to the cooldown bar image

        [Header("Smooth Transition Settings")]
        [SerializeField] private float smoothDuration = 0.5f; // Duration of the smooth transition in seconds

        private Coroutine _smoothTransitionCoroutine;

        private void Start()
        {
            // Initialize the cooldown bar to 0
            if (cooldownBarImage != null)
            {
                cooldownBarImage.fillAmount = 0f;
            }
        }

        /// <summary>
        /// Smoothly transitions the fill amount of the cooldown bar.
        /// </summary>
        /// <param name="targetFillAmount">The target fill amount (value between 0 and 1).</param>
        public void SetFillAmount(float targetFillAmount)
        {
            // Stop any ongoing transition
            if (_smoothTransitionCoroutine != null)
            {
                StopCoroutine(_smoothTransitionCoroutine);
            }

            // Start _a new smooth transition
            _smoothTransitionCoroutine = StartCoroutine(SmoothFill(targetFillAmount));
        }

        /// <summary>
        /// Coroutine to smoothly transition the fill amount.
        /// </summary>
        /// <param name="targetFillAmount">The target fill amount (value between 0 and 1).</param>
        private IEnumerator SmoothFill(float targetFillAmount)
        {
            if (cooldownBarImage == null) yield break;

            float startFillAmount = cooldownBarImage.fillAmount;
            float elapsedTime = 0f;

            while (elapsedTime < smoothDuration)
            {
                elapsedTime += Time.deltaTime;
                cooldownBarImage.fillAmount = Mathf.Lerp(startFillAmount, targetFillAmount, elapsedTime / smoothDuration);
                yield return null;
            }

            // Ensure the final value is set precisely
            cooldownBarImage.fillAmount = targetFillAmount;
        }
    }
}
