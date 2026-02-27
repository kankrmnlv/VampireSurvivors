namespace GameResources.Features.Data.Scripts
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Репозиторий баланса
    /// </summary>
    public sealed class BalanceRepository
    {
        /// <summary>
        /// Конструктор баланса
        /// </summary>
        public BalanceRepository(BalanceConfigDto dto)
        {
            if (dto == null)
            {
                Debug.LogError($"{nameof(BalanceRepository)}: DTO invalid");
            }

            _dto = dto;

            _enemiesById = new Dictionary<string, EnemyConfigDto>(StringComparer.Ordinal);
            _skillsById = new Dictionary<string, SkillConfigDto>(StringComparer.Ordinal);
            _upgradesById = new Dictionary<string, UpgradeConfigDto>(StringComparer.Ordinal);

            BuildIndexes(dto);
        }

        /// <summary>
        /// Паблик аксесор даты игрока
        /// </summary>
        public PlayerConfigDto Player => _dto.Player;
        
        /// <summary>
        /// Паблик аксесор даты уровня
        /// </summary>
        public LevelingConfigDto Leveling => _dto.Leveling;
        
        /// <summary>
        /// Паблик аксесор врагов
        /// </summary>
        public IReadOnlyDictionary<string, EnemyConfigDto> EnemiesById => _enemiesById;
        
        /// <summary>
        /// Паблик аксесор навыков
        /// </summary>
        public IReadOnlyDictionary<string, SkillConfigDto> SkillsById => _skillsById;
        
        /// <summary>
        /// Паблик аксесор апгрейдов
        /// </summary>
        public IReadOnlyDictionary<string, UpgradeConfigDto> UpgradesById => _upgradesById;

        private BalanceConfigDto _dto = default;

        private readonly Dictionary<string, EnemyConfigDto> _enemiesById = default;
        private readonly Dictionary<string, SkillConfigDto> _skillsById = default;
        private readonly Dictionary<string, UpgradeConfigDto> _upgradesById = default;

        /// <summary>
        /// Получить врагов по айди
        /// </summary>
        public bool TryGetEnemy(string id, out EnemyConfigDto enemy)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                enemy = default;
                Debug.LogError("Enemy Id invalid");
                return false;
            }

            return _enemiesById.TryGetValue(id, out enemy);
        }

        /// <summary>
        /// Получить навыки по айди
        /// </summary>
        public bool TryGetSkill(string id, out SkillConfigDto skill)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                skill = default;
                Debug.LogError("Skill Id invalid");
                return false;
            }

            return _skillsById.TryGetValue(id, out skill);
        }

        /// <summary>
        /// Получить апгрейды по айди
        /// </summary>
        public bool TryGetUpgrade(string id, out UpgradeConfigDto upgrade)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                upgrade = default;
                Debug.LogError("Upgrade Id invalid");
                return false;
            }

            return _upgradesById.TryGetValue(id, out upgrade);
        }

        private void BuildIndexes(BalanceConfigDto dto)
        {
            EnemyConfigDto[] enemies = dto.Enemies ?? Array.Empty<EnemyConfigDto>();
            for (int i = 0; i < enemies.Length; i++)
            {
                EnemyConfigDto enemy = enemies[i];
                string id = enemy != null ? enemy.Id : string.Empty;

                if (string.IsNullOrWhiteSpace(id))
                {
                    continue;
                }

                _enemiesById[id] = enemy;
            }

            SkillConfigDto[] skills = dto.Skills ?? Array.Empty<SkillConfigDto>();
            for (int i = 0; i < skills.Length; i++)
            {
                SkillConfigDto skill = skills[i];
                string id = skill != null ? skill.Id : string.Empty;

                if (string.IsNullOrWhiteSpace(id))
                {
                    continue;
                }

                _skillsById[id] = skill;
            }

            UpgradeConfigDto[] upgrades = dto.Upgrades ?? Array.Empty<UpgradeConfigDto>();
            for (int i = 0; i < upgrades.Length; i++)
            {
                UpgradeConfigDto upgrade = upgrades[i];
                string id = upgrade != null ? upgrade.Id : string.Empty;

                if (string.IsNullOrWhiteSpace(id))
                {
                    continue;
                }

                _upgradesById[id] = upgrade;
            }
        }
    }
}