namespace GameResources.Features.Skills.Scripts
{
    /// <summary>
    /// Рантайм статы
    /// </summary>
    public sealed class SkillRuntimeState
    {
        /// <summary>
        /// Конструктор-инит
        /// </summary>
        /// <param name="id"></param>
        public SkillRuntimeState(string id) => Id = id;
        
        public string Id { get; private set; }
        
        public int Damage { get; set; }

        public float Cooldown { get; set; }

        public float ProjectileSpeed { get; set; }

        public int ProjectilesCount { get; set; }

        public float TickInterval { get; set; }

        public float Radius { get; set; }
    }
}