using UnityEngine;

namespace MonsterCouchTest.Config
{
    [CreateAssetMenu(menuName = "Config/Player Settings", fileName = "DefaultPlayerSettings")]
    public sealed class PlayerSettings : ScriptableObject
    {
        [Header("Movement")]
        [Min(0.0f)] public float Acceleration = 20.0f;
        [Min(0.0f)] public float MaxSpeed = 6.0f;
        [Range(0.0f, 1.0f)] public float Damping = 0.0f;

        [Header("Bounds")]
        [Min(0.0f)] public float ClampMargin = 0.0f;
    }
}
