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
    public sealed class OnLadder : IMovingState, IFixedUpdatable
    {
        public string Name => "OnLadder";
        public byte Priority { get; }
        public Action? StateStarted { get; set; }
        public Action? StateStopped { get; set; }
        
        private readonly InputAction _ladderingAction;
        private readonly Rigidbody2D _rigidbody2D;
        private readonly IOnLadderSettings _ladderingSettings;
        private readonly Collider2D _collider2D;

        private float _originalGravity;

        public OnLadder(byte priority, Rigidbody2D rigidbody2D, IOnLadderSettings ladderingSettings,
            Collider2D collider2D)
        {
            Priority = priority;
            _ladderingAction = new PlayerMovingHandler().GetByType(PlayerMovingActions.Laddering);
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
            // _inputHandler.DisableAllActions();
            _ladderingAction.Enable();
            StateStarted?.Invoke();
        }

        public void DisableState()
        {
            _ladderingAction.Disable();
            StateStopped?.Invoke();
        }

        private bool TouchesLadder() => _collider2D.IsTouchingLayers(_ladderingSettings.LadderLayerMask.value);

        public void FixedUpdate() => ToLadder();

        private void ToLadder()
        {
            Vector2 actionValue =  _ladderingAction.ReadValue<Vector2>();
            
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