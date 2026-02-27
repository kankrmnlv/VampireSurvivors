namespace GameResources.Features.Combat.Scripts
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using GameResources.Features.Enemy.Scripts;
    using GameResources.Features.Skills.Scripts;

    /// <summary>
    /// Урон аурой по всем врагам в радиусе (свой коллайдер)
    /// </summary>
    [RequireComponent(typeof(CircleCollider2D))]
    public sealed class AuraDamageCoroutine : MonoBehaviour
    {
        private const string AURA_SKILL_ID = "aura";
        private const float MIN_TICK = 0.05f;
        
        [SerializeField] private SkillsRuntime _skillsRuntime = default;
        [SerializeField] private EnemyRegistryContainer _enemyRegistry = default;
        
        [SerializeField] private CircleCollider2D _auraCollider = default;

        private readonly List<Enemy> _targetsInRange = new List<Enemy>();
        private Coroutine _routine = default;

        private void OnEnable() => _routine = StartCoroutine(Loop());

        private void OnDisable()
        {
            if (_routine != null)
            {
                StopCoroutine(_routine);
                _routine = null;
            }

            _targetsInRange.Clear();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_enemyRegistry.TryGetEnemy(other, out Enemy enemy))
            {
                return;
            }

            if (enemy != null && !_targetsInRange.Contains(enemy))
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

        private IEnumerator Loop()
        {
            while (true)
            {
                SkillRuntimeState state = _skillsRuntime != null ? _skillsRuntime.Get(AURA_SKILL_ID) : null;
                if (state == null)
                {
                    yield return new WaitForSeconds(MIN_TICK);
                    continue;
                }
                
                if (_auraCollider != null)
                {
                    _auraCollider.radius = Mathf.Max(0f, state.Radius);
                }

                CleanupNullTargets();
                
                if (state.Damage > 0 && _targetsInRange.Count > 0)
                {
                    DealDamageToAll(state.Damage);
                }
                
                yield return new WaitForSeconds(state.TickInterval);
            }
        }

        private void DealDamageToAll(int damage)
        {
            for (int i = 0; i < _targetsInRange.Count; i++)
            {
                if (_targetsInRange[i] == null)
                {
                    continue;
                }
                
                _targetsInRange[i].TakeDamage(damage);
            }
        }

        private void CleanupNullTargets()
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