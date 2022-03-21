using System;
using UnityEngine.InputSystem;

namespace ChaosMission.Input
{
    public interface IActionsMapHandler<in TActions> where TActions : Enum
    {
        public InputAction GetByType(TActions actionType); 
        public bool IsEnabled(TActions actionType);
        public void EnableAction(TActions actionType);
        
        public void DisableAction(TActions actionType);

        public void DisableAllActions();
        // public bool IsCurrentlyTriggered(TActions actionType);

        public void AddHandler(TActions actionType, Action<InputAction.CallbackContext> handler);

        public void RemoveHandler(TActions actionType, Action<InputAction.CallbackContext> handler);
    }
}