#nullable enable
using System;
using ChaosMission.GameSettings.PlayerMoving;
using ChaosMission.Input.ActionsMaps;
using ChaosMission.Input.Handlers;
using ChaosMission.Player.Moving.Behaviours;
using UnityEngine;

namespace ChaosMission.Player.Moving.States
{
    public sealed class OnLadder : IMovingState, IFixedUpdatable
    {
        public string Name => "OnLadder";
        public byte Priority { get; }
        public Action? StateStarted { get; set; }
        public Action? StateStopped { get; set; }
        
        private readonly PlayerMovingHandler _inputHandler;
        private readonly Rigidbody2D _rigidbody2D;
        private readonly IOnLadderSettings _ladderingSettings;
        private readonly Collider2D _collider2D;

        private float _originalGravity;

        public OnLadder(byte priority, Rigidbody2D rigidbody2D, IOnLadderSettings ladderingSettings,
            Collider2D collider2D)
        {
            Priority = priority;
            _inputHandler = new PlayerMovingHandler();
            _rigidbody2D = rigidbody2D;
            _ladderingSettings = ladderingSettings;
            _collider2D = collider2D;

            InitActions();
        }

        private void InitActions()
        {
            StateStarted += () =>
            {
                _originalGravity = _rigidbody2D.gravityScale;
                _rigidbody2D.gravityScale = 0;
            };

            StateStopped += () =>
            {
                _rigidbody2D.gravityScale = _originalGravity;
            };
        }

        public bool IsTriggered() => TouchesLadder();

        public void EnableState()
        {
            _inputHandler.DisableAllActions();
            _inputHandler.EnableAction(PlayerMovingActions.Laddering);
            StateStarted?.Invoke();
        }

        public void DisableState()
        {
            _inputHandler.DisableAction(PlayerMovingActions.Laddering);
            StateStopped?.Invoke();
        }

        private bool TouchesLadder() => _collider2D.IsTouchingLayers(_ladderingSettings.LadderLayerMask.value);

        public void FixedUpdate() => ToLadder();

        private void ToLadder()
        {
            Vector2 actionValue = _inputHandler.ReadCurrentValue<Vector2>(PlayerMovingActions.Laddering);
            
            if (actionValue == Vector2.zero)
            {
                ResetVelocity();
                return;
            }

            Vector2 velocity = actionValue;
            _rigidbody2D.velocity = new Vector2(
                velocity.x * _ladderingSettings.MaxLadderSpeed,
                velocity.y * _ladderingSettings.MaxLadderSpeed);
        }

        private void ResetVelocity() => _rigidbody2D.velocity = Vector2.zero;

    }
}