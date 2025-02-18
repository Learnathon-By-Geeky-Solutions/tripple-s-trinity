using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace TrippleTrinity.MechaMorph.Ui
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