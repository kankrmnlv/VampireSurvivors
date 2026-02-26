namespace GameResources.Features.Combat.Scripts
{
    using System.Collections.Generic;
    using System.Collections;
    using UnityEngine;
    using GameResources.Features.Enemy.Scripts;
    using GameResources.Features.Data.Scripts;

    /// <summary>
    /// Автоатака по врагам
    /// </summary>
    public sealed class AutoAttackProjectileCoroutine : MonoBehaviour
    {
        private const string PROJECTILE_SKILL_ID = "projectile";

        [SerializeField] private EnemySpotter _spotter = default;
        [SerializeField] private Transform _shootPoint = default;
        [SerializeField] private ProjectilePool _projectilePool = default;

        private int _damage = default;
        private float _cooldown = default;
        private float _projectileSpeed = default;
        private int _count = default;

        private Coroutine _attackRoutine = default;
        private Enemy _targetEnemy = default;

        private void Start()
        {
            LoadFromBalance();
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
            WaitForSeconds wait = new WaitForSeconds(_cooldown);

            while (true)
            {
                _spotter.CleanupNullTargets();
                _targetEnemy = FindNearestTarget(_spotter.TargetsInRange);
                if (_targetEnemy != null)
                {
                    Shoot(_targetEnemy);
                }
                yield return wait;
                
                wait = new WaitForSeconds(_cooldown);
            }
        }

        private void LoadFromBalance()
        {
            if (BalanceManager.Balance == null)
            {
                Debug.LogError($"{nameof(AutoAttackProjectileCoroutine)}: Balance not loaded.");
                return;
            }
            
            if (!BalanceManager.Balance.TryGetSkill(PROJECTILE_SKILL_ID, out SkillConfigDto skill))
            {
                Debug.LogError($"{nameof(AutoAttackProjectileCoroutine)}: Skill '{PROJECTILE_SKILL_ID}' not found.");
                return;
            }

            _spotter.SetRadius(BalanceManager.Balance.Player.AutoAttackRange);
            _damage = skill.Damage;
            _cooldown = Mathf.Max(0.05f, skill.Cooldown);
            _projectileSpeed = skill.ProjectileSpeed;
            _count = Mathf.Max(1, skill.ProjectilesCount);
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

        private void Shoot(Enemy target)
        {
            Vector2 dir = (target.transform.position - _shootPoint.position).normalized;

            if (dir == Vector2.zero)
            {
                dir = Vector2.up;
            }

            for (int i = 0; i < _count; i++)
            {
                Projectile projectile = _projectilePool.Get(_shootPoint.position, Quaternion.identity);
                projectile.Initialize(dir, _projectileSpeed, _damage);
            }
        }
    }
}