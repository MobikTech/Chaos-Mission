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
    public class OnGround : MovingState, IFixedUpdatable, IOnGroundActions
    {
        public Action? IdleStarted { get; set; }
        public Action? IdleStopped { get; set; }
        public Action? WalkingStarted { get; set; }
        public Action? WalkingStopped { get; set; }
        
        private bool _isIdle = true;

        private readonly InputAction _walkingAction;
        private readonly InputAction _jumpingAction;
        private readonly Rigidbody2D _rigidbody2D;
        private readonly IOnGroundSettings _onGroundSettings;

        public OnGround(byte priority, Rigidbody2D rigidbody2D, IOnGroundSettings onGroundSettings) 
            : base("OnGround", priority)
        {
            _rigidbody2D = rigidbody2D;
            _onGroundSettings = onGroundSettings;
            
            PlayerMovingHandler handler = new PlayerMovingHandler();
            _walkingAction = handler.GetByType(PlayerMovingActions.Walking);
            _jumpingAction = handler.GetByType(PlayerMovingActions.Jumping);

            InitHandlers();
        }

        public override bool IsTriggered() => _rigidbody2D.velocity.y == 0;

        private void InitHandlers() => _jumpingAction.performed += JumpingHandler;

        public override void EnableState()
        {
            _walkingAction.Enable();
            _jumpingAction.Enable();
                    
            if (_rigidbody2D.velocity.x == 0)
            {
                IdleStarted?.Invoke();
                _isIdle = true;
            }
            else
            {
                WalkingStarted?.Invoke();
                _isIdle = false;
            }
            base.EnableState();
        }

        public override void DisableState()
        {
            _walkingAction.Disable();
            _jumpingAction.Disable();
   
            if (_rigidbody2D.velocity.x == 0)
            {
                IdleStopped?.Invoke();
            }
            else
            {
                WalkingStopped?.Invoke();
            }
            base.DisableState();
        }   

#region Jumping

        private void JumpingHandler(InputAction.CallbackContext context) => Jump();

        private void Jump() => _rigidbody2D.velocity += Vector2.up * _onGroundSettings.JumpForce;

#endregion

#region Walking

        public void FixedUpdate() => TryMove();
        
        private void TryMove()
        {
            if (!_walkingAction.enabled)
            {
                return;
            }
            
            float inputActionValue = _walkingAction.ReadValue<float>();

            CallSubStateActions(inputActionValue);

            if (inputActionValue == 0f && _rigidbody2D.velocity.x != 0)
            {
                ResetVelocity();
                return;
            }

            if (inputActionValue == 0f)
            {
                return;
            }

            if (DirectionChanged(inputActionValue))
            {
                ResetVelocity();
                return;
            }
                    
            Vector2 currentVelocity = _rigidbody2D.velocity;
            float xVelocityAddition = inputActionValue * _onGroundSettings.AccelerationFactor;
            _rigidbody2D.velocity = new Vector2(
                Mathf.Clamp(
                    currentVelocity.x + xVelocityAddition, 
                    -_onGroundSettings.MaxMoveSpeed, 
                    _onGroundSettings.MaxMoveSpeed), 
                currentVelocity.y);
        }

        private void CallSubStateActions(float inputActionValue)
        {
            if (inputActionValue == 0)
            {
                if (!_isIdle)
                {
                    WalkingStopped?.Invoke();
                    IdleStarted?.Invoke();
                    _isIdle = true;
                }
            }
            else
            {
                if (_isIdle)
                {
                    WalkingStarted?.Invoke();
                    IdleStopped?.Invoke();
                    _isIdle = false;
                }
            }
        }

        private bool DirectionChanged(float actionValue) =>
            CustomMath.HaveDifferentSigns(actionValue, _rigidbody2D.velocity.x);
       
        
        private void ResetVelocity() => _rigidbody2D.velocity = new Vector2(0f, _rigidbody2D.velocity.y);

        #endregion
        
    }
}
