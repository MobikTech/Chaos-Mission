using ChaosMission.Player.Moving;
using ChaosMission.Player.Moving.Controller;
using ChaosMission.Player.Moving.SubStateActions;
using UnityEngine;

namespace ChaosMission.Player.Effects
{
    public class WalkingParticleHandler : GeneralParticleHandler
    {
        [SerializeField] private MoveController _moveController = null!;
        private IOnGroundActions _onGroundActions = null!;
        
        protected override void Start()
        {
            _onGroundActions = (IOnGroundActions) _moveController.MovingStates[MovingStateType.OnGround];
            base.Start();
        } 
        protected override void SubscribeActions()
        {
            _onGroundActions.WalkingStarted += _particleSystem.Play;
            _onGroundActions.WalkingStopped += _particleSystem.Stop;
        }
    }
}