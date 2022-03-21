using UnityEngine;

namespace ChaosMission.GameSettings.PlayerMoving
{
    [CreateAssetMenu(fileName = "MovingSettings", menuName = "CustomSettings/MovingSettings", order = 0)]
    public sealed class MovingSettings : ScriptableObject, IOnGroundSettings, IOnAirSettings, IOnLadderSettings
    {
        [Header("OnGround")]
        [SerializeField] private float _maxMoveSpeed = 6f;
        [SerializeField, Range(0, 1)] private float _accelerationFactor = 0.3f;
        [SerializeField] private float _jumpForce = 13f;

        
        [Header("OnAir")]
        [SerializeField] private float _maxSteamSpeed = 6f;
        [SerializeField, Range(0, 1)] private float _steamAccelerationFactor = 0.3f;

        [Header("OnLadder")]
        [SerializeField] private LayerMask _ladderLayerMask;
        [SerializeField] private float _ladderMaxSpeed = 0.02f;
        
        
        public float MaxMoveSpeed => _maxMoveSpeed;
        public float AccelerationFactor => _accelerationFactor;
        public float JumpForce => _jumpForce;
        
        
        public float MaxSteamSpeed => _maxSteamSpeed;
        public float SteamAccelerationFactor => _steamAccelerationFactor;


        public LayerMask LadderLayerMask => _ladderLayerMask;
        public float MaxLadderSpeed => _ladderMaxSpeed;

    }
}