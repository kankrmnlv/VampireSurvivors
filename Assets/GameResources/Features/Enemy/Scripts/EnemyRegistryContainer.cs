namespace GameResources.Features.Enemy.Scripts
{
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Контейнер заспавненных врагов
    /// </summary>
    [CreateAssetMenu(fileName = nameof(EnemyRegistryContainer), menuName = "Features/Enemy/ " + nameof(EnemyRegistryContainer))]
    public sealed class EnemyRegistryContainer : ScriptableObject
    {
        /// <summary>
        /// Паблик аксесор врагов
        /// </summary>
        public IReadOnlyList<Enemy> ActiveEnemies => _activeEnemies;

        [SerializeField] private List<Enemy> _activeEnemies = default;
        
        private readonly Dictionary<Collider2D, Enemy> _enemyByCollider = new Dictionary<Collider2D, Enemy>();

        /// <summary>
        /// Очистить список
        /// </summary>
        public void Clear()
        {
            if (_activeEnemies == null)
            {
                _activeEnemies = new List<Enemy>();
                return;
            }

            _activeEnemies.Clear();
            _enemyByCollider.Clear();
        }

        /// <summary>
        /// Добавиться в список
        /// </summary>
        public void Register(Enemy enemy, Collider2D collider)
        {
            if (enemy == null)
            {
                return;
            }

            if (_activeEnemies == null)
            {
                _activeEnemies = new List<Enemy>();
            }

            if (!_activeEnemies.Contains(enemy))
            {
                _activeEnemies.Add(enemy);
            }
            
            _enemyByCollider[collider] = enemy;
        }

        /// <summary>
        /// Удалиться из списка
        /// </summary>
        public void Unregister(Enemy enemy, Collider2D collider)
        {
            if (enemy == null || _activeEnemies == null)
            {
                return;
            }

            _activeEnemies.Remove(enemy);
            _enemyByCollider.Remove(collider);
        }
        
        /// <summary>
        /// Получить компонент через коллайдер
        /// </summary>
        public bool TryGetEnemy(Collider2D collider, out Enemy enemy)
        {
            if (collider == null)
            {
                enemy = default;
                return false;
            }

            return _enemyByCollider.TryGetValue(collider, out enemy);
        }
    }
}