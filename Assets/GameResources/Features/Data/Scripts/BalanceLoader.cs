namespace GameResources.Features.Data.Scripts
{
    using UnityEngine;
    using System.IO;
    
    /// <summary>
    /// Загрузка баланса
    /// </summary>
    public static class BalanceLoader
    {
        /// <summary>
        /// Чтение данных баланса json
        /// </summary>
        public static BalanceConfigDto LoadFromStreamingAssets(string fileName)
        {
            string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

            if (!File.Exists(filePath))
            {
                Debug.LogError($"{nameof(BalanceLoader)}: Balance file not found: {filePath}");
            }

            string json = File.ReadAllText(filePath);

            if (string.IsNullOrWhiteSpace(json))
            {
                Debug.LogError($"{nameof(BalanceLoader)}: Balance file is empty: {filePath}");
            }

            BalanceConfigDto config = JsonUtility.FromJson<BalanceConfigDto>(json);

            if (config == null)
            {
                Debug.LogError($"{nameof(BalanceLoader)}: Failed to parse balance JSON: {filePath}");
            }

            return config;
        }
    }
}