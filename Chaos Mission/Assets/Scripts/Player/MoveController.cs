using System.Collections.Generic;
using System.Linq;
using ChaosMission.Input;
using ChaosMission.Player.MovingStates;
using ChaosMission.Settings.PlayerMoving;
using UnityEngine;

namespace ChaosMission.Player
{
    [RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
    public sealed class MoveController : MonoBehaviour
    {
        [SerializeField] private MovingSettings _movingSettings;
        private InputHandler _inputHandler;
        private Rigidbody2D _rigidbody2D;
        private Collider2D _collider2D;
        
        public Dictionary<MovingState, IMovingState> MovingStates { get; private set; }
        private List<IHandleableByInput> _handleableByInputStatesList;

        private void Awake()
        {
            _collider2D = GetComponent<Collider2D>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _inputHandler = new InputHandler();

            InitStates();
        }

        private void InitStates()
        {
            Walkable walkable = new Walkable(transform, _inputHandler, _rigidbody2D, _movingSettings);
            Jumpable jumpable = new Jumpable(_inputHandler, _rigidbody2D, _collider2D, _movingSettings);
            Climbable climbable = new Climbable(_rigidbody2D, _collider2D, _movingSettings);
            
            MovingStates = new Dictionary<MovingState, IMovingState>
            {
                {MovingState.Walking, walkable},
                {MovingState.Jumping, jumpable},
                {MovingState.Climbing, climbable}
            };

            _handleableByInputStatesList = MovingStates.Values
                .OfType<IHandleableByInput>()
                .ToList();
        }
 

        private void OnEnable() => _handleableByInputStatesList.ForEach(state => state.OnEnable());
        private void OnDisable() =>_handleableByInputStatesList.ForEach(state => state.OnDisable());
        private void OnDestroy() =>_handleableByInputStatesList.ForEach(state => state.OnDestroy());


    }
}