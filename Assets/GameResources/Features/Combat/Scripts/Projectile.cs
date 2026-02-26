namespace GameResources.Features.Combat.Scripts
{
    using UnityEngine;
    using GameResources.Features.Enemy.Scripts;
    using System.Collections;
    using UnityEngine.Pool;

    /// <summary>
    /// Снаряд
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public sealed class Projectile : MonoBehaviour
    {
        private const int PROJECTILE_EXPIRE = 5;
        
        [SerializeField] private Rigidbody2D _rigidbody2D = default;
        [SerializeField] private EnemyRegistryContainer _enemyRegistry = default;

        private int _damage = default;
        private IObjectPool<Projectile> _ownerPool = default;
        
        /// <summary>
        /// Запулить
        /// </summary>
        /// <param name="pool"></param>
        public void SetPool(IObjectPool<Projectile> pool) => _ownerPool = pool;
        
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

            if (_ownerPool != null)
            {
                _ownerPool.Release(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        private IEnumerator ProjectileLifeCycle()
        {
            yield return new WaitForSeconds(PROJECTILE_EXPIRE);
            Release();
        }
    }
}