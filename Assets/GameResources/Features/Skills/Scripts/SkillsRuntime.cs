namespace GameResources.Features.Skills.Scripts
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using GameResources.Features.Data.Scripts;

    public enum UpgradeStatType
    {
        Unknown = 0,
        Damage = 1,
        Cooldown = 2,
        ProjectileSpeed = 3,
        ProjectilesCount = 4,
        TickInterval = 5,
        Radius = 6
    }

    public enum UpgradeOpType
    {
        Unknown = 0,
        Add = 1,
        Mul = 2
    }

    /// <summary>
    /// Менеджер апрейдов умений
    /// </summary>
    public sealed class SkillsRuntime : MonoBehaviour
    {
        private const float MIN_TICK = 0.05f;

        private readonly Dictionary<string, SkillRuntimeState> _byId = new Dictionary<string, SkillRuntimeState>(StringComparer.Ordinal);

        public SkillRuntimeState Get(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }

            _byId.TryGetValue(id, out SkillRuntimeState state);
            return state;
        }

        private void Start() => InitializeFromBalance();

        private void InitializeFromBalance()
        {
            if (BalanceManager.Balance == null)
            {
                Debug.LogError($"[{nameof(SkillsRuntime)}] Balance not loaded.");
                return;
            }
            
            foreach (KeyValuePair<string, SkillConfigDto> pair in BalanceManager.Balance.SkillsById)
            {
                SkillRuntimeState state = new SkillRuntimeState(pair.Key);

                state.Damage = pair.Value.Damage;
                state.Cooldown = pair.Value.Cooldown;
                state.ProjectileSpeed = pair.Value.ProjectileSpeed;
                state.ProjectilesCount = pair.Value.ProjectilesCount;

                state.TickInterval = pair.Value.TickInterval;
                state.Radius = pair.Value.Radius;

                _byId[pair.Key] = state;
            }

            Debug.Log($"[{nameof(SkillsRuntime)}] Initialized skills: {_byId.Count}");
        }

        public bool TryApplyUpgrade(UpgradeConfigDto upgrade)
        {
            if (upgrade == null)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(upgrade.TargetSkillId))
            {
                Debug.LogWarning($"[{nameof(SkillsRuntime)}] Upgrade '{upgrade.Id}' has empty targetSkillId.");
                return false;
            }

            SkillRuntimeState state = Get(upgrade.TargetSkillId);
            if (state == null)
            {
                Debug.LogWarning($"[{nameof(SkillsRuntime)}] Target skill not found: {upgrade.TargetSkillId} (upgrade {upgrade.Id})");
                return false;
            }

            if (!Enum.TryParse(upgrade.Stat, ignoreCase: true, out UpgradeStatType stat) || stat == UpgradeStatType.Unknown)
            {
                Debug.LogWarning($"[{nameof(SkillsRuntime)}] Unknown stat '{upgrade.Stat}' (upgrade {upgrade.Id})");
                return false;
            }

            if (!Enum.TryParse(upgrade.Op, ignoreCase: true, out UpgradeOpType op) || op == UpgradeOpType.Unknown)
            {
                Debug.LogWarning($"[{nameof(SkillsRuntime)}] Unknown op '{upgrade.Op}' (upgrade {upgrade.Id})");
                return false;
            }

            Apply(state, stat, op, upgrade.Value);

            Debug.Log($"[{nameof(SkillsRuntime)}] Applied upgrade '{upgrade.Id}' to '{state.Id}': {stat} {op} {upgrade.Value}");
            return true;
        }

        private void Apply(SkillRuntimeState state, UpgradeStatType stat, UpgradeOpType op, float value)
        {
            switch (stat)
            {
                case UpgradeStatType.Damage:
                    state.Damage = ApplyInt(state.Damage, op, value);
                    return;

                case UpgradeStatType.Cooldown:
                    state.Cooldown = ApplyFloat(state.Cooldown, op, value);
                    return;

                case UpgradeStatType.ProjectileSpeed:
                    state.ProjectileSpeed = ApplyFloat(state.ProjectileSpeed, op, value);
                    return;

                case UpgradeStatType.ProjectilesCount:
                    state.ProjectilesCount = ApplyInt(state.ProjectilesCount, op, value);
                    return;

                case UpgradeStatType.TickInterval:
                    state.TickInterval = ApplyFloat(state.TickInterval, op, value);
                    return;

                case UpgradeStatType.Radius:
                    state.Radius = ApplyFloat(state.Radius, op, value);
                    return;
            }
        }

        private int ApplyInt(int current, UpgradeOpType op, float value)
        {
            if (op == UpgradeOpType.Add)
            {
                int add = Mathf.RoundToInt(value);
                return current + add;
            }

            if (op == UpgradeOpType.Mul)
            {
                return Mathf.RoundToInt(current * value);
            }

            return current;
        }

        private float ApplyFloat(float current, UpgradeOpType op, float value)
        {
            if (op == UpgradeOpType.Add)
            {
                return current + value;
            }

            if (op == UpgradeOpType.Mul)
            {
                return current * value;
            }

            return current;
        }
    }
}