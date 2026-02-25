namespace GameResources.Features.Data.Scripts
{
    using System;
    using UnityEngine;

    /// <summary>
    /// Дата врага
    /// </summary>
    [Serializable]
    public sealed class EnemyConfigDto
    {
        /// <summary>
        /// Паблик аксесор айди
        /// </summary>
        public string Id => id;
        
        /// <summary>
        /// Паблик аксесор макс здоровья
        /// </summary>
        public int MaxHealth => maxHealth;
        
        /// <summary>
        /// Паблик аксесор скорости передвижения
        /// </summary>
        public float MoveSpeed => moveSpeed;
        
        /// <summary>
        /// Паблик аксесор урона
        /// </summary>
        public int ContactDamage => contactDamage;
        
        /// <summary>
        /// Паблик аксесор награды
        /// </summary>
        public int XpReward => xpReward;
        
        [SerializeField] private string id = string.Empty;
        [SerializeField] private int maxHealth = default;
        [SerializeField] private float moveSpeed = default;
        [SerializeField] private int contactDamage = default;
        [SerializeField] private int xpReward = default;
    }
}