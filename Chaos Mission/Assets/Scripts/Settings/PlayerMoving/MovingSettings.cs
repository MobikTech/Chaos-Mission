using System.Collections.Generic;
using UnityEngine;

namespace ChaosMission.Settings.PlayerMoving
{
    [CreateAssetMenu(fileName = "MovingSettings", menuName = "CustomSettings/MovingSettings", order = 0)]
    public sealed class MovingSettings : ScriptableObject, IWalkingSettings, IJumpingSettings, IClimbingSettings, ILadderingSettings
    {
        [Header("Moving")]
        [SerializeField] private float _maxMoveSpeed = 6f;
        [SerializeField, Range(0, 1)] private float _accelerationFactor = 0.3f;
        
        [Header("Jumping")]
        [SerializeField] private float _jumpForce = 13f;
        [SerializeField] private LayerMask _groundLayerMask;
        [SerializeField] private float _groundCheckerHeightPercent = 0.1f;
        [SerializeField] private float _groundCheckerWidthPercent = 0.9f;
        
        [Header("Climbing")]
        [SerializeField] private LayerMask _wallLayerMask;
        [SerializeField] private float _fromWallJumpForce = 13f;
        [SerializeField] private float _slippingFactor = 1.2f;
        [SerializeField] private float _distance = 0.02f;
        
        [Header("Laddering")]
        [SerializeField] private LayerMask _ladderLayerMask;
        [SerializeField] private float _ladderDistance = 13f;
        [SerializeField] private float _ladderAccelerationFactor= 1.2f;
        [SerializeField] private float _ladderMaxSpeed = 0.02f;
        [SerializeField] private float _ladderFallingGravity = 2f;
        
        
        public float MAXMoveSpeed => _maxMoveSpeed;
        public float AccelerationFactor => _accelerationFactor;

        
        public LayerMask GroundLayerMask => _groundLayerMask;
        public float JumpForce => _jumpForce;
        public float GroundCheckerHeightPercent => _groundCheckerHeightPercent;
        public float GroundCheckerWidthPercent => _groundCheckerWidthPercent;

        
        public LayerMask WallLayerMask => _wallLayerMask;
        public float FromWallJumpForce => _fromWallJumpForce;
        public float SlippingFactor => _slippingFactor;
        public float Distance => _distance;

        public LayerMask LadderLayerMask => _ladderLayerMask;
        public float LadderDistance => _ladderDistance;
        public float LadderAccelerationFactor => _ladderAccelerationFactor;
        public float MAXLadderSpeed => _ladderMaxSpeed;

        public float LadderFallingGravity => _ladderDistance;
    }
}