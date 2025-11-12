using UnityEngine;
using MonsterCouchTest.Infrastructure;
using MonsterCouchTest.Input;
using MonsterCouchTest.Gameplay.Movement;
using MonsterCouchTest.Gameplay.World;

namespace MonsterCouchTest.Gameplay.Player
{
    [DisallowMultipleComponent]
    public sealed class PlayerMovementController : MonoBehaviour
    {
        [Inject]
        private IBoundsProvider _bounds;

        private IMovementModel _model;
        private IMotionApplier _applier;
        private IInputSource _input;

        private MovementParams _params;
        private MovementState _state;

        public void Initialize(in MovementParams p)
        {
            _params = p;
        }

        private void Awake()
        {
            _model = new BasicMovementModel();
            _applier = new TransformMotionApplier();
            _input = new KeyboardInputSource();
        }

        private void Update()
        {
            var move = _input != null ? _input.ReadMove() : Vector2.zero;
            _model.Step(ref _state, in _params, new MovementInput(move), Time.deltaTime);
            _applier.Apply(ref _state, transform, _bounds, Time.deltaTime);
        }
    }
}
