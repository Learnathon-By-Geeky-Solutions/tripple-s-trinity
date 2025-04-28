using UnityEngine;

namespace TheDeveloperTrain.SciFiGuns
{
    public class NameTMPRotation : MonoBehaviour
    {
        private Transform cameraTransform;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            cameraTransform = Camera.main.transform;
        }

        // Update is called once per frame
        void Update()
        {
            transform.LookAt(cameraTransform);
        }
    }
}