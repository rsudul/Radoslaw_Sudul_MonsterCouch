using UnityEngine;
using MonsterCouchTest.Gameplay.AI;
using MonsterCouchTest.Gameplay.Movement;
using MonsterCouchTest.Gameplay.World;
using MonsterCouchTest.Infrastructure;
using MonsterCouchTest.Input;

namespace MonsterCouchTest.Gameplay.Enemy
{
    [DisallowMultipleComponent]
    public sealed class EnemyMovementController : MonoBehaviour
    {
        [Inject]
        private IBoundsProvider _bounds;

        private IInputSource _input;
        private IMovementModel _model;
        private IMotionApplier _applier;

        private MovementParams _params;
        private MovementState _state;
        private bool _stopped;

        [Header("Movement")]
        [SerializeField, Min(0f)] private float _acceleration = 18f;
        [SerializeField, Min(0f)] private float _maxSpeed = 5f;
        [SerializeField, Range(0f, 1f)] private float _damping = 0.05f;
        [SerializeField, Range(0f, 1f)] private float _bounceDamping = 0.1f;

        public void Initialize(Transform self, ITargetProvider threat)
        {
            _input = new FleeInputSource(self, threat);
            _model = new BasicMovementModel();
            _applier = new BouncyTransformMotionApplier(_bounceDamping);

            _params = new MovementParams
            {
                Acceleration = _acceleration,
                MaxSpeed = _maxSpeed,
                Damping = _damping,
                ClampMargin = 0f
            };

            _stopped = false;
            _state = default;
        }

        public void Stop()
        {
            _stopped = true;
            _state.Velocity = Vector2.zero;
        }

        private void Update()
        {
            if (_stopped || _input == null)
            {
                return;
            }

            var move = _input.ReadMove();
            _model.Step(ref _state, in _params, new MovementInput(move), Time.deltaTime);
            _applier.Apply(ref _state, transform, _bounds, Time.deltaTime);
        }
    }
}
