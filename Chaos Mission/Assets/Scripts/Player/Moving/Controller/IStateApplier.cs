namespace ChaosMission.Player.Moving.Controller
{
    public interface IStateApplier
    {
        public void ApplyState(IMovingState state);
    }
}