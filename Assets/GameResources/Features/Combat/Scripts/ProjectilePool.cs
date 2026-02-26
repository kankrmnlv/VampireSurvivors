namespace GameResources.Features.Combat.Scripts
{
    using UnityEngine;
    using UnityEngine.Pool;

    /// <summary>
    /// Пул снарядов
    /// </summary>
    public sealed class ProjectilePool : MonoBehaviour
    {
        [SerializeField] private Projectile _projectilePrefab = default;
        [SerializeField] private int _defaultCapacity = default;
        [SerializeField] private int _maxSize = default;

        private ObjectPool<Projectile> _pool = default;

        private void Awake() => _pool = new ObjectPool<Projectile>(CreateProjectile, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject, collectionCheck: false, defaultCapacity: _defaultCapacity, maxSize: _maxSize);

        /// <summary>
        /// Вытащит
        /// </summary>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <returns></returns>
        public Projectile Get(Vector3 position, Quaternion rotation)
        {
            Projectile projectile = _pool.Get();

            Transform projectileTransform = projectile.transform;
            projectileTransform.SetPositionAndRotation(position, rotation);

            projectile.SetPool(_pool);

            return projectile;
        }

        private Projectile CreateProjectile()
        {
            Projectile instance = Instantiate(_projectilePrefab, transform);
            return instance;
        }

        private static void OnGetFromPool(Projectile projectile)
        {
            projectile.gameObject.SetActive(true);
        }

        private static void OnReleaseToPool(Projectile projectile)
        {
            projectile.gameObject.SetActive(false);
        }

        private static void OnDestroyPooledObject(Projectile projectile)
        {
            Destroy(projectile.gameObject);
        }
    }
}