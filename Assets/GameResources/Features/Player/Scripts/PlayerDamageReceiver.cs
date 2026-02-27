namespace GameResources.Features.Player.Scripts
{
    using UnityEngine;
    using GameResources.Features.Enemy.Scripts;

    /// <summary>
    /// Получить урон от врага
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public sealed class PlayerDamageReceiver : MonoBehaviour
    {
        private const float HIT_CD = 0.5f;
        
        [SerializeField] private EnemyRegistryContainer _enemyRegistry = default;
        [SerializeField] private PlayerHealth _playerHealth = default;
        
        private float _nextHitTime = default;

        private void OnTriggerStay2D(Collider2D other)
        {
            if (Time.time < _nextHitTime)
            {
                return;
            }

            if (_playerHealth == null || _playerHealth == null || _playerHealth.IsDead)
            {
                return;
            }

            if (!_enemyRegistry.TryGetEnemy(other, out Enemy enemy) || enemy == null)
            {
                return;
            }

            _playerHealth.TakeDamage(enemy.ContactDamage);
            _nextHitTime = Time.time + HIT_CD;
        }
    }
}