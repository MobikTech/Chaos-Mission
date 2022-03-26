using System;

namespace ChaosMission.Player.Moving.SubStateActions
{
    public interface IOnLadderActions
    {
        public Action? LadderingVertStarted { get; set; } 
        public Action? LadderingVertStopped { get; set; } 
        public Action? LadderingHorStarted { get; set; } 
        public Action? LadderingHorStopped { get; set; } 
    }
}