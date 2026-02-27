namespace GameResources.Features.Data.Scripts
{
    using System;
    using UnityEngine;

    /// <summary>
    /// Дата апгрейдов
    /// </summary>
    [Serializable]
    public sealed class UpgradeConfigDto
    {
        /// <summary>
        /// Паблик аксесор айди
        /// </summary>
        public string Id => id;
        
        /// <summary>
        /// Паблик аксесор названия
        /// </summary>
        public string Title => title;
        
        /// <summary>
        /// Паблик аксесор описания
        /// </summary>
        public string Description => description;
        
        /// <summary>
        /// Паблик аксесор айди навыка
        /// </summary>
        public string TargetSkillId => targetSkillId;
        
        /// <summary>
        /// Паблик аксесор статы
        /// </summary>
        public string Stat => stat;
        
        /// <summary>
        /// Паблик аксесор оп
        /// </summary>
        public string Op => op;
        
        /// <summary>
        /// Паблик аксесор значения
        /// </summary>
        public float Value => value;
        
        [SerializeField] private string id = string.Empty;
        [SerializeField] private string title = string.Empty;
        [SerializeField] private string description = string.Empty;

        [SerializeField] private string targetSkillId = string.Empty;
        [SerializeField] private string stat = string.Empty;
        [SerializeField] private string op = string.Empty;
        [SerializeField] private float value = default;
    }
}