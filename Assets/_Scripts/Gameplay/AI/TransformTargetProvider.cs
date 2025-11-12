using UnityEngine;

namespace MonsterCouchTest.Gameplay.AI
{
    public sealed class TransformTargetProvider : ITargetProvider
    {
        private readonly Transform _transform;

        public TransformTargetProvider(Transform t)
        {
            _transform = t;
        }

        public Vector3 Position => _transform != null ? _transform.position : Vector3.zero;
    }
}