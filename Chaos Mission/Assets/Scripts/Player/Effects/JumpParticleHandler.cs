using ChaosMission.Player.Moving;
using ChaosMission.Player.Moving.Controller;
using ChaosMission.Player.Moving.SubStateActions;
using UnityEngine;

namespace ChaosMission.Player.Effects
{
    public class JumpParticleHandler : GeneralParticleHandler
    {
        [SerializeField] private MoveController _moveController = null!;
        private IOnAirActions _onAirActions = null!;
        
        protected override void Start()
        {
            _onAirActions = (IOnAirActions) _moveController.MovingStates[MovingStateType.OnAir];
            base.Start();
        }

        protected override void SubscribeActions() => _onAirActions.FallingStopped += _particleSystem.Play;
    }
}