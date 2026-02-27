namespace GameResources.Features.LevelXp.Scripts
{
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Контейнер очков опыта
    /// </summary>
    [CreateAssetMenu(fileName = nameof(XpRegistryContainer), menuName = "Features/LevelXp/" + nameof(XpRegistryContainer))]
    public sealed class XpRegistryContainer : ScriptableObject
    {
        private readonly Dictionary<int, XpOrb> _byColliderId = new Dictionary<int, XpOrb>();

        /// <summary>
        /// Очистить список
        /// </summary>
        public void Clear() => _byColliderId.Clear();

        /// <summary>
        /// Добавиться в список
        /// </summary>
        public void Register(Collider2D collider, XpOrb orb)
        {
            if (collider == null || orb == null)
            {
                return;
            }

            _byColliderId[collider.GetInstanceID()] = orb;
        }

        /// <summary>
        /// Удалиться из списка
        /// </summary>
        public void Unregister(Collider2D collider)
        {
            if (collider == null)
            {
                return;
            }

            _byColliderId.Remove(collider.GetInstanceID());
        }

        /// <summary>
        /// Получить компонент через коллайдер
        /// </summary>
        public bool TryGet(Collider2D collider, out XpOrb orb)
        {
            if (collider == null)
            {
                orb = default;
                return false;
            }

            return _byColliderId.TryGetValue(collider.GetInstanceID(), out orb);
        }
    }
}