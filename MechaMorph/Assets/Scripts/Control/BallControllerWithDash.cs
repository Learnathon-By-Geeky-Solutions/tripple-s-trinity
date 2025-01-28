using System.Collections;
using TrippleTrinity.MechaMorph.Ui;
using UnityEngine;

namespace TrippleTrinity.MechaMorph.Control
{
public class BallControllerWithDash : MonoBehaviour
{
    [Header("Movement Settings")] [SerializeField]
    private float movementSpeed = 10f;

    [Header("Dash Settings")]
    [SerializeField] private float dashMultiplier = 2f;
    [SerializeField] private float dashDuration = 0.5f;
    [SerializeField] private float dashCooldown = 2f;

    [Header("Effects")]
    [SerializeField] private ParticleSystem dashParticleEffect;
    [SerializeField] private AudioClip dashSound;

    private Rigidbody _rb;
    private AudioSource _audioSource;

    private float _cachedHorizontal;
    private float _cachedVertical;

    private bool _isDashing;
    private bool _isCooldownActive;

    void Start()
    {
        InitializeComponents();
    }

    void Update()
    {
        _cachedHorizontal = Input.GetAxis("Horizontal");
        _cachedVertical = Input.GetAxis("Vertical");

        if (Input.GetMouseButtonDown(1) && !_isDashing && !_isCooldownActive)
        {
            StartCoroutine(PerformDash());
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(_cachedHorizontal, 0, _cachedVertical);
        _rb.AddForce(movement * movementSpeed);
    }

    private IEnumerator PerformDash()
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