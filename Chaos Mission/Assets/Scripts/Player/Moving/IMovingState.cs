using System;
using ChaosMission.Player.Moving.Behaviours;

namespace ChaosMission.Player.Moving
{
    public interface IMovingState
    {
        public string Name { get; }
        public byte Priority { get; }
        public Action? StateStarted { get; set; }
        public Action? StateStopped { get; set; }

        public bool IsTriggered();
        public void EnableState();
        public void DisableState();
        
    }
}