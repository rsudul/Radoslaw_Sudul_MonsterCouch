using UnityEngine;

namespace MonsterCouchTest.Input
{
    public sealed class KeyboardInputSource : IInputSource
    {
        public Vector2 ReadMove()
        {
            float h = UnityEngine.Input.GetAxisRaw("Horizontal");
            float v = UnityEngine.Input.GetAxisRaw("Vertical");
            var dir = new Vector2(h, v);

            return dir.sqrMagnitude > 1.0f ? dir.normalized : dir;
        }
    }
}
