using System.Collections;
using UnityEngine;
using TrippleTrinity.MechaMorph.InputHandling;

namespace TrippleTrinity.MechaMorph.Control
{
    public class NewBallControllerWithDash : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float movementSpeed = 10f;

        [Header("Dash Settings")]
        [SerializeField] private float dashMultiplier = 2f;
        [SerializeField] private float dashDuration = 0.2f;
        [SerializeField] private float dashCooldown = 1f;

        [Header("Effects")]
        [SerializeField] private ParticleSystem dashParticleEffect;
        [SerializeField] private AudioClip dashSound;

        private Rigidbody _rb;
        private AudioSource _audioSource;
        private Vector2 _moveInput;

        private bool _isDashing;
        private bool _isCooldownActive;

        private void Awake()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            _rb = GetComponent<Rigidbody>();
            if (_rb == null)
            {
                Debug.LogError("Rigidbody not found on Ball GameObject.");
            }

            _audioSource = GetComponent<AudioSource>();
            if (_audioSource == null)
            {
                _audioSource = gameObject.AddComponent<AudioSource>();
            }
        }

        private void Update()
        {
            // Get movement input from InputHandler
            _moveInput = InputHandler.Instance.GetMoveInput();

            // Check if dash is pressed and available
            if (InputHandler.Instance.IsDashPressed() && !_isDashing && !_isCooldownActive)
            {
                Debug.Log("Dash Started");
                StartCoroutine(DashCoroutine());
                InputHandler.Instance.ResetDash();  // Reset the dash input after consuming it
            }
        }

        private IEnumerator DashCoroutine()
        {
            _isDashing = true;

            // Get dash direction based on movement input
            Vector3 dashDirection = new Vector3(_moveInput.x, 0, _moveInput.y).normalized * dashMultiplier;
            if (dashDirection == Vector3.zero) // If no input, dash in current velocity direction
            {
                dashDirection = _rb.velocity.normalized * dashMultiplier;
            }
            
            _rb.velocity = dashDirection;

            // Dash effects
            if (dashParticleEffect != null)
            {
                ParticleSystem effect = Instantiate(dashParticleEffect, transform.position, Quaternion.identity);
                effect.Play();
                Destroy(effect.gameObject, effect.main.duration);
            }

            if (dashSound != null)
            {
                _audioSource.PlayOneShot(dashSound);
            }

            yield return new WaitForSeconds(dashDuration);

            _rb.velocity /= dashMultiplier; // Reduce velocity after dash

            _isDashing = false;
            StartCoroutine(DashCooldown());
        }

        private IEnumerator DashCooldown()
        {
            _isCooldownActive = true;
            yield return new WaitForSeconds(dashCooldown);
            _isCooldownActive = false;
        }

        private void FixedUpdate()
        {
            if (!_isDashing)
            {
                Vector3 movement = new Vector3(_moveInput.x, 0, _moveInput.y);
                _rb.AddForce(movement * movementSpeed);
            }
        }
    }
}
