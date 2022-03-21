using System;
using ChaosMission.Input.ActionsMaps;
using ChaosMission.Input.Core;
using UnityEngine.InputSystem;

namespace ChaosMission.Input.Handlers
{
    public class PlayerHandler : ActionsMapHandler<PlayerActions, InputCollection>
    {
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