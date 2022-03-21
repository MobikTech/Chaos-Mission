using UnityEngine;

namespace ChaosMission.GameSettings.PlayerMoving
{
    public interface IOnGroundSettings
    {
        public float MaxMoveSpeed { get; }
        public float AccelerationFactor { get; }
        public float JumpForce { get; }
    }
}
