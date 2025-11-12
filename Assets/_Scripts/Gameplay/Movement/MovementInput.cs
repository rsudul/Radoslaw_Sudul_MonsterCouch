using UnityEngine;

namespace MonsterCouchTest.Gameplay.Movement
{
    public readonly struct MovementInput
    {
        public readonly Vector2 Move;
        public MovementInput(Vector2 move) => Move = move;
    }
}
