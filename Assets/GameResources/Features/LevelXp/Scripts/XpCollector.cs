namespace GameResources.Features.LevelXp.Scripts
{
    using UnityEngine;
    using GameResources.Features.Data.Scripts;

    /// <summary>
    /// Собиратель очков
    /// </summary>
    [RequireComponent(typeof(CircleCollider2D))]
    public sealed class XpCollector : MonoBehaviour
    {
        [SerializeField] private CircleCollider2D _pickupCollider = default;
        [SerializeField] private XpSystem _xpSystem = default;
        [SerializeField] private XpRegistryContainer _xpRegistry = default;

        private void Start()
        {
            if (BalanceManager.Balance != null)
            {
                _pickupCollider.radius = Mathf.Max(0.1f, BalanceManager.Balance.Player.PickupRadius);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            bool hasOrb = _xpRegistry.TryGet(other, out XpOrb orb);
            if (!hasOrb || orb == null)
            {
                return;
            }
            
            orb.Collect();
            _xpSystem.AddXp(orb.XpAmount);
        }
    }
}