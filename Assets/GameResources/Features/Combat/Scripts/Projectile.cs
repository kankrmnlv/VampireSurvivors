namespace GameResources.Features.Combat.Scripts
{
    using UnityEngine;
    using GameResources.Features.Enemy.Scripts;
    using System.Collections;

    /// <summary>
    /// Снаряд
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public sealed class Projectile : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D = default;
        [SerializeField] private EnemyRegistryContainer _enemyRegistry = default;

        private int _damage = default;
        
        /// <summary>
        /// Инициализация
        /// </summary>
        public void Initialize(Vector2 direction, float speed, int damage)
        {
            _damage = damage;
            _rigidbody2D.linearVelocity = direction.normalized * speed;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_enemyRegistry.TryGetEnemy(other, out Enemy enemy))
            {
                return;
            }
            
            enemy.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}