namespace GameResources.Features.Player.Scripts
{
    using UnityEngine;
    using UnityEngine.InputSystem;
    using GameResources.Features.Data.Scripts;

    /// <summary>
    /// Передвижение игрока
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private PlayerTransformContainer _transformContainer = default;
        [SerializeField] private Rigidbody2D _rigidbody2D = default;
        [SerializeField] private InputActionReference _moveAction = default;

        private float _moveSpeed = default;
        private Vector2 _moveInput = default;

        private void Start()
        {
            if (BalanceManager.Balance != null)
            {
                _moveSpeed = BalanceManager.Balance.Player.MoveSpeed;
                Debug.Log(_moveSpeed);
            }
            else
            {
                Debug.LogWarning($"{nameof(PlayerMovement)}: Balance is not loaded. Move speed will be 0.");
                _moveSpeed = 0f;
            }
        }

        private void OnEnable()
        {
            if (_moveAction == null || _moveAction.action == null)
            {
                Debug.LogError($"{nameof(PlayerMovement)}: Move action is not assigned.");
                return;
            }

            _moveAction.action.performed += OnMovePerformed;
            _moveAction.action.canceled += OnMoveCanceled;
            _moveAction.action.Enable();
            
            _transformContainer.Set(transform);
        }

        private void OnDisable()
        {
            if (_moveAction != null && _moveAction.action != null)
            {
                _moveAction.action.performed -= OnMovePerformed;
                _moveAction.action.canceled -= OnMoveCanceled;
                _moveAction.action.Disable();
            }

            _moveInput = Vector2.zero;
            
            _transformContainer.Clear();
        }

        private void FixedUpdate()
        {
            if (_moveInput == Vector2.zero)
            {
                return;
            }
            
            _rigidbody2D.MovePosition(_rigidbody2D.position + _moveInput.normalized * _moveSpeed * Time.fixedDeltaTime);
        }

        private void OnMovePerformed(InputAction.CallbackContext context) => _moveInput = context.ReadValue<Vector2>();

        private void OnMoveCanceled(InputAction.CallbackContext context) => _moveInput = Vector2.zero;
    }
}