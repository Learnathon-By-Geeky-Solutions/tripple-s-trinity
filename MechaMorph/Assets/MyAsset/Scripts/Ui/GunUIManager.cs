using System.Collections;
using TMPro;
using UnityEngine;

namespace TrippleTrinity.MechaMorph.Ui
{
    public class GunUIManager : MonoBehaviour
    {
        private static GunUIManager _instance;

        [SerializeField] private TextMeshProUGUI gunNameText;
        [SerializeField] private TextMeshProUGUI gunStatusText;
        private Coroutine _hideCoroutine;

        public static GunUIManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("GunUIManager instance is null.");
                }
                return _instance;
            }
            private set => _instance = value;
        }

        private void Awake()
        {
            if (_instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject); // Optional safety
            }
        }

        public void UpdateGunName(string message)
        {
            gunNameText.text = message;
            gunNameText.alpha = 1f;

            if (_hideCoroutine != null)
                StopCoroutine(_hideCoroutine);

            _hideCoroutine = StartCoroutine(HideGunLogAfterDelay(0.5f));
        }

        public void UpdateGunStatus(string message)
        {
            gunStatusText.text = message;
        }

        private IEnumerator HideGunLogAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);

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
