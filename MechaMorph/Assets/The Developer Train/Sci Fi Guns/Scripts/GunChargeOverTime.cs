using System.Collections;
using System.Linq;
using UnityEngine;
namespace TheDeveloperTrain.SciFiGuns
{
    public class GunChargeOverTime : MonoBehaviour
    {
        private Gun gun;

        /// <summary>
        /// the intensity of the glow of the glowing substances when at rest
        /// </summary>
        [Tooltip("the intensity of the glow of the glowing substances when at rest")]
        [SerializeField] private float glowBaseIntensity;

        /// <summary>
        /// the intensity of the glow of the glowing substances when fully charged
        /// </summary>
        [Tooltip("the intensity of the glow of the glowing substances when fully charged")]
        [SerializeField] private float glowMaxIntensity;

        /// <summary>
        /// An Animation Curve to define the transition between the intensity between when at rest and when fully charged
        /// </summary>
        [Tooltip("An Animation Curve to define the transition between the intensity between when at rest and when fully charged")]
        [SerializeField] private AnimationCurve glowScaling;

        [SerializeField] private int indexOfGlowMaterial = 0;

        /// <summary>
        /// If true the gun will start at max glow and then reduce its glow over the "Charging" phase
        /// </summary>
        [Tooltip("If true the gun will start at max glow and then reduce its glow over the \"Charging\" phase")]
        public bool invertGlow = false;

        private Color glowColor;
        private Material material;
        private float transitionFactor = 0.0f;
        private Coroutine reloadCoroutine;

        private void Start()
        {
            gun = GetComponentInParent<Gun>();
            Material[] materials = GetComponent<Renderer>().materials;
            material = materials.ElementAt(indexOfGlowMaterial);

            // Subscribe to gun events
            gun.onGunReloadStart += OnGunReloadStart;

            glowColor = material.GetColor("_EmissionColor");
            material.EnableKeyword("_EMISSION");

            float initialIntensity = invertGlow ? glowMaxIntensity : glowBaseIntensity;
            material.SetColor("_EmissionColor", glowColor * initialIntensity);
        }

        private void OnGunReloadStart()
        {

            if (reloadCoroutine != null)
            {
                StopCoroutine(reloadCoroutine);
            }
            reloadCoroutine = StartCoroutine(ReloadGlowTransition());
        }

        private IEnumerator ReloadGlowTransition()
        {
            float duration = gun.stats.reloadDuration;
            float elapsedTime = 0f;

            Color initialColor = material.GetColor("_EmissionColor");
            Color targetColor = glowColor * (invertGlow ? glowMaxIntensity : glowBaseIntensity);
            while (elapsedTime < duration)
            {
                float t = elapsedTime / duration;
                material.SetColor("_EmissionColor", Color.Lerp(initialColor, targetColor, t));

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            material.SetColor("_EmissionColor", targetColor);
        }

        private void Update()
        {
            if (gun.isReloading) return;

            float glowIntensity = Mathf.Lerp(
                invertGlow ? glowBaseIntensity : glowMaxIntensity,
                invertGlow ? glowMaxIntensity : glowBaseIntensity,
                glowScaling.Evaluate(transitionFactor)
            );
            material.SetColor("_EmissionColor", glowColor * glowIntensity);

            float magazineSize = gun.stats.magazineSize;
            if (magazineSize > 0)
            {
                transitionFactor = invertGlow
                    ? gun.currentBulletCount / magazineSize
                    : 1 - (gun.currentBulletCount / magazineSize);
            }
            else
            {
                transitionFactor = invertGlow ? 0 : 1;
            }
        }

        private void OnDestroy()
        {
            if (gun != null)
            {
                gun.onGunReloadStart -= OnGunReloadStart;
            }
        }
    }
}