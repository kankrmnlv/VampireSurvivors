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
        }

        /// <summary>
        /// Добавиться в список
        /// </summary>
        public void Register(Enemy enemy)
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
        }

        /// <summary>
        /// Удалиться из списка
        /// </summary>
        public void Unregister(Enemy enemy)
        {
            if (enemy == null || _activeEnemies == null)
            {
                return;
            }

            _activeEnemies.Remove(enemy);
        }
    }
}