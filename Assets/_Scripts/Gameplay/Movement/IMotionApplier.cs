using UnityEngine;
using MonsterCouchTest.Gameplay.World;

namespace MonsterCouchTest.Gameplay.Movement
{
    public interface IMotionApplier
    {
        void Apply(ref MovementState state, Transform target, IBoundsProvider? bounds, float deltaTime);
    }
}
