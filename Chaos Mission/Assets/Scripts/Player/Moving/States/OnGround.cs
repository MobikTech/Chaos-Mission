#nullable enable
using System;
using ChaosMission.Common;
using ChaosMission.GameSettings.PlayerMoving;
using ChaosMission.Input.ActionsMaps;
using ChaosMission.Input.Handlers;
using ChaosMission.Player.Moving.Behaviours;
using UnityEngine;
using UnityEngine.InputSystem;


namespace ChaosMission.Player.Moving.States
{
    public class OnGround : IMovingState, IFixedUpdatable
    {
        public string Name => "OnGround";
        public byte Priority { get; }
        public Action? StateStarted { get; set; }
        public Action? StateStopped { get; set; }

        public bool IsIdle { get; private set; } = true;

        private readonly InputAction _walkingAction;
        private readonly InputAction _jumpingAction;
        private readonly Rigidbody2D _rigidbody2D;
        private readonly IOnGroundSettings _onGroundSettings;

        public OnGround(byte priority, Rigidbody2D rigidbody2D, IOnGroundSettings onGroundSettings)
        {
            Priority = priority;
            _rigidbody2D = rigidbody2D;
            _onGroundSettings = onGroundSettings;
            
            PlayerMovingHandler handler = new PlayerMovingHandler();
            _walkingAction = handler.GetByType(PlayerMovingActions.Walking);
            _jumpingAction = handler.GetByType(PlayerMovingActions.Jumping);

            InitHandlers();
        }

        public bool IsTriggered() => _rigidbody2D.velocity.y == 0;

        private void InitHandlers() => _jumpingAction.performed += JumpingHandler;

        public void EnableState()
        {
            _walkingAction.Enable();
            _jumpingAction.Enable();
                    
            StateStarted?.Invoke();
        }

        public void DisableState()
        {
            _walkingAction.Disable();
            _jumpingAction.Disable();
   
            StateStopped?.Invoke();
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
            
            float actionValue = _walkingAction.ReadValue<float>();

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
            float xVelocityAddition = actionValue * _onGroundSettings.AccelerationFactor;
            _rigidbody2D.velocity = new Vector2(
                Mathf.Clamp(
                    currentVelocity.x + xVelocityAddition, 
                    -_onGroundSettings.MaxMoveSpeed, 
                    _onGroundSettings.MaxMoveSpeed), 
                currentVelocity.y);
        }

        private bool DirectionChanged(float actionValue) =>
            CustomMath.HaveDifferentSigns(actionValue, _rigidbody2D.velocity.x);
       
        
        private void ResetVelocity() => _rigidbody2D.velocity = new Vector2(0f, _rigidbody2D.velocity.y);

        #endregion
        
    }
}
