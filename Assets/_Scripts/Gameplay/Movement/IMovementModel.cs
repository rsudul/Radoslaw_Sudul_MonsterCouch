namespace MonsterCouchTest.Gameplay.Movement
{
    public interface IMovementModel
    {
        void Step(ref MovementState state, in MovementParams p, in MovementInput input, float deltaTime);
    }
}
