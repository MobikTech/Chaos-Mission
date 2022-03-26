using System;

namespace ChaosMission.Player.Moving
{
    public abstract class MovingState : IMovingState
    {
        protected MovingState(string name, byte priority)
        {
            Name = name;
            Priority = priority;
            IsActive = false;
        }

        public string Name { get; }
        public byte Priority { get; }
        public bool IsActive { get; private set; }
        public Action? StateStarted { get; set; }
        public Action? StateStopped { get; set; }
        
        
        public abstract bool IsTriggered();
        public virtual void EnableState()
        {
            StateStarted?.Invoke();
            IsActive = true;
        }
        public virtual void DisableState()
        {
            StateStopped?.Invoke();
            IsActive = false;
        }
    }
}