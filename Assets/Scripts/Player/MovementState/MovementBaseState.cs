namespace ProjectTwo.Player
{
    public abstract class MovementBaseState
    {
        public abstract void EnterState(PlayerMove movement);

        public abstract void UpdateState(PlayerMove movement);
    }
}


