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
            _inputHandler.AddHandler(InputActions.Jumping, OnJump);
        }

        void IHandleableByInput.OnDisable()
        {
            _inputHandler.DisableAction(InputActions.Jumping);
            _inputHandler.RemoveHandler(InputActions.Jumping, OnJump);
        }

#endregion


        private void OnJump(InputAction.CallbackContext context)
        {
            if (!OnGround())
            {
                return;
            }
            _rigidbody2D.AddForce(Vector2.up * _jumpingSettings.JumpForce, ForceMode2D.Impulse);
            IsActive = true;
            Jumping();
        }

        
        private bool OnGround()
        {
            Bounds bounds = _collider2D.bounds;
            Vector2 colliderSize = bounds.size;
            Vector3 colliderCenter = bounds.center;

            Vector2 boxCenter = new Vector2(colliderCenter.x, colliderCenter.y - colliderSize.y / 2);
            Vector2 boxSize = new Vector2(
                colliderSize.x * _jumpingSettings.GroundCheckerWidthPercent,
                colliderSize.y * _jumpingSettings.GroundCheckerHeightPercent);

            return Physics2D.OverlapBox(boxCenter, boxSize, 0f, _jumpingSettings.GroundLayerMask.value) != null;
        }
        
        
        private async void Jumping()
        {
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