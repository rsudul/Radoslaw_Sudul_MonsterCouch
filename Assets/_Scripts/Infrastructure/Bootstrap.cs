using UnityEngine;

namespace MonsterCouchTest.Infrastructure
{
    /// <summary>
    /// Entry point of every scene.
    /// Initializes and readies game systems, runs before other scripts.
    /// </summary>
    [DefaultExecutionOrder(-100)]
    public sealed class Bootstrap : MonoBehaviour
    {
        private static bool _initialized = false;

        [SerializeField]
        private Camera _camera;
        [SerializeField, Min(0.0f)]
        private float _boundsMargin = 0.0f;

        private void Awake()
        {
            if (_initialized)
            {
                return;
            }

            InitSystems();
            _initialized = true;
        }

        private void OnDestroy()
        {
            if (_initialized)
            {
                Shutdown();
                _initialized = false;
            }
        }

        private void InitSystems()
        {
            if (_camera == null)
            {
                _camera = Camera.main;
            }

            var registry = new ServiceRegistry().AddGameSystems(_camera, _boundsMargin);

            ServiceLocator.Current = registry.Build();

            InjectHierarchy();
        }

        private void Shutdown()
        {
            (ServiceLocator.Current as System.IDisposable)?.Dispose();
        }

        private void InjectHierarchy()
        {
            var resolver = ServiceLocator.Current;
            var roots = gameObject.scene.GetRootGameObjects();
            foreach (var go in roots)
            {
                var all = go.GetComponentsInChildren<MonoBehaviour>(true);
                foreach (var mb in all)
                {
                    Injector.InjectInto(mb, resolver);
                }
            }
        }
    }
}
