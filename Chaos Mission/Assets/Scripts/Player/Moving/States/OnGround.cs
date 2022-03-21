#nullable enable
using System;
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

        private readonly PlayerMovingHandler _inputHandler;
        private readonly Rigidbody2D _rigidbody2D;
        private readonly IOnGroundSettings _onGroundSettings;

        public OnGround(byte priority, Rigidbody2D rigidbody2D, IOnGroundSettings onGroundSettings)
        {
            Priority = priority;
            _inputHandler = new PlayerMovingHandler();
            _rigidbody2D = rigidbody2D;
            _onGroundSettings = onGroundSettings;

            InitHandlers();
        }

        public bool IsTriggered() => _rigidbody2D.velocity.y == 0;

        private void InitHandlers() => _inputHandler.AddHandler(PlayerMovingActions.Jumping, JumpingHandler);

        public void EnableState()
        {
            _inputHandler.DisableAllActions();
            _inputHandler.EnableAction(PlayerMovingActions.Walking);
            _inputHandler.EnableAction(PlayerMovingActions.Jumping);
                    
            StateStarted?.Invoke();
        }

        public void DisableState()
        {
            _inputHandler.DisableAction(PlayerMovingActions.Walking);
            _inputHandler.DisableAction(PlayerMovingActions.Jumping);
   
            StateStopped?.Invoke();
            // ResetVelocity();
        }   

#region Jumping

        private void JumpingHandler(InputAction.CallbackContext context) => Jump();

        private void Jump() => _rigidbody2D.velocity += Vector2.up * _onGroundSettings.JumpForce;

#endregion

#region Walking

        public void FixedUpdate() => TryMove();

        private void TryMove()
        {
            float actionValue = _inputHandler.ReadCurrentValue<float>(PlayerMovingActions.Walking);
                    
            if (actionValue == 0)
            {
                if (_rigidbody2D.velocity != Vector2.zero 
                    && _inputHandler.GetByType(PlayerMovingActions.Walking).enabled)
                {
                    // ResetVelocity();
                }
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

        private void ResetVelocity() => _rigidbody2D.velocity = new Vector2(0f, _rigidbody2D.velocity.y);

#endregion
        
    }
}