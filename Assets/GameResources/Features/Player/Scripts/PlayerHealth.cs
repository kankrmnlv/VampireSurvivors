namespace GameResources.Features.Player.Scripts
{
    using UnityEngine;
    using GameResources.Features.Data.Scripts;

    /// <summary>
    /// Хп игрока
    /// </summary>
    public sealed class PlayerHealth : MonoBehaviour
    {
        private int _maxHealth = default;
        private int _currentHealth = default;

        /// <summary>
        /// Мертв ли игрок
        /// </summary>
        public bool IsDead => _currentHealth <= 0;

        private void Start()
        {
            if (BalanceManager.Balance != null)
            {
                _maxHealth = BalanceManager.Balance.Player.MaxHealth;
            }
            else
            {
                Debug.LogWarning($"{nameof(PlayerHealth)}: Balance is not loaded. MaxHealth will be 1.");
                _maxHealth = 1;
            }

            _currentHealth = _maxHealth;
        }

        /// <summary>
        /// Получить урон
        /// </summary>
        public void TakeDamage(int damage)
        {
            Debug.Log("TakeDamage: " + damage);
            if (IsDead || damage <= 0)
            {
                return;
            }

            _currentHealth -= damage;

            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                Die();
            }
        }

        private void Die()
        {
            Debug.Log($"{nameof(PlayerHealth)}: Player died");
        }
    }
}