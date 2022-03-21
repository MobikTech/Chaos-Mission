#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ChaosMission.GameSettings.PlayerMoving;
using ChaosMission.Player.Moving.Behaviours;
using ChaosMission.Player.Moving.States;
using UnityEngine;

namespace ChaosMission.Player.Moving.Controller
{
    [RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
    public sealed class MoveController : MonoBehaviour, ICoroutineActivator, IStateApplier
    {
        [SerializeField] private MovingSettings? _movingSettings;
        private Rigidbody2D _rigidbody2D = null!;
        private Collider2D _collider2D = null!;

        private IMovingState? _currentState;
        public Dictionary<MovingStateType, IMovingState> MovingStates { get; private set; } = null!;
        private List<IMovingState> _statesByPriorityAscending = null!;
        private IFixedUpdatable? _currentFixedUpdatable;
        private IUpdatable? _currentUpdatable;
        
        private void Awake()
        {
            _collider2D = GetComponent<Collider2D>();
            _rigidbody2D = GetComponent<Rigidbody2D>();

            InitStates();

            ApplyState(MovingStates[MovingStateType.OnGround]);
        }

        private void Update() => _currentUpdatable?.Update();

        private void FixedUpdate()
        {
            _currentFixedUpdatable?.FixedUpdate();
            TryChangeState();
        }
        
        private void InitStates()
        {
            IMovingState onGroundState = new OnGround(
                3,
                _rigidbody2D,
                _movingSettings!);
            
            IMovingState onAirState = new OnAir(
                2,
                _rigidbody2D,
                _movingSettings!);
            
            IMovingState onLadderState = new OnLadder(
                1,
                _rigidbody2D,
                _movingSettings!,
                _collider2D);
            
            MovingStates = new Dictionary<MovingStateType, IMovingState>
            {
                {MovingStateType.OnGround, onGroundState},
                {MovingStateType.OnAir, onAirState},
                {MovingStateType.OnLadder, onLadderState}
            };

            _statesByPriorityAscending = MovingStates.Values
                .OrderBy(state => state.Priority)
                .ToList();
        }

        private void TryChangeState()
        {
            foreach (IMovingState state in _statesByPriorityAscending)
            {
                if (state.IsTriggered())
                {
                    if (_currentState != state)
                    {
                        ApplyState(state);
                        // Debug.Log($"State - {state.Name}");
                    }
                    break;
                }
            }
        }

        public void ActivateCoroutine(Func<IEnumerator> coroutine) => StartCoroutine(coroutine());
        
        public void ActivateCoroutine<TParam>(Func<TParam, IEnumerator> coroutine, TParam paramValue) =>
            StartCoroutine(coroutine(paramValue));

       

        public void ApplyState(IMovingState state)
        {
            _currentState?.DisableState();
            _currentState = state;
            _currentFixedUpdatable = _currentState as IFixedUpdatable;
            _currentUpdatable = _currentState as IUpdatable;
            _currentState.EnableState();
        }

        private void OnDisable() => _currentState?.DisableState();

    }

}