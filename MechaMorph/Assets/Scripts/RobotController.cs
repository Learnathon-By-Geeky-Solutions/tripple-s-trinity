using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RobotController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;                 // Movement speed
    [SerializeField] private float rotationSpeed = 10f;           // Speed of rotation
    [SerializeField] private float jumpForce = 7f;                // Force for jumping
    [SerializeField] LayerMask groundLayer;               // Layer for ground detection
    [SerializeField] Transform groundCheck;               // Ground check position
    [SerializeField] float groundCheckRadius = 0.3f;      // Ground check radius

    private Rigidbody _rb;
    private bool _isGrounded;
    private Vector3 _movementInput;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;               // Prevent unintended rotations
        _rb.interpolation = RigidbodyInterpolation.Interpolate; // Smooth motion updates
        _rb.collisionDetectionMode = CollisionDetectionMode.Continuous; // Precise collision detection
    }

    void Update()
    {
        // Handle player input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        _movementInput = new Vector3(horizontalInput, 0, verticalInput).normalized;

        // Handle jump input
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        CheckGrounded();
        Move();
        RotateToMouse();
    }

    private void CheckGrounded()
    {
        if (groundCheck != null)
        {
            // Use a smooth ground check to prevent rapid toggling
            _isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
        }
    }

    private void Move()
    {
        if (_movementInput.magnitude >= 0.1f)
        {
            // Calculate target velocity
            Vector3 targetVelocity = _movementInput * moveSpeed;

            // Smoothly change velocity while preserving vertical motion
            Vector3 velocityChange = targetVelocity - new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
            _rb.AddForce(velocityChange, ForceMode.VelocityChange);
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

        Ray ray = activeCamera.ScreenPointToRay(Input.mousePosition);

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
        _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);      // Apply jump force
    }

    void OnDrawGizmosSelected()
    {
        // Visualize the ground check sphere in the editor
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
