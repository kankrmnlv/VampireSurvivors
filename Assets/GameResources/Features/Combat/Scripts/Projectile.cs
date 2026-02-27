namespace GameResources.Features.Combat.Scripts
{
    using UnityEngine;
    using GameResources.Features.Enemy.Scripts;
    using System.Collections;
    using GameResources.Features.Pool;

    /// <summary>
    /// Снаряд
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public sealed class Projectile : PooledBehaviour<Projectile>
    {
        private const int PROJECTILE_EXPIRE = 5;
        
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
            StopCoroutine(nameof(ProjectileLifeCycle));
            StartCoroutine(ProjectileLifeCycle());
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_enemyRegistry.TryGetEnemy(other, out Enemy enemy))
            {
                return;
            }
            
            enemy.TakeDamage(_damage);
            Release();
        }
        
        private void Release()
        {
            StopCoroutine(nameof(ProjectileLifeCycle));
            _damage = 0;

            _rigidbody2D.linearVelocity = Vector2.zero;
            _rigidbody2D.angularVelocity = 0f;
            ReleaseSelf();
        }
        
        private IEnumerator ProjectileLifeCycle()
        {
            yield return new WaitForSeconds(PROJECTILE_EXPIRE);
            Release();
        }
    }
}