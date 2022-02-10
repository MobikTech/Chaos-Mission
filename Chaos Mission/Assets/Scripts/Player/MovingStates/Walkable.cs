using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChaosMission.Extensions.UnityExtensions;
using ChaosMission.Input;
using ChaosMission.Settings.PlayerMoving;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ChaosMission.Player.MovingStates
{
    public class Walkable : IMovingState, IHandleableByInput
    {
        private readonly Transform _transform;
        private readonly InputHandler _inputHandler;
        private readonly Rigidbody2D _rigidbody2D;
        private readonly IWalkingSettings _walkingSettings;

        public Action StateStartedAction { get; set; }
        public Action StateStoppedAction { get; set; }
        public bool IsActive { get; private set; } = false;

        public Walkable(Transform transform, InputHandler inputHandler, Rigidbody2D rigidbody2D,
            IWalkingSettings walkingSettings)
        {
            _transform = transform;
            _walkingSettings = walkingSettings;
            _rigidbody2D = rigidbody2D;
            _inputHandler = inputHandler;
        }

#region IHandleableByInputMethods
        
        void IHandleableByInput.OnDestroy() => ((IHandleableByInput) this).OnDisable();

        void IHandleableByInput.OnEnable()
        {
      
            _inputHandler.EnableAction(InputActions.Moving);
            _inputHandler.AddHandler(InputActions.Moving, OnWalking);
        }

        void IHandleableByInput.OnDisable()
        {
            _inputHandler.DisableAction(InputActions.Moving);
            _inputHandler.RemoveHandler(InputActions.Moving, OnWalking);
        }

#endregion

        private void OnWalking(InputAction.CallbackContext context) => WalkAsync(context);
      
        private async void WalkAsync(InputAction.CallbackContext context)
        {
            IsActive = true;
            TryFlip(context.ReadValue<float>());
            
            StateStartedAction?.Invoke();
            while (context.phase == InputActionPhase.Performed)
            {
                Vector2 currentVelocity = _rigidbody2D.velocity;
                float xVelocityAddition = context.ReadValue<float>() * _walkingSettings.AccelerationFactor;
                _rigidbody2D.velocity = new Vector2(
                    Mathf.Clamp(
                        currentVelocity.x + xVelocityAddition, 
                        -_walkingSettings.MAXMoveSpeed, 
                        _walkingSettings.MAXMoveSpeed), 
                    currentVelocity.y);

                await Task.Delay(UnityTimeHelper.GetMillisecondsToNextFixedUpdate());
            }
            
            _rigidbody2D.velocity = new Vector2(0f, _rigidbody2D.velocity.y);
            StateStoppedAction?.Invoke();
            IsActive = false;
        }
        
        private void TryFlip(float keyAxisValue)
        {
            if (keyAxisValue == 0)
            {
                return;
            }
        
            Vector3 triggeredDirection = keyAxisValue < 0 ? Vector3.left : Vector3.right;
            _transform.TryFlip(Axes.X, triggeredDirection);
        }
        
    }
}