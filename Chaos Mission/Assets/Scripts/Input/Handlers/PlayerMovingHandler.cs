using System;
using ChaosMission.Input.ActionsMaps;
using ChaosMission.Input.Core;
using UnityEngine.InputSystem;

namespace ChaosMission.Input.Handlers
{
    public sealed class PlayerMovingHandler : ActionsMapHandler<PlayerMovingActions, InputCollection>
    {
        public override InputAction GetByType(PlayerMovingActions actionType)
        {
            return actionType switch
            {
                PlayerMovingActions.Walking => s_inputCollection.PlayerMoving.Walking,
                PlayerMovingActions.Jumping => s_inputCollection.PlayerMoving.Jumping,
                PlayerMovingActions.AirMoving => s_inputCollection.PlayerMoving.AirMoving,
                PlayerMovingActions.Laddering => s_inputCollection.PlayerMoving.Laddering,
                _ => throw new ArgumentOutOfRangeException(nameof(actionType), actionType, null)
            };
        }

    }
}