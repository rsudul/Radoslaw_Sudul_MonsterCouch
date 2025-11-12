using System;
using UnityEngine;

namespace MonsterCouchTest.Gameplay.Enemy
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Collider2D))]
    public sealed class EnemyCollisionController : MonoBehaviour
    {
        public event EventHandler OnPlayerTouched;

        private void Reset()
        {
            var col = GetComponent<Collider2D>();
            col.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                OnPlayerTouched?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}