using UnityEngine;

namespace TheDeveloperTrain.SciFiGuns
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 20f;
        [SerializeField] private float mouseSensitivity = 2f;

        [SerializeField] Vector2 xAxisClamp = new Vector2(-100, 100);
        [SerializeField] private Vector2 yAxisClamp = new Vector2(-100, 100);
        [SerializeField] private Vector2 zAxisClamp = new Vector2(-100, 100);

        private float rotationX = 0f;
        private float rotationY = 0f;

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            // Mouse look
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            rotationX -= mouseY;
            rotationY += mouseX;
            rotationX = Mathf.Clamp(rotationX, -90f, 90f);

            transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0f);


            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            Vector3 move = transform.forward * moveZ + transform.right * moveX;
            transform.position += moveSpeed * Time.deltaTime * move;

            transform.position = new Vector3(Mathf.Clamp(transform.position.x, xAxisClamp.x, xAxisClamp.y), Mathf.Clamp(transform.position.y, yAxisClamp.x, yAxisClamp.y), Mathf.Clamp(transform.position.z, zAxisClamp.x, zAxisClamp.y));


        }
    }
}
