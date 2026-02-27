namespace GameResources.Features.UI.Scripts
{
    using UnityEngine;
    using UnityEngine.UI;
    using GameResources.Features.LevelXp.Scripts;
    using GameResources.Features.Player.Scripts;

    /// <summary>
    /// Контроллер hud игрока
    /// </summary>
    public sealed class PlayerHudPresenter : MonoBehaviour
    {
        [Header("Sources")]
        [SerializeField] private PlayerHealth _playerHealth = default;
        [SerializeField] private XpSystem _xpSystem = default;

        [Header("UI")]
        [SerializeField] private Slider _hpBar = default;
        [SerializeField] private Slider _xpBar = default;
        [SerializeField] private Text _levelText = default;
        
        [Header("Death panel")]
        [SerializeField] private GameObject _deathPanel = default;

        private void OnEnable()
        {
            _playerHealth.onDeath += OnDeath;
            _playerHealth.onHealthChanged += OnHealthChanged;
            _xpSystem.onProgressChanged += OnXpChanged;
            OnXpChanged(_xpSystem.Level, _xpSystem.CurrentXp, _xpSystem.XpToNext);
        }

        private void OnDisable()
        {
            _playerHealth.onDeath -= OnDeath;
            _playerHealth.onHealthChanged -= OnHealthChanged;
            _xpSystem.onProgressChanged -= OnXpChanged;
        }

        private void OnHealthChanged(int current, int max)
        {
            if (_hpBar == null)
                return;

            float normalized = max > 0 ? (float)current / max : 0f;
            _hpBar.value = Mathf.Clamp01(normalized);
        }

        private void OnXpChanged(int level, int currentXp, int xpToNext)
        {
            _levelText.text = $"Lvl {level}";

            if (_xpBar != null)
            {
                float normalized = xpToNext > 0 ? (float)currentXp / xpToNext : 0f;
                _xpBar.value = Mathf.Clamp01(normalized);
            }
        }
        
        private void OnDeath() => _deathPanel.SetActive(true);
    }
}