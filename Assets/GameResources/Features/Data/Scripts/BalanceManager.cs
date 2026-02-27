namespace GameResources.Features.Data.Scripts
{
    using System;
    using UnityEngine;

    /// <summary>
    /// Инициализация данных баланса
    /// </summary>
    public sealed class BalanceManager : MonoBehaviour
    {
        /// <summary>
        /// Паблик аксесор синглтона баланса
        /// </summary>
        public static BalanceRepository Balance => _balance;

        private static BalanceRepository _balance = default;
        
        [SerializeField] private string _balanceFileName = "balance.json";

        private void Awake()
        {
            if (_balance != null)
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);

            try
            {
                _balance = new BalanceRepository(BalanceLoader.LoadFromStreamingAssets(_balanceFileName));

                Debug.Log($"{nameof(BalanceManager)}: Balance loaded. Enemies={_balance.EnemiesById.Count}, Skills={_balance.SkillsById.Count}, Upgrades={_balance.UpgradesById.Count}");
            }
            catch (Exception exception)
            {
                Debug.LogError($"{nameof(BalanceManager)}: Failed to load balance. {exception}");
                _balance = null;
            }
        }
    }
}