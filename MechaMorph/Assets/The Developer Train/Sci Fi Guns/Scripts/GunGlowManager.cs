using System.Linq;
using UnityEngine;

namespace TheDeveloperTrain.SciFiGuns
{
    public class GunGlowManager : MonoBehaviour
    {
        private Gun gun;

        [SerializeField] private float glowBaseIntensity = 0.2f;
        [SerializeField] private float glowMaxIntensity = 2f;
        [SerializeField] private AnimationCurve glowScaling = AnimationCurve.EaseInOut(0, 0, 1, 1);
        public bool invertGlow = false;

        [Tooltip("Ratio between the charge and discharge speeds (discharge is faster if > 1)")]
        public float chargingDischargingSpeedRatio = 1.0f;

        public float speed = 1.0f;

        private float transitionFactor = 0.0f;
        private bool isChargingUp = false;

        private Color glowColor;
        private Material material;

        private void Start()
        {
            gun = GetComponentInParent<Gun>();
            Material[] materials = GetComponent<Renderer>().materials;
            material = materials.Last();
            gun.onGunShootingStart += OnGunShootingStart;

            glowColor = material.GetColor("_EmissionColor");
            material.EnableKeyword("_EMISSION");

            float startIntensity = invertGlow ? glowMaxIntensity : glowBaseIntensity;
            material.SetColor("_EmissionColor", glowColor * startIntensity);
        }

        private void OnGunShootingStart()
        {
            isChargingUp = true;
        }

        private void Update()
        {
            float direction = isChargingUp ? 1f : -1f * chargingDischargingSpeedRatio;
            transitionFactor += Time.deltaTime * speed * direction;
            transitionFactor = Mathf.Clamp01(transitionFactor);

            float t = glowScaling.Evaluate(transitionFactor);
            float glowIntensity = invertGlow
                ? Mathf.Lerp(glowMaxIntensity, glowBaseIntensity, t)
                : Mathf.Lerp(glowBaseIntensity, glowMaxIntensity, t);

            material.SetColor("_EmissionColor", glowColor * glowIntensity);

            // Automatically flip direction when max glow is reached
            if (isChargingUp && transitionFactor >= 1.0f)
            {
                isChargingUp = false;
            }
        }

        private void OnDestroy()
        {
            if (gun != null)
                gun.onGunShootingStart -= OnGunShootingStart;
        }
    }
}
