using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ChaosMission.Input
{
    public enum InputActions
    {
        Moving,
        Jumping,
        Shooting
    }
    public sealed class InputHandler : MonoBehaviour
    {
        // private static readonly InputCollection InputCollection;
        private InputCollection InputCollection;

        // static InputHandler()
        // {
        //     InputCollection = new InputCollection();
        // }

        private void Awake()
        {
            InputCollection = new InputCollection();
        }

        private InputAction GetByType(InputActions action)
        {
            return action switch
            {
                InputActions.Moving => InputCollection.Player.Moving,
                InputActions.Jumping => InputCollection.Player.Jumping,
                InputActions.Shooting => InputCollection.Player.Shooting,
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