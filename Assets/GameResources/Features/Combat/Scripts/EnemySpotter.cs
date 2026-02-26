namespace GameResources.Features.Combat.Scripts
{
    using System.Collections.Generic;
    using UnityEngine;
    using GameResources.Features.Enemy.Scripts;

    /// <summary>
    /// Детектор врагов
    /// </summary>
    [RequireComponent(typeof(CircleCollider2D))]
    public sealed class EnemySpotter : MonoBehaviour
    {
        /// <summary>
        /// Список врагов в радиусе
        /// </summary>
        public IReadOnlyList<Enemy> TargetsInRange => _targetsInRange;

        [SerializeField] private CircleCollider2D _rangeCollider = default;
        [SerializeField] private EnemyRegistryContainer _enemyRegistry = default;

        private readonly List<Enemy> _targetsInRange = new List<Enemy>();

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_enemyRegistry.TryGetEnemy(other, out Enemy enemy))
            {
                return;
            }

            if (!_targetsInRange.Contains(enemy))
            {
                _targetsInRange.Add(enemy);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!_enemyRegistry.TryGetEnemy(other, out Enemy enemy))
            {
                return;
            }

            _targetsInRange.Remove(enemy);
        }
        
        /// <summary>
        /// Выставить радиус детекта врагов
        /// </summary>
        public void SetRadius(float radius) => _rangeCollider.radius = radius;

        public void CleanupNullTargets()
        {
            for (int i = _targetsInRange.Count - 1; i >= 0; i--)
            {
                if (_targetsInRange[i] == null)
                {
                    _targetsInRange.RemoveAt(i);
                }
            }
        }
    }
}