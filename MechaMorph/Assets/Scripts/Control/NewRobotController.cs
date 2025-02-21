using UnityEngine;
using TrippleTrinity.MechaMorph.InputHandling;
using UnityEngine.InputSystem;

namespace TrippleTrinity.MechaMorph.Control
{
    [RequireComponent(typeof(Rigidbody))]
    public class NewRobotController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float rotationSpeed = 10f;
        [SerializeField] private float jumpForce = 7f;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundCheckRadius = 0.3f;

        private Rigidbody _rb;
        private bool _isGrounded;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.freezeRotation = true;
            _rb.interpolation = RigidbodyInterpolation.Interpolate;
            _rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }

        private void FixedUpdate()
        {
            CheckGrounded();
            Move();
            RotateToMouse();

            if (InputHandler.Instance.IsJumpPressed() && _isGrounded)
            {
                Jump();
                InputHandler.Instance.ResetJump();  // Reset after handling
            }
        }

        private void CheckGrounded()
        {
            _isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
        }

        private void Move()
        {
            Vector2 moveInput = InputHandler.Instance.GetMoveInput();
            Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y).normalized;

            if (moveDirection.magnitude > 0.1f)
            {
                _rb.velocity = new Vector3(moveDirection.x * moveSpeed, _rb.velocity.y, moveDirection.z * moveSpeed);
            }
            else if (_isGrounded)
            {
                _rb.velocity = new Vector3(0, _rb.velocity.y, 0);
            }
        }

        private void RotateToMouse()
        {
            Camera activeCamera = Camera.main;
            if (activeCamera == null) return;

            Ray ray = activeCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                Vector3 targetDirection = hitInfo.point - transform.position;
                targetDirection.y = 0f;

                if (targetDirection.sqrMagnitude > 0.1f)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                    _rb.rotation = Quaternion.Slerp(_rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
                }
            }
        }

        private void Jump()
        {
            _rb.velocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);
            _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        private void OnDrawGizmosSelected()
        {
            if (groundCheck != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
            }
        }
    }
}
