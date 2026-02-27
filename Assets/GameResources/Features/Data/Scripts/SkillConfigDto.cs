namespace GameResources.Features.Data.Scripts
{
    using System;
    using UnityEngine;

    /// <summary>
    /// Дата навыков
    /// </summary>
    [Serializable]
    public sealed class SkillConfigDto
    {
        [SerializeField] private string id = string.Empty;

        // Projectile stats
        [SerializeField] private int damage = default;
        [SerializeField] private float cooldown = default;
        [SerializeField] private float projectileSpeed = default;
        [SerializeField] private int projectilesCount = default;

        // Aura stats
        [SerializeField] private float tickInterval = default;
        [SerializeField] private float radius = default;

        public string Id => id;

        public int Damage => damage;
        public float Cooldown => cooldown;
        public float ProjectileSpeed => projectileSpeed;
        public int ProjectilesCount => projectilesCount;

        public float TickInterval => tickInterval;
        public float Radius => radius;
    }
}