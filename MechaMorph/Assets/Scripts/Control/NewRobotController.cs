using UnityEngine;
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
        [SerializeField] private InputActionAsset playerInput;

        private Rigidbody _rb;
        private bool _isGrounded;
        private Vector2 _moveInput;
        private bool _jumpPressed;

        private InputAction _moveAction;
        private InputAction _jumpAction;

        void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.freezeRotation = true;
            _rb.interpolation = RigidbodyInterpolation.Interpolate;
            _rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

            // Find input actions
            _moveAction = playerInput.FindAction("Move");
            _jumpAction = playerInput.FindAction("Jump");
        }

        void OnEnable()
        {
            if (_moveAction != null)
            {
                _moveAction.performed += MovePerformed;
                _moveAction.canceled += MoveCanceled;
                _moveAction.Enable();
            }
            else
            {
                Debug.LogError("Move action not found in InputActionAsset!");
            }

            if (_jumpAction != null)
            {
                _jumpAction.performed += JumpPerformed;
                _jumpAction.Enable();
            }
            else
            {
                Debug.LogError("Jump action not found in InputActionAsset!");
            }
        }

        void OnDisable()
        {
            if (_moveAction != null)
            {
                _moveAction.performed -= MovePerformed;
                _moveAction.canceled -= MoveCanceled;
                _moveAction.Disable();
            }

            if (_jumpAction != null)
            {
                _jumpAction.performed -= JumpPerformed;
                _jumpAction.Disable();
            }
        }

        private void MovePerformed(InputAction.CallbackContext ctx)
        {
            _moveInput = ctx.ReadValue<Vector2>();
        }

        private void MoveCanceled(InputAction.CallbackContext _) // Use "_" to avoid unused parameter warning
        {
            _moveInput = Vector2.zero;
        }

        private void JumpPerformed(InputAction.CallbackContext ctx)
        {
            _jumpPressed = true;
        }

        void FixedUpdate()
        {
            CheckGrounded();
            Move();
            RotateToMouse();

            if (_jumpPressed && _isGrounded)
            {
                Jump();
                _jumpPressed = false;
            }
        }

        private void CheckGrounded()
        {
            _isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
        }

        private void Move()
        {
            Vector3 moveDirection = new Vector3(_moveInput.x, 0, _moveInput.y).normalized; // Normalize to avoid faster diagonal movement

            if (moveDirection.magnitude > 0.1f)
            {
                // Set the velocity directly to maintain constant speed
                _rb.velocity = new Vector3(moveDirection.x * moveSpeed, _rb.velocity.y, moveDirection.z * moveSpeed);
            }
            else if (_isGrounded)
            {
                // Stop horizontal movement when grounded and no input
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
            _rb.velocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z); // Reset vertical velocity
            _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Apply jump force
        }

        void OnDrawGizmosSelected()
        {
            if (groundCheck != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
            }
        }
    }
}
