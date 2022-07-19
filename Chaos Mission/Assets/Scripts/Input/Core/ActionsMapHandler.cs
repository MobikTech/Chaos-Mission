using System;
using UnityEngine.InputSystem;

namespace ChaosMission.Input.Core
{
    public abstract class ActionsMapHandler<TActionMap, TInputCollection> : IActionsMapHandler<TActionMap>
        where TActionMap : Enum
        where TInputCollection : IInputActionCollection, new()
    {
        protected static readonly TInputCollection s_inputCollection;

        static ActionsMapHandler() => s_inputCollection = new TInputCollection();

        //todo think about adding DI mechanism for receiving singleton InputCollection 
        // protected ActionsMapHandler(InputCollection inputCollection) => _inputCollection = inputCollection;

        public abstract InputAction GetByType(TActionMap actionType);

       
        public void DisableAllActions()
        {
            foreach (TActionMap actionType in Enum.GetValues(typeof(TActionMap)))
            {
                GetByType(actionType).Disable();
            }
        }
    }
}