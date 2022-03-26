using System;

namespace ChaosMission.Player.Moving.SubStateActions
{
    public interface IOnGroundActions
    {
        public Action? IdleStarted { get; set; }
        public Action? IdleStopped { get; set; }
        public Action? WalkingStarted { get; set; }
        public Action? WalkingStopped { get; set; }
    }
}