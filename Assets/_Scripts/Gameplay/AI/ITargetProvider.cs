using UnityEngine;

namespace MonsterCouchTest.Gameplay.AI
{
    public interface ITargetProvider
    {
        Vector3 Position { get; }
    }
}