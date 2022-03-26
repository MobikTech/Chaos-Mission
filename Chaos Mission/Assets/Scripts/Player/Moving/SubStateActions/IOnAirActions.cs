using System;

namespace ChaosMission.Player.Moving.SubStateActions
{
    public interface IOnAirActions
    {
        public Action? JumpingStarted { get; set; } 
        public Action? JumpingStopped { get; set; } 
        public Action? FallingStarted { get; set; } 
        public Action? FallingStopped { get; set; } 
    }
}