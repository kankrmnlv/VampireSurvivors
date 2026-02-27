namespace GameResources.Features.Combat.Scripts
{
    using System.Collections.Generic;
    using System.Collections;
    using UnityEngine;
    using GameResources.Features.Enemy.Scripts;
    using GameResources.Features.Data.Scripts;
    using GameResources.Features.Skills.Scripts;

    /// <summary>
    /// Автоатака по врагам
    /// </summary>
    public sealed class AutoAttackProjectileCoroutine : MonoBehaviour
    {
        private const string PROJECTILE_SKILL_ID = "projectile";
        private const float MIN_COOLDOWN = 0.05f;

        [SerializeField] private EnemySpotter _spotter = default;
        [SerializeField] private Transform _shootPoint = default;
        [SerializeField] private ProjectilePool _projectilePool = default;

        [SerializeField] private SkillsRuntime _skillsRuntime = default;

        private Coroutine _attackRoutine = default;

        private void Start()
        {
            SetupFromBalance();
            _attackRoutine = StartCoroutine(AttackLoop());
        }

        private void OnDisable()
        {
            if (_attackRoutine != null)
            {
                StopCoroutine(_attackRoutine);
                _attackRoutine = null;
            }
        }

        private IEnumerator AttackLoop()
        {
            while (true)
            {
                SkillRuntimeState state = _skillsRuntime.Get(PROJECTILE_SKILL_ID);
                if (state == null)
                {
                    Debug.LogWarning($"{nameof(AutoAttackProjectileCoroutine)}: Runtime state for '{PROJECTILE_SKILL_ID}' not found.");
                    yield return new WaitForSeconds(MIN_COOLDOWN);
                    continue;
                }

                _spotter.CleanupNullTargets();

                Enemy targetEnemy = FindNearestTarget(_spotter.TargetsInRange);
                if (targetEnemy != null)
                {
                    Shoot(targetEnemy, state);
                }
                
                yield return new WaitForSeconds(state.Cooldown);
            }
        }

        private void SetupFromBalance()
        {
            if (BalanceManager.Balance == null)
            {
                Debug.LogError($"{nameof(AutoAttackProjectileCoroutine)}: Balance not loaded.");
                return;
            }
            
            _spotter.SetRadius(BalanceManager.Balance.Player.AutoAttackRange);
        }

        private Enemy FindNearestTarget(IReadOnlyList<Enemy> targets)
        {
            if (targets == null || targets.Count == 0)
            {
                return null;
            }

            float bestSqr = float.MaxValue;
            Enemy best = null;
            
            for (int i = 0; i < targets.Count; i++)
            {
                if (targets[i] == null)
                {
                    continue;
                }

                Vector2 delta = targets[i].transform.position - transform.position;

                if (delta.sqrMagnitude < bestSqr)
                {
                    bestSqr = delta.sqrMagnitude;
                    best = targets[i];
                }
            }

            return best;
        }

        private void Shoot(Enemy target, SkillRuntimeState state)
        {
            Vector2 dir = (target.transform.position - _shootPoint.position).normalized;
            if (dir == Vector2.zero)
            {
                dir = Vector2.up;
            }

            int count = Mathf.Max(1, state.ProjectilesCount);

            for (int i = 0; i < count; i++)
            {
                Projectile projectile = _projectilePool.Get(_shootPoint.position, Quaternion.identity);
                projectile.Initialize(dir, state.ProjectileSpeed, state.Damage);
            }
        }
    }
}