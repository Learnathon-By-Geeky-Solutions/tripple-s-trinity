using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RobotController : MonoBehaviour
{
    public float moveSpeed = 5f;                 // Movement speed
    public float rotationSpeed = 10f;           // Speed of rotation
    public float jumpForce = 7f;                // Force for jumping
    public LayerMask groundLayer;               // Layer for ground detection
    public Transform groundCheck;               // Ground check position
    public float groundCheckRadius = 0.3f;      // Ground check radius

    private Rigidbody rb;
    private bool isGrounded;
    private Vector3 movementInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;               // Prevent unintended rotations
        rb.interpolation = RigidbodyInterpolation.Interpolate; // Smooth motion updates
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous; // Precise collision detection
    }

    void Update()
    {
        // Handle player input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        movementInput = new Vector3(horizontalInput, 0, verticalInput).normalized;

        // Handle jump input
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
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
            isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
        }
    }

    private void Move()
    {
        if (movementInput.magnitude >= 0.1f)
        {
            // Calculate target velocity
            Vector3 targetVelocity = movementInput * moveSpeed;

            // Smoothly change velocity while preserving vertical motion
            Vector3 velocityChange = targetVelocity - new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(velocityChange, ForceMode.VelocityChange);
        }
        else if (isGrounded)
        {
            // Stop horizontal movement when grounded and no input
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
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
                rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            }
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); // Reset vertical velocity
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);      // Apply jump force
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
