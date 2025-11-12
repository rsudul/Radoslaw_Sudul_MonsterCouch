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

        }

        private void Shutdown()
        {

        }
    }
}
