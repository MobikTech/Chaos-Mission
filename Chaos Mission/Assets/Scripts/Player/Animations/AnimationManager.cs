using UnityEngine;

namespace ChaosMission.Player.Animations
{
    [RequireComponent(typeof(Animator), typeof(MoveController))]
    public class AnimationManager : MonoBehaviour
    {
        private Animator _animator;
        private MoveController _moveController;
        
        private static readonly int IsRunning = Animator.StringToHash("IsRunning");

        private void Start()
        {
            _moveController = GetComponent<MoveController>();
            _animator = GetComponent<Animator>();
            AddHandlers();
        }

        private void AddHandlers()
        {
            _moveController.MovingStates[MovingState.Walking].StateStartedAction += OnRunningStarted;
            _moveController.MovingStates[MovingState.Walking].StateStoppedAction += OnRunningStopped; 
        }

        private void OnRunningStarted() => _animator.SetBool(IsRunning, true);

        private void OnRunningStopped() => _animator.SetBool(IsRunning, false);
    }
}
