using System;

namespace ChaosMission.Player
{
    public interface IMovingState
    {
        public Action StateStartedAction { get; set; }
        public Action StateStoppedAction { get; set; }
        
        public bool IsActive { get; }
        
    }
}