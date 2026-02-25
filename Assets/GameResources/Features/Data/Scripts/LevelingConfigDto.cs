namespace GameResources.Features.Data.Scripts
{
    using System;
    using UnityEngine;

    /// <summary>
    /// Дата уровня
    /// </summary>
    [Serializable]
    public sealed class LevelingConfigDto
    {
        /// <summary>
        /// Паблик аксесор опыта
        /// </summary>
        public int[] XpToNextLevel => xpToNextLevel;
        
        [SerializeField] private int[] xpToNextLevel = default;
    }
}