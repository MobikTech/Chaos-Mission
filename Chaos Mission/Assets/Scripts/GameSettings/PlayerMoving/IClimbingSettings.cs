using UnityEngine;

namespace ChaosMission.Settings.PlayerMoving
{
    public interface IClimbingSettings
    {
        public LayerMask WallLayerMask { get; }
        public LayerMask GroundLayerMask { get; }
        public float FromWallJumpForce { get; }
        public float SlippingFactor { get; }   
        public float Distance { get; }         
        public float GroundCheckerHeightPercent { get; }
        public float GroundCheckerWidthPercent { get; }
    }
}
