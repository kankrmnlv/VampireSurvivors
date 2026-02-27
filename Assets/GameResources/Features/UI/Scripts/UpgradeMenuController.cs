namespace GameResources.Features.UI.Scripts
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using GameResources.Features.Data.Scripts;
    using GameResources.Features.Skills.Scripts;

    /// <summary>
    /// View-контроллер
    /// </summary>
    public sealed class UpgradeMenuController : MonoBehaviour
    {
        private const int UPGRADE_COUNT = 3;
        
        [SerializeField] private UpgradeMenuView _view = default;
        [SerializeField] private SkillsRuntime _skillsRuntime = default;

        private readonly List<UpgradeConfigDto> _options = new List<UpgradeConfigDto>(3);
        private System.Random _random = default;

        private void Awake()
        {
            _random = new System.Random();
            if (_view != null)
            {
                _view.SetVisible(false);
            }
        }
        
        /// <summary>
        /// Логика при открытии
        /// </summary>
        public void Open()
        {
            if (_view == null || _skillsRuntime == null)
            {
                return;
            }

            if (BalanceManager.Balance == null)
            {
                return;
            }

            BuildOptions();

            for (int i = 0; i < UPGRADE_COUNT; i++)
            {
                int captured = i;
                _view.SetOption(i, _options[i].Title, _options[i].Description);
                _view.SetOptionHandler(captured, () => OnOptionSelected(captured));
            }

            Time.timeScale = 0f;
            _view.SetVisible(true);
        }

        private void OnOptionSelected(int index)
        {
            if (index < 0 || index >= _options.Count)
            {
                return;
            }

            _skillsRuntime.TryApplyUpgrade(_options[index]);
            _view.SetVisible(false);
            Time.timeScale = 1f;
        }

        private void BuildOptions()
        {
            _options.Clear();

            UpgradeConfigDto[] all = BalanceManager.Balance.UpgradesById != null ? GetUpgradesArrayFromRepo(BalanceManager.Balance.UpgradesById) : Array.Empty<UpgradeConfigDto>();

            if (all.Length == 0)
            {
                return;
            }
            
            for (int i = 0; i < UPGRADE_COUNT; i++)
            {
                _options.Add(PickUnique(all, _options));
            }
        }

        private UpgradeConfigDto[] GetUpgradesArrayFromRepo(IReadOnlyDictionary<string, UpgradeConfigDto> dict)
        {
            UpgradeConfigDto[] arr = new UpgradeConfigDto[dict.Count];
            int index = 0;

            foreach (KeyValuePair<string, UpgradeConfigDto> pair in dict)
            {
                arr[index] = pair.Value;
                index++;
            }

            return arr;
        }

        private UpgradeConfigDto PickUnique(UpgradeConfigDto[] all, List<UpgradeConfigDto> alreadyPicked)
        {
            int safety = 50;

            while (safety > 0)
            {
                safety--;

                int idx = _random.Next(0, all.Length);
                UpgradeConfigDto candidate = all[idx];

                bool exists = false;
                for (int i = 0; i < alreadyPicked.Count; i++)
                {
                    if (alreadyPicked[i] != null && candidate != null && alreadyPicked[i].Id == candidate.Id)
                    {
                        exists = true;
                        break;
                    }
                }

                if (!exists)
                {
                    return candidate;
                }
            }

            return all[_random.Next(0, all.Length)];
        }
    }
}