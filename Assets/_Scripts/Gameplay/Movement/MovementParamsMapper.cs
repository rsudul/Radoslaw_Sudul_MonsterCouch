using MonsterCouchTest.Config;

namespace MonsterCouchTest.Gameplay.Movement
{
    public static class MovementParamsMapper
    {
        public static MovementParams From(PlayerSettings settings) => new MovementParams
        {
            Acceleration = settings.Acceleration,
            MaxSpeed = settings.MaxSpeed,
            Damping = settings.Damping,
            ClampMargin = settings.ClampMargin
        };
    }
}
