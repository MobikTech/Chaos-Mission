using UnityEngine;

namespace ChaosMission.GameSettings.PlayerMoving
{
    public interface IOnLadderSettings
    {
        public LayerMask LadderLayerMask { get; }
        public float MaxLadderSpeed { get; }
    }
}
