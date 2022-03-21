using System;
using ChaosMission.Input.ActionsMaps;
using UnityEngine.InputSystem;

namespace ChaosMission.Input.Handlers
{
    public class PlayerHandler : ActionsMapHandler<PlayerActions>
    {
        // public PlayerHandler(InputCollection inputCollection) : base(inputCollection) { }
        
        public override InputAction GetByType(PlayerActions actionType)
        {
            return actionType switch
            {
                PlayerActions.Shooting => s_inputCollection.Player.Shooting,
                _ => throw new ArgumentOutOfRangeException(nameof(actionType), actionType, null)
            };
        }
    }
}