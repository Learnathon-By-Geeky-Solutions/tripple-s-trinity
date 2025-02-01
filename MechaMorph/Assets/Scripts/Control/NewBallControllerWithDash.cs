using System.Collections;
using TrippleTrinity.MechaMorph.Ui;
using UnityEngine;
using UnityEngine.InputSystem;

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

        [Header("Input")]
        [SerializeField] private InputActionAsset playerInput;

        private Rigidbody _rb;
        private AudioSource _audioSource;
        private Vector2 _moveInput;

        private bool _isDashing;
        private bool _isCooldownActive;
        private InputAction _moveAction;
        private InputAction _dashAction;

        void Awake()
        {
            InitializeComponents();
        
            _moveAction = playerInput.FindAction("Move");
            _dashAction = playerInput.FindAction("Dash");
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

            if (_dashAction != null)
            {
                _dashAction.performed += _ => PerformDash(); 
                _dashAction.Enable();
            }
            else
            {
                Debug.LogError("Dash action not found in InputActionAsset!");
            }
        }

        private void MovePerformed(InputAction.CallbackContext ctx)
        {
            _moveInput = ctx.ReadValue<Vector2>(); // ✅ ctx ব্যবহার করা হয়েছে, Warning দূর হবে
        }

        private void MoveCanceled(InputAction.CallbackContext ctx)
        {
            _moveInput = Vector2.zero;
        }


        void OnDisable()
        {
            if (_moveAction != null)
            {
                _moveAction.Disable();
            }

            if (_dashAction != null)
            {
                _dashAction.Disable();
            }
        }

        void FixedUpdate()
        {
            Vector3 movement = new Vector3(_moveInput.x, 0, _moveInput.y);
            _rb.AddForce(movement * movementSpeed);
        }

        private void PerformDash()
        {
            if (!_isDashing && !_isCooldownActive)
            {
                StartCoroutine(DashCoroutine());
            }
        }

        private IEnumerator DashCoroutine()
        {
            _isDashing = true;

            float originalSpeed = movementSpeed;
            movementSpeed *= dashMultiplier;

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

            DashCooldownUI.Instance?.StartCooldown(dashCooldown);

            yield return new WaitForSeconds(dashDuration);

            movementSpeed = originalSpeed;
            _isDashing = false;

            _isCooldownActive = true;
            yield return new WaitForSeconds(dashCooldown);
            _isCooldownActive = false;
        }

        public void OnTransformToBall()
        {
            // Reset dash-related variables
            _isDashing = false;
            _isCooldownActive = false;

            // Re-initialize components
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