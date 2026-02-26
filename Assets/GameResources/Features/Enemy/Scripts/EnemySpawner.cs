namespace GameResources.Features.Enemy.Scripts
{
    using System.Collections;
    using UnityEngine;
    using GameResources.Features.Player.Scripts;

    /// <summary>
    /// Спавнер врагов
    /// </summary>
    public sealed class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private PlayerTransformContainer _playerTransform = default;

        [SerializeField] private Enemy _normalEnemyPrefab = default;
        [SerializeField] private Enemy _fastEnemyPrefab = default;

        [SerializeField] private float _spawnInterval = 2f;
        [SerializeField] private float _spawnRadius = 10f;

        private void Start() => StartCoroutine(SpawnRoutine());

        private IEnumerator SpawnRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(_spawnInterval);

                Vector2 spawnPos = (Vector2)_playerTransform.Transform.position + Random.insideUnitCircle.normalized * _spawnRadius;
                Enemy prefab = Random.value > 0.5f ? _normalEnemyPrefab : _fastEnemyPrefab;

                Instantiate(prefab, spawnPos, Quaternion.identity);
            }
        }
    }
}