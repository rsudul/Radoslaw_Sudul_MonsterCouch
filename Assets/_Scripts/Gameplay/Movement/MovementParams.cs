namespace MonsterCouchTest.Gameplay.Movement
{
    public struct MovementParams
    {
        public float Acceleration;
        public float MaxSpeed;
        public float Damping;
        public float ClampMargin;

        public static MovementParams Default => new()
        {
            Acceleration = 20.0f,
            MaxSpeed = 6.0f,
            Damping = 0.0f,
            ClampMargin = 0.0f
        };
    }
}
