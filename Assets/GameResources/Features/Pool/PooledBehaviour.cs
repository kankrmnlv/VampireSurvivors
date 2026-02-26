namespace GameResources.Features.Pool
{
    using UnityEngine;
    using UnityEngine.Pool;

    /// <summary>
    /// Абстракция системы пула
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class PooledBehaviour<T> : MonoBehaviour where T : Component
    {
        private IObjectPool<T> _ownerPool = default;

        /// <summary>
        /// Запулить
        /// </summary>
        public void SetPool(IObjectPool<T> pool) => _ownerPool = pool;

        protected void ReleaseSelf()
        {
            if (_ownerPool != null)
            {
                _ownerPool.Release((T)(object)this);
                return;
            }

            Destroy(gameObject);
        }
    }
}