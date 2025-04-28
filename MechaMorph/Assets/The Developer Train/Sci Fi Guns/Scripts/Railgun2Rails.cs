using UnityEngine;

namespace TheDeveloperTrain.SciFiGuns
{
    public class Railgun2Rails : MonoBehaviour
    {
        /// <summary>
        /// The Transform of the final position of the rail in it's shoot animation, only the y value is extracted from it
        /// </summary>
        [Tooltip("The Transform of the final position of the rail in it's shoot animation, only the y value is extracted from it")]
        [SerializeField] private Transform railEndTransform;
        private Vector3 railEndPosition;

        /// <summary>
        /// a speed multiplier of the lerp of the rail between its outer and inner positions
        /// </summary>
        [Tooltip("a speed multiplier of the lerp of the rail between its outer and inner positions")]
        [SerializeField] private float speed = 20.0f;

        private Vector3 railStartPosition;
        private bool isEffectActive = false;
        private bool isContracting = true;

        private float effectLerpFactor = 0.0f;

        private Gun railgun;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Start()
        {
            railgun = GetComponentInParent<Gun>();
            railStartPosition = transform.localPosition;
            railEndPosition = railEndTransform.position;
            railgun.onBulletShot += EnableEffect;
            Debug.Log($"Start Y: {railStartPosition.y}, End Y: {railEndPosition.y}");
        }

        private void Update()
        {
            if (isEffectActive && isContracting)
            {
                effectLerpFactor += Time.deltaTime * speed;
                transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(railStartPosition.y, railEndPosition.y, effectLerpFactor), transform.localPosition.z);
            }
            else if (isEffectActive && !isContracting)
            {
                effectLerpFactor -= Time.deltaTime * speed;
                transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(railStartPosition.y, railEndPosition.y, effectLerpFactor), transform.localPosition.z);
                Debug.Log("effect Factor = " + effectLerpFactor);
                Debug.Log("transform: " + transform.localPosition);
                Debug.Log("rail end: " + railEndPosition);
                Debug.DrawLine(transform.position, transform.position + Vector3.up * 0.2f, Color.green);

            }
            if (effectLerpFactor > 1.0f && isContracting)
            {
                isContracting = false;

            }
            else if (effectLerpFactor < 0.0f && !isContracting)
            {
                effectLerpFactor = 0.0f;
                isEffectActive = false;
                isContracting = true;
            }


        }

        private void EnableEffect()
        {
            isEffectActive = true;
        }

        private void OnDestroy()
        {
            railgun.onBulletShot -= EnableEffect;
        }
    }

}