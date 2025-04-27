using UnityEngine;
using TrippleTrinity.MechaMorph.InputHandling;

namespace TrippleTrinity.MechaMorph.Animation
{
    public class JumpAnimation : MonoBehaviour
    {
        private Animator _animator;

        private void Awake()
        {
            // If Animator is on the child object, reference it through GetComponentInChildren
            _animator = GetComponentInChildren<Animator>();  // This will find the Animator on the child
        }

        private void Update()
        {
            HandleJumpAnimation();
        }

        private void HandleJumpAnimation()
        {
            if (InputHandler.Instance.IsJumpPressed())
            {
                _animator.SetTrigger("Jump");  // Trigger the jump animation
                InputHandler.Instance.ResetJump();  // Reset the jump input
            }
        }
    }
}