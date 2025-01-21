using UnityEngine;

namespace TrippleTrinity.MechaMorph.Control
{
    public class CameraController : MonoBehaviour
    {
        public Transform target;                  // Target to follow (robot)
        [SerializeField] private Vector3 offset = new Vector3(0, 10, -10); // Fixed offset from the target
        [SerializeField] private float followSpeed = 5f;            // Speed of following
        [SerializeField] private float rotationSmoothTime = 0.1f;   // Time to smooth rotation

        private Vector3 _velocity = Vector3.zero;

        void LateUpdate()
        {
            if (target == null) return;

            // Calculate the desired position based on the offset
            Vector3 targetPosition = target.position + offset;

            // Smoothly move the camera to the target position
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, 1f / followSpeed);

            // Smoothly rotate the camera to look at the target
            Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSmoothTime);
        }
    }
}