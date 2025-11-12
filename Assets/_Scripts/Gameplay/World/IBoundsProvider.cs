using UnityEngine;

namespace MonsterCouchTest.Gameplay.World
{
    public interface IBoundsProvider
    {
        Rect GetWorldRect();
        Vector3 Clamp(Vector3 position);
    }
}