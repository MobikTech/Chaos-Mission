using System;
using System.Threading.Tasks;
using ChaosMission.Input;
using ChaosMission.Settings.PlayerMoving;
using ChaosMission.UnityExtensions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ChaosMission.Player.MovingStates
{
    public class Jumpable : IMovingState, IHandleableByInput
    {
        private readonly InputHandler _inputHandler;
        private readonly Rigidbody2D _rigidbody2D;
        private readonly Collider2D _collider2D;
        private readonly IJumpingSettings _jumpingSettings;

        public bool IsActive { get; private set; } = false;

        public Action StateStartedAction { get; set; }
        public Action StateStoppedAction { get; set; }

        public Jumpable(InputHandler inputHandler, Rigidbody2D rigidbody2D, 
            Collider2D collider2D, IJumpingSettings jumpingSettings)
        {
            _inputHandler = inputHandler;
            _rigidbody2D = rigidbody2D;
            _collider2D = collider2D;
            _jumpingSettings = jumpingSettings;
        }
        
        
#region IHandleableByInputMethods
        
        void IHandleableByInput.OnDestroy() => ((IHandleableByInput) this).OnDisable();

        void IHandleableByInput.OnEnable()
        {
            _inputHandler.EnableAction(InputActions.Jumping);
            _inputHandler.AddHandler(InputActions.Jumping, OnJumpingWant);
        }

        void IHandleableByInput.OnDisable()
        {
            _inputHandler.DisableAction(InputActions.Jumping);
            _inputHandler.RemoveHandler(InputActions.Jumping, OnJumpingWant);
        }

#endregion

        private void OnJumpingWant(InputAction.CallbackContext context)
        {
            if (!OnGround())
            {
                return;
            }
            JumpAsync(Vector2.up);
        }
        
        private bool OnGround() =>
            _collider2D.OverlapBoxOnDown(
                _jumpingSettings.GroundLayerMask,
                _jumpingSettings.GroundCheckerHeightPercent,
                _jumpingSettings.GroundCheckerWidthPercent);

        private async void JumpAsync(Vector2 forceDirection)
        {
            _rigidbody2D.velocity += forceDirection * _jumpingSettings.JumpForce;

            IsActive = true;
            StateStartedAction?.Invoke();
            
            while (OnGround())
            {
                await Task.Delay(UnityTimeHelper.GetMillisecondsToNextFixedUpdate());
            }
            while (!OnGround())
            {
                await Task.Delay(UnityTimeHelper.GetMillisecondsToNextFixedUpdate());
            }

            IsActive = false;
            StateStoppedAction?.Invoke();
        }

    }
}