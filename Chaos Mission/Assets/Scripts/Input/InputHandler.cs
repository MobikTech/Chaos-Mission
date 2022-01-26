using System;
using UnityEngine.InputSystem;

namespace ChaosMission.Input
{
    public enum InputActions
    {
        Moving,
        Jumping,
        Shooting,
        ClimbingJump,
        ClimbingDownhill,
    }
    
    public sealed class InputHandler
    {
        private readonly InputCollection _inputCollection;

        public InputHandler()
        {
            _inputCollection = new InputCollection();
        }

        private InputAction GetByType(InputActions action)
        {
            return action switch
            {
                InputActions.Moving => _inputCollection.Player.Moving,
                InputActions.Jumping => _inputCollection.Player.Jumping,
                InputActions.Shooting => _inputCollection.Player.Shooting,
                InputActions.ClimbingJump => _inputCollection.Player.ClimbingJump,
                InputActions.ClimbingDownhill => _inputCollection.Player.ClimbingDownhill,
                _ => throw new ArgumentOutOfRangeException(nameof(action), action, null)
            };
        }
        
        public void EnableAction(InputActions action) => GetByType(action).Enable();
        
        public void DisableAction(InputActions action) => GetByType(action).Disable();

        public void AddHandler(InputActions action, Action<InputAction.CallbackContext> handler) =>
            GetByType(action).performed += handler;

        public void RemoveHandler(InputActions action, Action<InputAction.CallbackContext> handler) =>
            GetByType(action).performed -= handler;
    }
}