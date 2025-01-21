using System.Collections;
using TrippleTrinity.MechaMorph.Control;
using TrippleTrinity.MechaMorph.Ui_Scripts;
using UnityEngine;

namespace TrippleTrinity.MechaMorph.Ability
{
    
public class DashAbility : MonoBehaviour
{
    [Header("Dash Settings")]
    [SerializeField] private float dashMultiplier = 2f; // Speed multiplier during dash
    [SerializeField] private float dashDuration = 0.5f; // Duration of the dash
    [SerializeField] private float dashCooldown = 2f; // Cooldown between dashes

    [Header("Effects")]
    [SerializeField] private ParticleSystem dashParticleEffect; // Particle effect for the dash
    [SerializeField] private AudioClip dashSound; // Sound effect for the dash
    private AudioSource _audioSource;

    private BallControllerWithDash _ballController;
    private bool _isDashing ;
    private bool _isCooldownActive;

    private void Start()
    {
        _ballController = GetComponent<BallControllerWithDash>();
        if (_ballController == null)
        {
            Debug.LogError("BallController script not found on this GameObject.");
        }

        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && !_isDashing && !_isCooldownActive)
        {
            StartCoroutine(PerformDash());
        }
    }

    private IEnumerator PerformDash()
    {
        _isDashing = true;

        // Temporarily increase movement speed
        float originalSpeed = _ballController.movementSpeed;
        _ballController.movementSpeed *= dashMultiplier;

        // Play particle effect if assigned
        if (dashParticleEffect != null)
        {
            ParticleSystem effect = Instantiate(dashParticleEffect, transform.position, Quaternion.identity);
            effect.Play();
            Destroy(effect.gameObject, effect.main.duration);
        }

        // Play sound effect if assigned
        if (dashSound != null)
        {
            _audioSource.PlayOneShot(dashSound);
        }

        // Notify the cooldown UI to start
        DashCooldownUI.Instance?.StartCooldown(dashCooldown);

        // Wait for dash duration
        yield return new WaitForSeconds(dashDuration);

        // Reset movement speed
        _ballController.movementSpeed = originalSpeed;
        _isDashing = false;

        // Start cooldown
        _isCooldownActive = true;
        yield return new WaitForSeconds(dashCooldown);
        _isCooldownActive = false;
    }
}
}
