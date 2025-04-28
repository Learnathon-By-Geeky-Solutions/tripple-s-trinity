using System.Collections;
using TrippleTrinity.MechaMorph.InputHandling;
using TrippleTrinity.MechaMorph.Ui;
using UnityEngine;

namespace TrippleTrinity.MechaMorph.MyAsset.Scripts.Control
{
    public class NewBallControllerWithDash : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float movementSpeed = 10f;

        [Header("Dash Settings")]
        [SerializeField] private float dashMultiplier = 2f;
        [SerializeField] private float dashDuration = 0.5f;
        [SerializeField] private float dashCooldown = 2f;

        [Header("Effects")]
        [SerializeField] private ParticleSystem dashParticleEffect;
        [SerializeField] private AudioClip dashSound;
        [SerializeField] private AudioClip ballRollSound;  // Ball roll sound clip
        [SerializeField] private float rollSoundThreshold = 50f;  // Minimum speed to play sound

        private Rigidbody _rb;
        private AudioSource _audioSource;
        private bool _isDashing;
        private bool _isCooldownActive;
        private ParticleSystem _activeDashEffect;
        private Vector3 _dashDirection;

        private bool _isRolling;  // Flag to check if the ball is rolling

        void Awake()
        {
            InitializeComponents();
        }

        void FixedUpdate()
        {
            HandleMovementAndDash();
            FollowPlayerWithEffect();
            ManageRollSound();  // Manage sound based on movement/rotation
        }

        private void HandleMovementAndDash()
        {
            Vector2 moveInput = InputHandler.Instance.GetMoveInput();
            Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y);
            _rb.AddForce(movement * movementSpeed);

            if (CanDash(movement))
            {
                _dashDirection = movement.normalized;
                PerformDash();
                InputHandler.Instance.ResetDash();
            }

            // Check if the ball is moving or rotating, above the threshold
            _isRolling = (movement.sqrMagnitude > rollSoundThreshold || _rb.angularVelocity.sqrMagnitude > rollSoundThreshold);
        }

        private bool CanDash(Vector3 movement)
        {
            return InputHandler.Instance.IsDashPressed()
                   && !_isDashing
                   && !_isCooldownActive
                   && movement.sqrMagnitude > 0.01f;
        }

        private void FollowPlayerWithEffect()
        {
            if (_activeDashEffect != null)
            {
                _activeDashEffect.transform.position = transform.position;
            }
        }

        private void PerformDash()
        {
            StartCoroutine(DashCoroutine());
        }

        private IEnumerator DashCoroutine()
        {
            _isDashing = true;
            float originalSpeed = movementSpeed;
            movementSpeed *= dashMultiplier;

            PlayDashEffects();

            DashCooldownUI.Instance?.StartCooldown(dashCooldown);
            yield return new WaitForSeconds(dashDuration);

            movementSpeed = originalSpeed;
            _isDashing = false;

            StopAndDestroyEffect();

            _isCooldownActive = true;
            yield return new WaitForSeconds(dashCooldown);
            _isCooldownActive = false;
        }

        private void PlayDashEffects()
        {
            if (dashParticleEffect != null && _dashDirection != Vector3.zero)
            {
                _activeDashEffect = Instantiate(dashParticleEffect, transform.position, Quaternion.LookRotation(-_dashDirection));
                _activeDashEffect.Play();
            }

            if (dashSound != null)
            {
                _audioSource.PlayOneShot(dashSound);
            }
        }

        private void StopAndDestroyEffect()
        {
            if (_activeDashEffect != null)
            {
                _activeDashEffect.Stop();
                Destroy(_activeDashEffect.gameObject, _activeDashEffect.main.duration);
            }
        }

        public void OnTransformToBall()
        {
            _isDashing = false;
            _isCooldownActive = false;
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            _rb = GetComponent<Rigidbody>();
            if (_rb == null)
            {
                Debug.LogError("Rigidbody not found on Ball GameObject.");
            }

            if (_audioSource == null)
            {
                _audioSource = gameObject.AddComponent<AudioSource>();
            }
        }

        // Manage ball roll sound (play while moving, stop when stationary)
        private void ManageRollSound()
        {
            if (_isRolling && !_audioSource.isPlaying)
            {
                _audioSource.clip = ballRollSound;
                _audioSource.loop = true;  // Loop the sound while moving
                _audioSource.Play();
            }
            else if (!_isRolling && _audioSource.isPlaying)
            {
                _audioSource.Stop();  // Stop the sound if the ball stops moving
            }
        }
    }
}
