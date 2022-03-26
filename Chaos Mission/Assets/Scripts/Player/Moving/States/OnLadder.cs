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
    public sealed class OnLadder : MovingState, IFixedUpdatable, IOnLadderActions
    {
        public Action? LadderingVertStarted { get; set; }
        public Action? LadderingVertStopped { get; set; }
        public Action? LadderingHorStarted { get; set; }
        public Action? LadderingHorStopped { get; set; }
        
        private readonly InputAction _ladderingAction;
        private readonly Rigidbody2D _rigidbody2D;
        private readonly IOnLadderSettings _ladderingSettings;
        private readonly Collider2D _collider2D;

        private float _originalGravity;
        private bool _isVertLaddering = false;
        private bool _isHorLaddering = false;

        public OnLadder(byte priority, Rigidbody2D rigidbody2D, IOnLadderSettings ladderingSettings,
            Collider2D collider2D) : base("OnLadder", priority)
        {
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

        public override bool IsTriggered()
        {
            return TouchesLadder()
            && Keyboard.current.wKey.isPressed;
        }

        public override void EnableState()
        {
            _ladderingAction.Enable();
            base.EnableState();

            if (!CustomMath.EqualsZero(_rigidbody2D.velocity.y))
            {
                LadderingVertStarted?.Invoke();
                _isVertLaddering = true;
                return;
            }

            if (!CustomMath.EqualsZero(_rigidbody2D.velocity.x))
            {
                LadderingHorStarted?.Invoke();
                _isHorLaddering = true;
                return;
            }
        }

        public override void DisableState()
        {
            _ladderingAction.Disable();
            base.DisableState();

            if (_isVertLaddering)
            {
                LadderingVertStopped?.Invoke();
                _isVertLaddering = false;
            }
            else if (_isHorLaddering)
            {
                LadderingHorStopped?.Invoke();
                _isHorLaddering = false;
            }
        }

        private bool TouchesLadder() => _collider2D.IsTouchingLayers(_ladderingSettings.LadderLayerMask.value);

        public void FixedUpdate()
        {
            CallSubStateActions();
            ToLadder();
        }

        private void CallSubStateActions()
        {
            Vector2 actionValue =  _ladderingAction.ReadValue<Vector2>();
            
            if (_isHorLaddering && !CustomMath.EqualsZero(actionValue.y) || !CustomMath.EqualsZero(actionValue.y))
            {
                LadderingHorStopped?.Invoke();
                LadderingVertStarted?.Invoke();
                _isVertLaddering = true;
                _isHorLaddering = false;
            }
            else if (_isVertLaddering && CustomMath.EqualsZero(actionValue.y) && !CustomMath.EqualsZero(actionValue.x))
            {
                LadderingVertStopped?.Invoke();
                LadderingHorStarted?.Invoke();
                _isVertLaddering = false;
                _isHorLaddering = true;
            }
        }
        
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
