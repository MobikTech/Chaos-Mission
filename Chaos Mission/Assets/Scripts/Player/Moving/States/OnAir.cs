#nullable enable
using System;
using ChaosMission.GameSettings.PlayerMoving;
using ChaosMission.Input.ActionsMaps;
using ChaosMission.Input.Handlers;
using ChaosMission.Player.Moving.Behaviours;
using UnityEngine;

namespace ChaosMission.Player.Moving.States
{
    public sealed class OnAir : IMovingState, IFixedUpdatable
    {
        public string Name => "OnAir";
        public byte Priority { get; }
        public Action? StateStarted { get; set; }
        public Action? StateStopped { get; set; }

        private readonly PlayerMovingHandler _inputHandler;
        private readonly Rigidbody2D _rigidbody2D;
        private readonly IOnAirSettings _onGroundSettings;

        public OnAir(byte priority, Rigidbody2D rigidbody2D, IOnAirSettings onAirSettings)
        {
            Priority = priority;
            _inputHandler = new PlayerMovingHandler();
            _rigidbody2D = rigidbody2D;
            _onGroundSettings = onAirSettings;
        }

        public bool IsTriggered() => _rigidbody2D.velocity.y != 0;

        public void EnableState()
        {
            _inputHandler.DisableAllActions();
            _inputHandler.EnableAction(PlayerMovingActions.AirMoving);
            StateStarted?.Invoke();
        }

        public void DisableState()
        {
            _inputHandler.DisableAction(PlayerMovingActions.AirMoving);
            StateStopped?.Invoke();
            // ResetVelocity();
        }
     
        public void FixedUpdate() => TryMove();

        private void TryMove()
        {
            float actionValue = _inputHandler.ReadCurrentValue<float>(PlayerMovingActions.AirMoving);

            if (actionValue == 0)
            {
                if (_rigidbody2D.velocity != Vector2.zero
                    && _inputHandler.IsEnabled(PlayerMovingActions.AirMoving))
                {
                    // ResetVelocity();
                }
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

        private void ResetVelocity() => _rigidbody2D.velocity = new Vector2(0f, _rigidbody2D.velocity.y);
    }
}