using GameResources.Features.Player.Scripts;

namespace GameResources.Features.Enemy.Scripts
{
    using UnityEngine;
    using GameResources.Features.Data.Scripts;

    /// <summary>
    /// Движение врага
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private Enemy _enemy = default;
        [SerializeField] private Rigidbody2D _rigidbody2D = default;
        [SerializeField] private PlayerTransformContainer _playerTransform = default;

        private float _moveSpeed = default;
        private Vector2 _direction = default;

        private void Awake() => LoadSpeed();

        private void FixedUpdate()
        {
            if (_playerTransform.Transform == null)
            {
                return;
            }

            _direction = (_playerTransform.Transform.position - transform.position).normalized;
            _rigidbody2D.MovePosition(_rigidbody2D.position + _direction * _moveSpeed * Time.fixedDeltaTime);
        }

        private void LoadSpeed()
        {
            if (BalanceManager.Balance == null)
            {
                Debug.LogWarning($"{nameof(EnemyMovement)}: Balance is not loaded.");
                return;
            }

            if (!BalanceManager.Balance.TryGetEnemy(_enemy.EnemyId, out EnemyConfigDto config))
            {
                Debug.LogError($"{nameof(EnemyMovement)}: Enemy config not found for id: {_enemy.EnemyId}");
                return;
            }

            _moveSpeed = config.MoveSpeed;
        }
    }
}