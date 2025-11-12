using System;
using UnityEngine;
using MonsterCouchTest.Gameplay.AI;

namespace MonsterCouchTest.Gameplay.Enemy
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(EnemyMovementController))]
    [RequireComponent(typeof(EnemyCollisionController))]
    public sealed class Enemy : MonoBehaviour
    {
        private Transform _player;

        private EnemyMovementController _movement;
        private EnemyCollisionController _collision;

        private void Awake()
        {
            _movement = GetComponent<EnemyMovementController>();
            _collision = GetComponent<EnemyCollisionController>();

            var threat = new TransformTargetProvider(_player);
            _movement.Initialize(transform, threat);

            _collision.OnPlayerTouched += OnPlayerTouched;
        }

        private void OnDestroy()
        {
            if (_collision != null)
            {
                _collision.OnPlayerTouched -= OnPlayerTouched;
            }
        }

        private void OnPlayerTouched(object sender, EventArgs e)
        {
            _movement.Stop();
        }

        public void SetPlayer(Transform player)
        {
            _player = player;
            var threat = new TransformTargetProvider(_player);
            _movement.Initialize(transform, threat);
        }

        public void ResetAgent()
        {
            var threat = new TransformTargetProvider(_player);
            _movement.Initialize(transform, threat);
        }
    }
}