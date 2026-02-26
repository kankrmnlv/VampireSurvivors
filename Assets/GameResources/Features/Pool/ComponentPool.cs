namespace GameResources.Features.Pool
{
    using UnityEngine;
    using UnityEngine.Pool;

    /// <summary>
    /// Абстрация компонента пула
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ComponentPool<T> : MonoBehaviour where T : Component
    {
        [SerializeField] private T _prefab = default;
        [SerializeField] private int _defaultCapacity = default;
        [SerializeField] private int _maxSize = default;

        private ObjectPool<T> _pool = default;

        protected virtual void Awake() => _pool = new ObjectPool<T>(Create, OnGet, OnRelease, OnDestroyObject, collectionCheck: false, defaultCapacity: _defaultCapacity, maxSize: _maxSize);

        public T Get(Vector3 position, Quaternion rotation)
        {
            T instance = _pool.Get();

            Transform t = instance.transform;
            t.SetPositionAndRotation(position, rotation);

            PooledBehaviour<T> pooled = instance as PooledBehaviour<T>;
            if (pooled != null)
            {
                pooled.SetPool(_pool);
            }

            return instance;
        }

        protected virtual T Create()
        {
            return Instantiate(_prefab, transform);
        }

        protected virtual void OnGet(T instance) => instance.gameObject.SetActive(true);

        protected virtual void OnRelease(T instance) => instance.gameObject.SetActive(false);

        protected virtual void OnDestroyObject(T instance) => Destroy(instance.gameObject);
    }
}