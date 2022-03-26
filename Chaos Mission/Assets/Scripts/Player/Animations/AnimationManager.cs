using ChaosMission.Player.Moving;
using ChaosMission.Player.Moving.Controller;
using ChaosMission.Player.Moving.SubStateActions;
using UnityEngine;

namespace ChaosMission.Player.Animations
{
    public class AnimationManager : MonoBehaviour
    {
        [SerializeField] private MoveController _moveController = null!;
        [SerializeField] private Animator _animator = null!;

        private static readonly int s_isWalking = Animator.StringToHash("IsRunning");
        private static readonly int s_isJumping = Animator.StringToHash("Jumping");
        private static readonly int s_isFalling = Animator.StringToHash("Falling");
        private static readonly int s_isLadderingVert = Animator.StringToHash("LadderingVert");
        private static readonly int s_isLadderingHor = Animator.StringToHash("LadderingHor");
        private IOnGroundActions _onGroundActions = null!;
        private IOnAirActions _onAirActions = null!;
        private IOnLadderActions _onLadderActions = null!;

        private void Start()
        {
            _onGroundActions = (IOnGroundActions) _moveController.MovingStates[MovingStateType.OnGround];
            _onAirActions = (IOnAirActions) _moveController.MovingStates[MovingStateType.OnAir];
            _onLadderActions = (IOnLadderActions) _moveController.MovingStates[MovingStateType.OnLadder];
            AddHandlers();
        }

        private void AddHandlers()
        {
            _onGroundActions.WalkingStarted += OnRunningStarted;
            _onGroundActions.WalkingStopped += OnRunningStopped;

            _onAirActions.JumpingStarted += OnJumpingStarted;
            _onAirActions.JumpingStopped += OnJumpingStopped;

            _onAirActions.FallingStarted += OnFallingStarted;
            _onAirActions.FallingStopped += OnFallingStopped;

            _onLadderActions.LadderingVertStarted += OnLadderingVertStarted;
            _onLadderActions.LadderingVertStopped += OnLadderingVertStopped;

            _onLadderActions.LadderingHorStarted += OnLadderingHorStarted;
            _onLadderActions.LadderingHorStopped += OnLadderingHorStopped;
        }

        private void OnRunningStarted() => _animator.SetBool(s_isWalking, true);
        private void OnRunningStopped() => _animator.SetBool(s_isWalking, false);
        
        private void OnJumpingStarted() => _animator.SetBool(s_isJumping, true);
        private void OnJumpingStopped() => _animator.SetBool(s_isJumping, false);
        
        private void OnFallingStarted() => _animator.SetBool(s_isFalling, true);
        private void OnFallingStopped() => _animator.SetBool(s_isFalling, false);
        
        private void OnLadderingVertStarted() => _animator.SetBool(s_isLadderingVert, true);
        private void OnLadderingVertStopped() => _animator.SetBool(s_isLadderingVert, false);
        
        private void OnLadderingHorStarted() => _animator.SetBool(s_isLadderingHor, true);
        private void OnLadderingHorStopped() => _animator.SetBool(s_isLadderingHor, false);
    }
}
