using System;
using UnityEngine.InputSystem;

namespace ChaosMission.Input
{
    public interface IActionsMapHandler<in TActions> where TActions : Enum
    {
        public InputAction GetByType(TActions actionType); 
        public void DisableAllActions();
    }
}