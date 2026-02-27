namespace GameResources.Features.LevelXp.Scripts
{
    using UnityEngine;
    using GameResources.Features.Data.Scripts;
    using System;

    public sealed class XpSystem : MonoBehaviour
    {
        public event Action<int> onLevelUp = delegate { };
        public event Action<int, int, int> onProgressChanged = delegate { };
        
        /// <summary>
        /// Паблки аксесор уровня
        /// </summary>
        public int Level => _level;
        
        /// <summary>
        /// Паблик аксесор текущего опыта
        /// </summary>
        public int CurrentXp => _currentXp;
        
        /// <summary>
        /// Паблик аксесор сколько осталось до след повышения уровня
        /// </summary>
        public int XpToNext => _xpToNext;

        private int _level = 1;
        private int _currentXp = 0;
        private int _xpToNext = 0;

        private int[] _curve = default;

        private void Start()
        {
            if (BalanceManager.Balance == null)
            {
                Debug.LogError($"[{nameof(XpSystem)}] Balance not loaded.");
                return;
            }

            _curve = BalanceManager.Balance.Leveling.XpToNextLevel;
            _xpToNext = GetXpToNext(_level);
            onProgressChanged(_level, _currentXp, _xpToNext);
        }

        /// <summary>
        /// Прибавить очки опыта
        /// </summary>
        /// <param name="amount"></param>
        public void AddXp(int amount)
        {
            if (amount <= 0)
            {
                return;
            }

            _currentXp += amount;

            while (_currentXp >= _xpToNext && _xpToNext > 0)
            {
                _currentXp -= _xpToNext;
                LevelUp();
            }
            onProgressChanged(_level, _currentXp, _xpToNext);
        }

        private void LevelUp()
        {
            _level += 1;
            _xpToNext = GetXpToNext(_level);

            onLevelUp(_level);
            Debug.Log($"[{nameof(XpSystem)}] Level Up! Level={_level}");
        }

        private int GetXpToNext(int level)
        {
            if (_curve == null || _curve.Length == 0)
            {
                return 0;
            }

            int index = Mathf.Clamp(level - 1, 0, _curve.Length - 1);
            return Mathf.Max(1, _curve[index]);
        }
    }
}