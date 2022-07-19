using System;
using ChaosMission.Common;
using ChaosMission.GameSettings.PlayerMoving;
using ChaosMission.Input.ActionsMaps;
using ChaosMission.Input.Handlers;
using ChaosMission.Player.Moving.Behaviours;
using ChaosMission.Player.Moving.SubStateActions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ChaosMission.Player.Moving.States
{
    public sealed class OnAir : MovingState, IFixedUpdatable, IOnAirActions
    {
        public Action? JumpingStarted { get; set; }
        public Action? JumpingStopped { get; set; }
        public Action? FallingStarted { get; set; }
        public Action? FallingStopped { get; set; }
        
        private readonly InputAction _airMovingAction;
        private readonly Rigidbody2D _rigidbody2D;
        private readonly IOnAirSettings _onGroundSettings;
        private bool _isFalling = false;
        private const float JumpTriggerApproximation = 0.1f;

        public OnAir(byte priority, Rigidbody2D rigidbody2D, IOnAirSettings onAirSettings) 
            : base("OnAir", priority)
        {
            _airMovingAction = new PlayerMovingHandler().GetByType(PlayerMovingActions.AirMoving);
            _rigidbody2D = rigidbody2D;
            _onGroundSettings = onAirSettings;
        }

        public override bool IsTriggered()
        {
            return !CustomMath.Approximate(_rigidbody2D.velocity.y, 0f, JumpTriggerApproximation);
        }

        public override void EnableState()
        {
            _airMovingAction.Enable();
            
            float verticalVelocity = _rigidbody2D.velocity.y;
            
            if (CustomMath.LargerThanZero(verticalVelocity))
            {
                JumpingStarted?.Invoke();
                _isFalling = false;
            }
            else if (CustomMath.LessThanZero(verticalVelocity))
            {
                FallingStarted?.Invoke();
                _isFalling = true;
            }
            base.EnableState();
        }

        public override void DisableState()
        {
            _airMovingAction.Disable();
            
            float verticalVelocity = _rigidbody2D.velocity.y;
            
            if (CustomMath.LargerThanZero(verticalVelocity))
            {
                JumpingStopped?.Invoke();
            }
            else
            {
                FallingStopped?.Invoke();
            }
            
            base.DisableState();
        }
     
        public void FixedUpdate()
        {
            CallSubStateActions();
            TryMove();
        }

        private void CallSubStateActions()
        {
            if (CanStartJumping())
            {
                JumpingStarted?.Invoke();
            }
            else if (CanStartFalling())
            {
                JumpingStopped?.Invoke();
                FallingStarted?.Invoke();
            }
        }

        private bool CanStartJumping() => CustomMath.LargerThanZero(_rigidbody2D.velocity.y) && _isFalling;
        private bool CanStartFalling() => CustomMath.LessThanZero(_rigidbody2D.velocity.y) && !_isFalling;

        private void TryMove()
        {
            if (!_airMovingAction.enabled)
            {
                return;
            }
            
            float actionValue = _airMovingAction.ReadValue<float>();

            if (actionValue == 0f && _rigidbody2D.velocity.x != 0)
            {
                ResetVelocity();
                return;
            }

            if (actionValue == 0f)
            {
                return;
            }

            if (DirectionChanged(actionValue))
            {
                ResetVelocity();
                return;
            }
            
            Vector2 currentVelocity = _rigidbody2D.velocity;
            float xVelocityAddition = actionValue * _onGroundSettings.SteamAccelerationFactor;
            _rigidbody2D.velocity = new Vector2(
                Mathf.Clamp(
                    currentVelocity.x + xVelocityAddition, 
                    -_onGroundSettings.MaxSteamSpeed, 
                    _onGroundSettings.MaxSteamSpeed), 
                currentVelocity.y);
        }

        private bool DirectionChanged(float actionValue) =>
            CustomMath.HaveDifferentSigns(actionValue, _rigidbody2D.velocity.x);
        
        private void ResetVelocity() => _rigidbody2D.velocity = new Vector2(0f, _rigidbody2D.velocity.y);
       
    }
}
