using UnityEngine;
using TMPro;
using System.Collections;

namespace TrippleTrinity.MechaMorph
{
    public class GunUIManager : MonoBehaviour
    {
        public static GunUIManager Instance;

        [SerializeField] private TextMeshProUGUI gunNameText;
        [SerializeField] private TextMeshProUGUI gunStatusText;
        private Coroutine hideCoroutine;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        public void UpdateGunName(string message)
        {
            gunNameText.text = message;
            gunNameText.alpha = 1f; 

            
            if (hideCoroutine != null)
                StopCoroutine(hideCoroutine);

            hideCoroutine = StartCoroutine(HideGunLogAfterDelay(0.5f));
        }

        public void UpdateGunStatus(string message)
        {
            gunStatusText.text = message;
        }
        private IEnumerator HideGunLogAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);

            // Optional: fade-out effect
            float fadeDuration = 1f;
            float t = 0f;

            while (t < fadeDuration)
            {
                t += Time.deltaTime;
                gunNameText.alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);
                yield return null;
            }

            gunNameText.alpha = 0f;
        }
    }
}
