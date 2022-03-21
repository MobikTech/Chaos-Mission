// using System;
// using System.Threading;
// using System.Threading.Tasks;
// using ChaosMission.Extensions.UnityExtensions;
// using ChaosMission.GameSettings.PlayerMoving;
// using ChaosMission.Input;
// using ChaosMission.Player.Moving.Behaviours;
// using UnityEngine;
// using UnityEngine.InputSystem;
//
// namespace ChaosMission.Player.MovingStates
// {
//     public class Ladderable : IMovingState, IHandleableByInput
//     {
//         private readonly Transform _transform;
//         private readonly InputHandler _inputHandler;
//         private readonly Rigidbody2D _rigidbody2D;
//         private readonly Collider2D _collider2D;
//         private readonly ILadderingSettings _ladderingSettings;
//         private readonly CancellationTokenSource _cancellationTokenSource;
//         public Action StateStartedAction { get; set; }
//         public Action StateStoppedAction { get; set; }
//         public bool IsActive { get; private set; } = false;
//
//         public Ladderable(Transform transform, InputHandler inputHandler, Rigidbody2D rigidbody2D, 
//             Collider2D collider2D, ILadderingSettings ladderingSettings)
//         {
//             _transform = transform;
//             _inputHandler = inputHandler;
//             _rigidbody2D = rigidbody2D;
//             _collider2D = collider2D;
//             _ladderingSettings = ladderingSettings;
//             
//             _cancellationTokenSource = new CancellationTokenSource();
//             Update(_cancellationTokenSource.Token);
//         }
//         
//         private async void Update(CancellationToken cancellationToken)
//         {
//             while (cancellationToken.IsCancellationRequested == false)
//             {
//                 if (!IsActive && TouchesLadder())
//                 {
//                     EnableAction();
//                     await Task.Delay(500, cancellationToken);
//                 }
//                 await Task.Yield();
//             }
//         }
//         
//         #region IHandleableByInputMethods
//         
//         void IHandleableByInput.OnDestroy() => ((IHandleableByInput) this).OnDisableState();
//
//         void IHandleableByInput.OnEnableState()
//         {
//             _inputHandler.AddHandler(InputActions.Laddering, OnLaddering);
//         }
//
//         void IHandleableByInput.OnDisableState()
//         {
//             _inputHandler.RemoveHandler(InputActions.Laddering, OnLaddering);
//         }
//
//         #endregion
//
//         private void OnLaddering(InputAction.CallbackContext context) => LadderAsync(context);
//         
//         private bool TouchesLadder() => _collider2D.IsTouchingLayers(_ladderingSettings.LadderLayerMask.value);
//
//         private void EnableAction()
//         {
//             _inputHandler.EnableAction(InputActions.Laddering);
//         }
//         
//         private void DisableAction()
//         {
//             _inputHandler.DisableAction(InputActions.Laddering);
//         }
//
//         private async void LadderAsync(InputAction.CallbackContext context)
//         {
//             IsActive = true;
//             StateStartedAction?.Invoke();
//             
//             TryFlip(context.ReadValue<Vector2>().x);
//
//             while (context.phase == InputActionPhase.Performed && TouchesLadder())
//             {
//                 
//                 Vector2 currentVelocity = _rigidbody2D.velocity;
//                 Vector2 velocityAddition = context.ReadValue<Vector2>() * _ladderingSettings.LadderAccelerationFactor;
//                 _rigidbody2D.velocity = new Vector2(
//                     Mathf.Clamp(
//                         currentVelocity.x + velocityAddition.x, 
//                         -_ladderingSettings.MAXLadderSpeed, 
//                         _ladderingSettings.MAXLadderSpeed), 
//                     Mathf.Clamp(currentVelocity.y + velocityAddition.y, 
//                         -_ladderingSettings.MAXLadderSpeed, 
//                         _ladderingSettings.MAXLadderSpeed
//                         ));
//
//                 await Task.Delay(UnityTimeHelper.GetMillisecondsToNextFixedUpdate());
//             }
//
//             _rigidbody2D.velocity = new Vector2(0f, 0f);
//             StateStoppedAction?.Invoke();
//             IsActive = false;
//             DisableAction();
//         }
//         
//         private void TryFlip(float keyAxisValue)
//         {
//             if (keyAxisValue == 0)
//             {
//                 return;
//             }
//         
//             Vector3 triggeredDirection = keyAxisValue < 0 ? Vector3.left : Vector3.right;
//             _transform.TryFlip(Axes.X, triggeredDirection);
//         }
//         
//     }
// }
