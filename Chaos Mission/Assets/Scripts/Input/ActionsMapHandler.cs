using System;
using ChaosMission.Input.ActionsMaps;
using UnityEngine.InputSystem;

namespace ChaosMission.Input
{
    public abstract class ActionsMapHandler<TActions> : IActionsMapHandler<TActions>
        where TActions : Enum
    {
        protected static readonly InputCollection s_inputCollection;

        static ActionsMapHandler() => s_inputCollection = new InputCollection();

        //todo add DI mechanism for receiving singleton InputCollection 
        // protected ActionsMapHandler(InputCollection inputCollection) => _inputCollection = inputCollection;

        public abstract InputAction GetByType(TActions actionType);

       
        public void DisableAllActions()
        {
            foreach (TActions actionType in Enum.GetValues(typeof(TActions)))
            {
                GetByType(actionType).Disable();
            }
        }
        
        // public TValue ReadCurrentValue<TValue>(TActions actionType) 
        //     where TValue : struct =>
        //     GetByType(actionType).ReadValue<TValue>();
        //
        //
        // public void AddHandler(TActions actionType, Action<InputAction.CallbackContext> handler) =>
        //     GetByType(actionType).performed += handler;
        //
        // public void RemoveHandler(TActions actionType, Action<InputAction.CallbackContext> handler) =>
        //     GetByType(actionType).performed -= handler;
    }
}