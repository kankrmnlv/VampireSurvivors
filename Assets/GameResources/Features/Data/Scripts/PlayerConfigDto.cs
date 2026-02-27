namespace GameResources.Features.Data.Scripts
{
    using System;
    using UnityEngine;

    /// <summary>
    /// Дата игрока
    /// </summary>
    [Serializable]
    public sealed class PlayerConfigDto
    {
        /// <summary>
        /// Паблик аксесор макс здоровья
        /// </summary>
        public int MaxHealth => maxHealth;
        
        /// <summary>
        /// Паблик аксесор скорости передвижения
        /// </summary>
        public float MoveSpeed => moveSpeed;
        
        /// <summary>
        /// Паблик аксесор радиуса подбора предметов
        /// </summary>
        public float PickupRadius => pickupRadius;
        
        /// <summary>
        /// Паблик аксесор расстояния автоатаки
        /// </summary>
        public float AutoAttackRange => autoAttackRange;
        
        /// <summary>
        /// Паблик аксесор интервала автоатаки
        /// </summary>
        public float AutoAttackScanInterval => autoAttackScanInterval;
        
        [SerializeField] private int maxHealth = default;
        [SerializeField] private float moveSpeed = default;
        [SerializeField] private float pickupRadius = default;
        [SerializeField] private float autoAttackRange = default;
        [SerializeField] private float autoAttackScanInterval = default;
    }
}