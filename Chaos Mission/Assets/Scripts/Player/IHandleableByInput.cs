namespace ChaosMission.Player
{
    public interface IHandleableByInput
    {
        public void OnDestroy();
        public void OnEnable();
        public void OnDisable();
    }
}