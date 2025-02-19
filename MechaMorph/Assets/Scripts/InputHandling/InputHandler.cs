using UnityEngine;
using UnityEngine.InputSystem;

namespace TrippleTrinity.MechaMorph.InputHandling
{
    public class InputHandler : MonoBehaviour
    {
        public static InputHandler Instance { get; private set; }

        [SerializeField] private InputActionAsset robotInput;
        [SerializeField] private InputActionAsset ballInput;
        [SerializeField] private InputActionAsset combatInput;

        private InputAction _robotMoveAction;
        private InputAction _ballMoveAction;
        private InputAction _jumpAction;
        private InputAction _dashAction;
        private InputAction _activateAbilityAction;
        private InputAction _fireAction;
        private InputAction _reloadAction;

        private Vector2 _moveInput;
        private bool _jumpPressed;
        private bool _dashPressed;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            // Initialize all actions
            _robotMoveAction = robotInput.FindAction("RMove");
            _ballMoveAction = ballInput.FindAction("Move");
            _jumpAction = robotInput.FindAction("Jump");
            _dashAction = ballInput.FindAction("Dash");
            _activateAbilityAction = robotInput.FindAction("ActivateAbility");
            _fireAction = combatInput.FindAction("Fire");
            _reloadAction = combatInput.FindAction("Reloading");

            if (_robotMoveAction == null || _jumpAction == null || _dashAction == null || _activateAbilityAction == null || _fireAction == null || _reloadAction == null)
            {
                Debug.LogError("Some input actions are missing in InputActionAsset!");
            }
        }

        private void OnEnable()
        {
            if (_robotMoveAction != null)
            {
                _robotMoveAction.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
                _robotMoveAction.canceled += _ => _moveInput = Vector2.zero;
                _robotMoveAction.Enable();
            }
            if (_ballMoveAction != null)
            {
                _ballMoveAction.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
                _ballMoveAction.canceled += _ => _moveInput = Vector2.zero;
                _ballMoveAction.Enable();
            }

            if (_jumpAction != null)
            {
                _jumpAction.performed += _ => _jumpPressed = true;
                _jumpAction.Enable();
            }

            if (_dashAction != null)
            {
                _dashAction.performed += _ =>
                {
                    _dashPressed = true;
                    Debug.Log("Dash Button Pressed");
                };
                _dashAction.Enable();
            }

            if (_activateAbilityAction != null)
            {
                _activateAbilityAction.Enable();
            }

            if (_fireAction != null)
            {
                _fireAction.Enable();
            }

            if (_reloadAction != null)
            {
                _reloadAction.Enable();
            }
        }

        private void OnDisable()
        {
            _robotMoveAction?.Disable();
            _jumpAction?.Disable();
            _dashAction?.Disable();
            _activateAbilityAction?.Disable();
            _fireAction?.Disable();
            _reloadAction?.Disable();
        }

        public Vector2 GetMoveInput() => _moveInput;
        public bool IsJumpPressed() => _jumpPressed;
        public bool IsDashPressed()
        {
            Debug.Log("Checking Dash: " + _dashPressed);
            return _dashPressed;
        }

        public bool IsAbilityActivated() => _activateAbilityAction.triggered;
        public bool IsFirePressed() => _fireAction.triggered;
        public bool IsReloadPressed() => _reloadAction.triggered;

        public void ResetJump() => _jumpPressed = false;
        public void ResetDash()
        {
            Debug.Log("Dash Reset!");
            _dashPressed = false;
        }
    }
}
