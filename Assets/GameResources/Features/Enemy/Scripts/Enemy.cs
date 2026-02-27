namespace GameResources.Features.Enemy.Scripts
{
    using UnityEngine;
    using GameResources.Features.Data.Scripts;
    using GameResources.Features.LevelXp.Scripts;

    /// <summary>
    /// Враг
    /// </summary>
    public sealed class Enemy : MonoBehaviour
    {
        /// <summary>
        /// Паблик аксесор айди врага
        /// </summary>
        public string EnemyId => _enemyId;
        
        /// <summary>
        /// Паблик аксесор текущего хп
        /// </summary>
        public int CurrentHealth => _currentHealth;
        
        /// <summary>
        /// Паблик аксесор награды
        /// </summary>
        public int XpReward => _xpReward;
        
        [SerializeField] private string _enemyId = string.Empty;
        [SerializeField] private EnemyRegistryContainer _registry = default;
        [SerializeField] private Collider2D _collider = default;

        private XpPool _xpOrbPool = default;

        private int _currentHealth = default;
        private int _xpReward = default;

        private void Awake() => LoadFromBalance();

        private void OnEnable() => _registry.Register(this, _collider);

        private void OnDisable() => _registry.Unregister(this, _collider);

        /// <summary>
        /// Получение урона
        /// </summary>
        public void TakeDamage(int damage)
        {
            if (damage <= 0)
            {
                return;
            }

            _currentHealth -= damage;

            if (_currentHealth <= 0)
            {
                Die();
            }
        }
        
        /// <summary>
        /// Получить компонента пула
        /// </summary>
        /// <param name="pool"></param>
        public void SetXpPool(XpPool pool) => _xpOrbPool = pool;

        private void LoadFromBalance()
        {
            if (BalanceManager.Balance == null)
            {
                Debug.LogError($"{nameof(Enemy)}: Balance not loaded.");
                return;
            }

            if (!BalanceManager.Balance.TryGetEnemy(_enemyId, out EnemyConfigDto config))
            {
                Debug.LogError($"{nameof(Enemy)}: Enemy config not found for id: {_enemyId}");
                return;
            }

            _currentHealth = config.MaxHealth;
            _xpReward = config.XpReward;
        }

        private void Die()
        {
            SpawnXpOrb();
            Destroy(gameObject);
        }

        private void SpawnXpOrb()
        {
            if (_xpOrbPool == null)
            {
                Debug.LogError($"{nameof(Enemy)}: XpPool not loaded.");
                return;
            }

            XpOrb orb = _xpOrbPool.Get(transform.position, Quaternion.identity);
            if (orb != null)
            {
                orb.SetXpAmount(_xpReward);
            }
        }
    }
}