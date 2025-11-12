using UnityEngine;
using MonsterCouchTest.Config;
using MonsterCouchTest.Gameplay.Movement;

namespace MonsterCouchTest.Gameplay.Player
{
    [DisallowMultipleComponent]
    public sealed class Player : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private PlayerSettings _settings;

        [Header("Controllers")]
        [SerializeField] private PlayerMovementController _movementController;

        private void Awake()
        {
            if (_movementController == null)
            {
                _movementController = GetComponent<PlayerMovementController>();
            }

            var p = new MovementParams
            {
                Acceleration = _settings != null ? _settings.Acceleration : MovementParams.Default.Acceleration,
                MaxSpeed = _settings != null ? _settings.MaxSpeed : MovementParams.Default.MaxSpeed,
                Damping = _settings != null ? _settings.Damping : MovementParams.Default.Damping,
                ClampMargin = _settings != null ? _settings.ClampMargin : MovementParams.Default.ClampMargin
            };

            _movementController.Initialize(p);
        }

        private void Reset()
        {
            if (_movementController == null)
            {
                _movementController = GetComponent<PlayerMovementController>();
            }
        }
    }
}
