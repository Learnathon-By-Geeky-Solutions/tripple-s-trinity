using TrippleTrinity.MechaMorph.InputHandling;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TrippleTrinity.MechaMorph.MyAsset.Scripts.Control
{
    [RequireComponent(typeof(Rigidbody))]
    public class NewRobotController : MonoBehaviour
    {
        private static readonly int Jump = Animator.StringToHash("Jump");
        private static readonly int IsWalking = Animator.StringToHash("isWalking");
        private Animator _animator;

        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float rotationSpeed = 10f;
        [SerializeField] private float jumpForce = 7f;

        [Header("Ground Check Settings")]
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundCheckRadius = 0.3f;

        [Header("Sound Settings")]
        [SerializeField] private AudioClip walkSound;  // Walking sound
        [SerializeField] private AudioClip jumpSound;  
        private AudioSource _audioSource;  // AudioSource for walking
        private AudioSource _sfxSource;    // AudioSource for jump and other SFX

        private Rigidbody _rb;
        private bool _isGrounded;

        private void Awake()
        {
            InitializeRigidbody();
            _animator = GetComponentInChildren<Animator>();

            // Setup walking audio source
            _audioSource = GetComponent<AudioSource>();

            // Create second audio source for SFX
            _sfxSource = gameObject.AddComponent<AudioSource>();
            _sfxSource.playOnAwake = false;
        }

        private void FixedUpdate()
        {
            PerformMovementCycle();
        }

        private void InitializeRigidbody()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.freezeRotation = true;
            _rb.interpolation = RigidbodyInterpolation.Interpolate;
            _rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }

        private void PerformMovementCycle()
        {
            CheckGrounded();
            HandleMovement();
            HandleRotation();
            HandleJump();
        }

        private void CheckGrounded()
        {
            _isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
        }

        private void HandleMovement()
        {
            Vector2 moveInput = InputHandler.Instance.GetMoveInput();
            Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y).normalized;

            bool isWalking = moveDirection.sqrMagnitude > 0.01f;  // Check if movement input is greater than a small threshold

            if (isWalking)
            {
                _rb.velocity = new Vector3(moveDirection.x * moveSpeed, _rb.velocity.y, moveDirection.z * moveSpeed);
                PlayWalkingSound();
            }
            else if (_isGrounded)
            {
                _rb.velocity = new Vector3(0, _rb.velocity.y, 0);  // Stop the movement
                StopWalkingSound();  // Stop the sound if not walking
            }

            if (_animator != null)
            {
                _animator.SetBool(IsWalking, isWalking);
            }
        }

        private void PlayWalkingSound()
        {
            if (!_audioSource.isPlaying && walkSound != null)  // Only play if not already playing
            {
                _audioSource.clip = walkSound;
                _audioSource.loop = true;  // Loop walking sound
                _audioSource.Play();
            }
        }

        private void StopWalkingSound()
        {
            if (_audioSource.isPlaying)
            {
                _audioSource.Stop();
            }
        }

        private void HandleRotation()
        {
            Camera cam = Camera.main;
            if (cam == null) return;

            Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (!Physics.Raycast(ray, out RaycastHit hitInfo)) return;

            Vector3 direction = hitInfo.point - transform.position;
            direction.y = 0;

            if (direction.sqrMagnitude > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                _rb.rotation = Quaternion.Slerp(_rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            }
        }

        private void HandleJump()
        {
            if (!InputHandler.Instance.IsJumpPressed() || !_isGrounded) return;

            _rb.velocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);
            _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            InputHandler.Instance.ResetJump();

            if (_animator != null)
            {
                _animator.SetTrigger(Jump);
            }

            PlayJumpSound();  // Play jump sound
        }

        private void PlayJumpSound()
        {
            if (jumpSound != null && _sfxSource != null)
            {
                _sfxSource.PlayOneShot(jumpSound);  // Play jump sound independently
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (groundCheck == null) return;

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
