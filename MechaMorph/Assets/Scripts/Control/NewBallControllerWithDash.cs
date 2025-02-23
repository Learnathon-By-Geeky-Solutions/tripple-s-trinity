using System.Collections;
using TrippleTrinity.MechaMorph.InputHandling;
using TrippleTrinity.MechaMorph.Ui;
using UnityEngine;

namespace TrippleTrinity.MechaMorph.Control
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

        private Rigidbody _rb;
        private AudioSource _audioSource;
        private bool _isDashing;
        private bool _isCooldownActive;
        private ParticleSystem _activeDashEffect;
        private Vector3 _dashDirection; // Store dash direction

        void Awake()
        {
            InitializeComponents();
        }

        void FixedUpdate()
        {
            Vector2 moveInput = InputHandler.Instance.GetMoveInput();
            Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y);
            _rb.AddForce(movement * movementSpeed);

            if (InputHandler.Instance.IsDashPressed() && !_isDashing && !_isCooldownActive)
            {
                _dashDirection = movement.normalized; // Store direction before dashing
                PerformDash();
                InputHandler.Instance.ResetDash();
            }

            // Keep effect following player
            if (_activeDashEffect != null)
            {
                _activeDashEffect.transform.position = transform.position; // Follow player position
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

            // Spawn and orient the effect correctly
            if (dashParticleEffect != null && _dashDirection != Vector3.zero)
            {
                _activeDashEffect = Instantiate(dashParticleEffect, transform.position, Quaternion.LookRotation(-_dashDirection)); // Flipped direction
                _activeDashEffect.Play();
            }

            if (dashSound != null)
            {
                _audioSource.PlayOneShot(dashSound);
            }

            DashCooldownUI.Instance?.StartCooldown(dashCooldown);

            yield return new WaitForSeconds(dashDuration);

            movementSpeed = originalSpeed;
            _isDashing = false;

            // Stop and destroy the effect after dash
            if (_activeDashEffect != null)
            {
                _activeDashEffect.Stop();
                Destroy(_activeDashEffect.gameObject, _activeDashEffect.main.duration);
            }

            _isCooldownActive = true;
            yield return new WaitForSeconds(dashCooldown);
            _isCooldownActive = false;
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
    }
}
