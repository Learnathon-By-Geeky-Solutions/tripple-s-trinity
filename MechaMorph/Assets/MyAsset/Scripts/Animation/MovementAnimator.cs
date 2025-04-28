using TrippleTrinity.MechaMorph.InputHandling;
using UnityEngine;

namespace TrippleTrinity.MechaMorph.MyAsset.Scripts.Animation
{
    public class MoveAnimation : MonoBehaviour
    {
        private Animator _animator;

        private void Awake()
        {
            // If Animator is on the child object, reference it through GetComponentInChildren
            _animator = GetComponentInChildren<Animator>();  // This will find the Animator on the child
        }

        private void Update()
        {
            HandleMovementAnimation();
        }

        private void HandleMovementAnimation()
        {
            Vector2 moveInput = InputHandler.Instance.GetMoveInput();
            Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y).normalized;

            // Trigger walking animation when moving
            if (moveDirection.sqrMagnitude > 0.01f)
            {
                _animator.SetBool("IsWalking", true);
            }
            else
            {
                _animator.SetBool("IsWalking", false);
            }
        }
    }
}