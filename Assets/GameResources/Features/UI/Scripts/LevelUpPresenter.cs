namespace GameResources.Features.UI.Scripts
{
    using UnityEngine;
    using GameResources.Features.LevelXp.Scripts;

    public sealed class LevelUpPresenter : MonoBehaviour
    {
        [SerializeField] private XpSystem _xpSystem = default;
        [SerializeField] private UpgradeMenuController _menu = default;

        private void OnEnable() => _xpSystem.onLevelUp += OnLevelUp;

        private void OnDisable() => _xpSystem.onLevelUp -= OnLevelUp;

        private void OnLevelUp(int level) => _menu.Open();
    }
}