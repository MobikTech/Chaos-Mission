using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ChaosMission.Input
{
    public enum InputActions
    {
        Moving,
        Jumping,
        Shooting,
        Climbing
    }
    
    public sealed class InputHandler : MonoBehaviour
    {
        private InputCollection _inputCollection;

        private void Awake() => _inputCollection = new InputCollection();

        private InputAction GetByType(InputActions action)
        {
            return action switch
            {
                InputActions.Moving => _inputCollection.Player.Moving,
                InputActions.Jumping => _inputCollection.Player.Jumping,
                InputActions.Shooting => _inputCollection.Player.Shooting,
                InputActions.Climbing => _inputCollection.Player.Climbing,
                _ => throw new ArgumentOutOfRangeException(nameof(action), action, null)
            };
        }
        
        public void EnableAction(InputActions action) => GetByType(action).Enable();
        
        public void DisableAction(InputActions action) => GetByType(action).Disable();

        public void AddHandler(InputActions action, Action<InputAction.CallbackContext> handler)
        {
            GetByType(action).performed += handler;
            // GetByType(action).triggered
        }

        public void RemoveHandler(InputActions action, Action<InputAction.CallbackContext> handler) =>
            GetByType(action).performed -= handler;
    }
}