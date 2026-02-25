namespace GameResources.Features.Data.Scripts
{
    using System;
    using UnityEngine;
    
    /// <summary>
    /// Дата общего баланса
    /// </summary>
    [Serializable]
    public sealed class BalanceConfigDto
    {
        /// <summary>
        /// Паблик аксесор даты игрока
        /// </summary>
        public PlayerConfigDto Player => player;
        
        /// <summary>
        /// Паблик аксесор даты уровня
        /// </summary>
        public LevelingConfigDto Leveling => leveling;
        
        /// <summary>
        /// Паблик аксесор даты врага
        /// </summary>
        public EnemyConfigDto[] Enemies => enemies;
        
        /// <summary>
        /// Паблик аксесор даты навыков
        /// </summary>
        public SkillConfigDto[] Skills => skills;
        
        /// <summary>
        /// Паблик аксесор даты апгрейдов
        /// </summary>
        public UpgradeConfigDto[] Upgrades => upgrades;
        
        [SerializeField] private PlayerConfigDto player = default;
        [SerializeField] private LevelingConfigDto leveling = default;
        [SerializeField] private EnemyConfigDto[] enemies = default;
        [SerializeField] private SkillConfigDto[] skills = default;
        [SerializeField] private UpgradeConfigDto[] upgrades = default;
    }
}