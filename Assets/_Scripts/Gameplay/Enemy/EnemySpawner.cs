using System.Collections.Generic;
using UnityEngine;
using MonsterCouchTest.Gameplay.World;
using MonsterCouchTest.Infrastructure;
using MonsterCouchTest.Utilities;

namespace MonsterCouchTest.Gameplay.Enemy
{
    public sealed class EnemySpawner : MonoBehaviour
    {
        [Inject] private IBoundsProvider _bounds;

        private ObjectPool<Enemy> _pool;
        private readonly List<Enemy> _active = new();

        [Header("Setup")]
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField, Min(1)] private int _initialCount = 1000;
        [SerializeField, Min(0f)] private float _spawnMargin = 0.2f;
        [SerializeField] private Transform _playerTransform;

        [Header("Parenting")]
        [SerializeField] private Transform _poolParent;
        [SerializeField] private Transform _activeParent;

        private void Awake()
        {
            if (_enemyPrefab == null)
            {
                Debug.LogError("[EnemySpawner] Enemy prefab not assigned.");
                enabled = false;
                return;
            }

            if (_playerTransform == null)
            {
                var tagged = GameObject.FindGameObjectWithTag("Player");
                if (tagged != null)
                {
                    _playerTransform = tagged.transform;
                }
            }

            _pool = new ObjectPool<Enemy>(_enemyPrefab, prewarm: _initialCount, parent: _poolParent);
        }

        private void Start()
        {
            BeginPlay();
        }

        public void BeginPlay()
        {
            var rect = _bounds != null ? _bounds.GetWorldRect() : new Rect(-8, -5, 16, 10);

            for (int i = 0; i < _initialCount; i++)
            {
                var enemy = _pool.Get();

                var resolver = ServiceLocator.Current;
                var behaviours = enemy.GetComponentsInChildren<MonoBehaviour>(true);
                foreach (var mb in behaviours)
                {
                    Injector.InjectInto(mb, resolver);
                }

                _active.Add(enemy);

                var pos = RandomPointInRect(rect, _spawnMargin);
                var t = enemy.transform;
                if (_activeParent != null)
                {
                    t.SetParent(_activeParent, worldPositionStays: true);
                }
                t.position = pos;

                enemy.ResetAgent();
                if (_playerTransform != null)
                {
                    enemy.SetPlayer(_playerTransform);
                }
            }
        }

        public void EndPlay()
        {
            for (int i = _active.Count - 1; i >= 0; i--)
            {
                _pool.Release(_active[i]);
            }
            _active.Clear();
        }

        private static Vector3 RandomPointInRect(Rect r, float margin)
        {
            float x = Random.Range(r.xMin + margin, r.xMax - margin);
            float y = Random.Range(r.yMin + margin, r.yMax - margin);
            return new Vector3(x, y, 0f);
        }
    }
}