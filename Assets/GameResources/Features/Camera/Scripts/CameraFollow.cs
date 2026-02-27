namespace GameResources.Features.Camera.Scripts
{
    using UnityEngine;
    using GameResources.Features.Player.Scripts;

    /// <summary>
    /// Простое следование камеры за игроком
    /// </summary>
    public sealed class CameraFollow : MonoBehaviour
    {
        [SerializeField] private PlayerTransformContainer _playerTransform = default;
        [SerializeField] private float _followSpeed = 8f;
        [SerializeField] private Vector3 _offset = new Vector3(0f, 0f, -10f);

        private void FixedUpdate()
        {
            if (_playerTransform == null || _playerTransform.Transform == null)
                return;

            Vector3 targetPos = _playerTransform.Transform.position + _offset;

            transform.position = Vector3.Lerp(
                transform.position,
                targetPos,
                _followSpeed * Time.fixedDeltaTime
            );
        }
    }
}