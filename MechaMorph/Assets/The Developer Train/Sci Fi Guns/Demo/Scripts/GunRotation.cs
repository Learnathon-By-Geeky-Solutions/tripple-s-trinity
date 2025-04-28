using UnityEngine;

namespace TheDeveloperTrain.SciFiGuns
{
    public class GunRotation : MonoBehaviour
    {
        [SerializeField] private float speed = 1.0f;
        private float rotation = 0.0f;
        void Update()
        {
            rotation += Time.deltaTime * speed;
            transform.rotation = Quaternion.Euler(0, rotation, 0);
        }
    }
}