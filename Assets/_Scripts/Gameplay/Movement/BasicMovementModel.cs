namespace MonsterCouchTest.Gameplay.Movement
{
    public sealed class BasicMovementModel : IMovementModel
    {
        public void Step(ref MovementState state, in MovementParams p, in MovementInput input, float deltaTime)
        {
            var vel = state.Velocity;

            var move = input.Move;
            if (move.sqrMagnitude > 0.0f)
            {
                var dir = move.normalized;
                vel += dir * p.Acceleration * deltaTime;
            }

            if (p.Damping > 0.0f)
            {
                var factor = 1.0f - p.Damping * deltaTime;
                factor = factor < 0.0f ? 0.0f : factor;
                vel *= factor;
            }

            float maxSpeed = p.MaxSpeed;
            if (maxSpeed > 0.0f && vel.sqrMagnitude > maxSpeed * maxSpeed)
            {
                vel = vel.normalized * maxSpeed;
            }

            state.Velocity = vel;
        }
    }
}
