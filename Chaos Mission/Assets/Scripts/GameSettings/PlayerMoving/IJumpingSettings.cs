using UnityEngine;

namespace ChaosMission.Settings.PlayerMoving
{
    public interface IJumpingSettings
    {
        public LayerMask GroundLayerMask { get; }
        public LayerMask WallLayerMask { get; }
        public float JumpForce { get; }
        public float Distance { get; }         
        public float GroundCheckerHeightPercent { get; }
        public float GroundCheckerWidthPercent { get; }

    }
}
