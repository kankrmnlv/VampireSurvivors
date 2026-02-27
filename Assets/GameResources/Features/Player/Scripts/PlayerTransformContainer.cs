namespace GameResources.Features.Player.Scripts
{
    using UnityEngine;

    /// <summary>
    /// Контейнер трансформа игрока
    /// </summary>
    [CreateAssetMenu(fileName = nameof(PlayerTransformContainer), menuName = "Features/Player/" + nameof(PlayerTransformContainer))]
    public sealed class PlayerTransformContainer : ScriptableObject
    {
        /// <summary>
        /// Паблик аксесор транформа
        /// </summary>
        public Transform Transform => _transform; 
        
        private Transform _transform = default;

        /// <summary>
        /// Передать трансформ
        /// </summary>
        /// <param name="transform"></param>
        public void Set(Transform transform) => _transform = transform;

        /// <summary>
        /// Очистить контейнер
        /// </summary>
        public void Clear() => _transform = null;
    }
}