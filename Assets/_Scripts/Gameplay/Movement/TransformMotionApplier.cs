using UnityEngine;
using MonsterCouchTest.Gameplay.World;

namespace MonsterCouchTest.Gameplay.Movement
{
    public sealed class TransformMotionApplier : IMotionApplier
    {
        public void Apply(ref MovementState state, Transform target, IBoundsProvider? bounds, float deltaTime)
        {
            var pos = target.position;
            pos += (Vector3)(state.Velocity) * deltaTime;

            if (bounds != null)
            {
                pos = bounds.Clamp(pos);
            }

            target.position = pos;
        }
    }
}
