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

            InitializeInputActions();
        }

        private void OnEnable()
        {
            BindMoveInput(_robotMoveAction);
            BindMoveInput(_ballMoveAction);
            BindJumpInput();
            BindDashInput();

            EnableAction(_activateAbilityAction);
            EnableAction(_fireAction);
            EnableAction(_reloadAction);
        }

        private void OnDisable()
        {
            _robotMoveAction?.Disable();
            _ballMoveAction?.Disable();
            _jumpAction?.Disable();
            _dashAction?.Disable();
            _activateAbilityAction?.Disable();
            _fireAction?.Disable();
            _reloadAction?.Disable();
        }

        private void InitializeInputActions()
        {
            _robotMoveAction = robotInput.FindAction("RMove");
            _ballMoveAction = ballInput.FindAction("Move");
            _jumpAction = robotInput.FindAction("Jump");
            _dashAction = ballInput.FindAction("Dash");
            _activateAbilityAction = robotInput.FindAction("ActivateAbility");
            _fireAction = combatInput.FindAction("Fire");
            _reloadAction = combatInput.FindAction("Reloading");

            if (_robotMoveAction == null || _jumpAction == null || _dashAction == null || 
                _activateAbilityAction == null || _fireAction == null || _reloadAction == null)
            {
                Debug.LogError("Some input actions are missing in InputActionAsset!");
            }
        }

        private void BindMoveInput(InputAction moveAction)
        {
            if (moveAction == null) return;

            moveAction.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
            moveAction.canceled += _ => _moveInput = Vector2.zero;
            moveAction.Enable();
        }

        private void BindJumpInput()
        {
            if (_jumpAction == null) return;

            _jumpAction.performed += _ => _jumpPressed = true;
            _jumpAction.Enable();
        }

        private void BindDashInput()
        {
            if (_dashAction == null) return;

            _dashAction.performed += _ =>
            {
                _dashPressed = true;
                Debug.Log("Dash Button Pressed");
            };
            _dashAction.Enable();
        }

        private void EnableAction(InputAction action)
        {
            if (action != null)
            {
                action.Enable();
            }
        }

        // Public Getters
        public Vector2 GetMoveInput() => _moveInput;
        public bool IsJumpPressed() => _jumpPressed;
        public bool IsDashPressed() => _dashPressed;
        public bool IsAbilityActivated() => _activateAbilityAction?.triggered ?? false;
        public bool IsFirePressed() => _fireAction?.triggered ?? false;
        public bool IsReloadPressed() => _reloadAction?.triggered ?? false;

        public void ResetJump() => _jumpPressed = false;
        public void ResetDash()
        {
            Debug.Log("Dash Reset!");
            _dashPressed = false;
        }
    }
}
