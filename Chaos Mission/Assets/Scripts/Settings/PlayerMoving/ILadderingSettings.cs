using UnityEngine;

namespace ChaosMission.Settings.PlayerMoving
{
    public interface ILadderingSettings
    {
        public LayerMask LadderLayerMask { get; }
        public float LadderDistance { get; }
        float LadderAccelerationFactor { get; }
        float MAXLadderSpeed { get; }
        
        float LadderFallingGravity { get; }
    }
}