using UnityEngine;
using MonsterCouchTest.Gameplay.World;

namespace MonsterCouchTest.Gameplay.Movement
{
    public sealed class BouncyTransformMotionApplier : IMotionApplier
    {
        private readonly float _bounceDamping;

        public BouncyTransformMotionApplier(float bounceDamping = 0.1f)
        {
            _bounceDamping = Mathf.Clamp01(bounceDamping);
        }

        public void Apply(ref MovementState state, Transform target, IBoundsProvider? bounds, float deltaTime)
        {
            var pos = target.position;
            var next = pos + (Vector3)state.Velocity * deltaTime;

            if (bounds != null)
            {
                var r = bounds.GetWorldRect();
                bool outX = next.x < r.xMin || next.x > r.xMax;
                bool outY = next.y < r.yMin || next.y > r.yMax;

                if (outX)
                {
                    state.Velocity.x = -state.Velocity.x * (1f - _bounceDamping);
                    next.x = Mathf.Clamp(next.x, r.xMin, r.xMax);
                }
                if (outY)
                {
                    state.Velocity.y = -state.Velocity.y * (1f - _bounceDamping);
                    next.y = Mathf.Clamp(next.y, r.yMin, r.yMax);
                }
            }

            target.position = next;
        }
    }
}