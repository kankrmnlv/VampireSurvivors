namespace GameResources.Features.LevelXp.Scripts
{
    using UnityEngine;
    using GameResources.Features.Pool;

    /// <summary>
    /// Очко опыта
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public sealed class XpOrb : PooledBehaviour<XpOrb>
    {
        /// <summary>
        /// Паблик аксесор очков
        /// </summary>
        public int XpAmount => _xpAmount;
        
        [SerializeField] private Collider2D _collider2D = default;
        [SerializeField] private XpRegistryContainer _registry = default;

        [SerializeField] private int _xpAmount = 1;

        private void OnEnable() => _registry.Register(_collider2D, this);

        private void OnDisable() => _registry.Unregister(_collider2D);

        /// <summary>
        /// Передать очки опыта
        /// </summary>
        /// <param name="xpAmount"></param>
        public void SetXpAmount(int xpAmount) => _xpAmount = xpAmount;

        /// <summary>
        /// Сбор опыта
        /// </summary>
        public void Collect() => ReleaseSelf();
    }
}