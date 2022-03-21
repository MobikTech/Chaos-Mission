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
//     public class Climbable : IMovingState, IHandleableByInput, IDisposable
//     {
//         private readonly InputHandler _inputHandler;
//         private readonly Rigidbody2D _rigidbody2D;
//         private readonly Collider2D _collider2D;
//         private readonly IClimbingSettings _climbingSettings;
//         
//         private readonly float _originalGravityScale;
//         private readonly CancellationTokenSource _cancellationTokenSource;
//         private bool _jumping;
//
//         public Action StateStartedAction { get; set; }
//         public Action StateStoppedAction { get; set; }
//         public bool IsActive { get; private set; }
//
//         public Climbable(InputHandler inputHandler, Rigidbody2D rigidbody2D, Collider2D collider2D,
//             IClimbingSettings climbingSettings)
//         {
//             _inputHandler = inputHandler;
//             _rigidbody2D = rigidbody2D;
//             _collider2D = collider2D;
//             _climbingSettings = climbingSettings;
//
//             _originalGravityScale = _rigidbody2D.gravityScale;
//
//             _cancellationTokenSource = new CancellationTokenSource();
//             Update(_cancellationTokenSource.Token);
//
//         }
//
//         private async void Update(CancellationToken cancellationToken)
//         {
//             while (cancellationToken.IsCancellationRequested == false)
//             {
//                 if (!IsActive && IsVerticalMoving() && TouchesWall())
//                 {
//                     ClimbAsync();
//                     await Task.Delay(500, cancellationToken);
//                 }
//                 await Task.Yield();
//             }
//         }
//         
// #region IHandleableByInputMethods
//         
//         void IHandleableByInput.OnDestroy() => ((IHandleableByInput) this).OnDisableState();
//
//         void IHandleableByInput.OnEnableState()
//         {
//             _inputHandler.AddHandler(InputActions.ClimbingJump, OnClimbingJumpHandler);
//             _inputHandler.AddHandler(InputActions.ClimbingDownhill, OnClimbingDownhillHandler);
//         }
//
//         void IHandleableByInput.OnDisableState()
//         {
//             _inputHandler.RemoveHandler(InputActions.ClimbingJump, OnClimbingJumpHandler);
//             _inputHandler.RemoveHandler(InputActions.ClimbingDownhill, OnClimbingDownhillHandler);
//         }
//
// #endregion
//
//         private void OnClimbingJumpHandler(InputAction.CallbackContext context)
//         {
//             IsActive = false;
//             // Debug.Log("JUMPPPPPPPPPPPPPPPPPPPPPPPPP");
//             Vector2 forceDirection = (Vector2.up + (Vector2)_collider2D.transform.right).normalized;
//             _rigidbody2D.velocity = forceDirection * _climbingSettings.FromWallJumpForce;
//         }
//
//         private void OnClimbingDownhillHandler(InputAction.CallbackContext context)
//         {
//             
//         }
//
//         private bool OnGround() =>
//             _collider2D.OverlapBoxOnDown(
//                 _climbingSettings.GroundLayerMask,
//                 _climbingSettings.GroundCheckerHeightPercent,
//                 _climbingSettings.GroundCheckerWidthPercent);
//         
//         private bool TouchesWall() => _collider2D.RaycastFromCenterToRight(
//             _climbingSettings.WallLayerMask, 
//             _climbingSettings.Distance);
//         
//         private bool IsVerticalMoving() => _rigidbody2D.velocity.y != 0f;
//
//         
//         
//
//         private async void ClimbAsync()
//         {
//             // Debug.Log("start");
//             IsActive = true;
//             StateStartedAction?.Invoke();
//             _inputHandler.EnableAction(InputActions.ClimbingJump);
//             _inputHandler.EnableAction(InputActions.ClimbingDownhill);
//
//             _rigidbody2D.gravityScale = 0f;
//             _rigidbody2D.velocity = Vector2.down * _climbingSettings.SlippingFactor;
//             
//             while (IsActive && TouchesWall() && !OnGround())
//             {
//                 // Debug.Log("climbing");
//                 // Debug.Log(IsActive);
//                 await Task.Delay(UnityTimeHelper.GetMillisecondsToNextFixedUpdate());
//             }
//
//             _rigidbody2D.gravityScale = _originalGravityScale;
//             
//             _inputHandler.DisableAction(InputActions.ClimbingJump);
//             _inputHandler.DisableAction(InputActions.ClimbingDownhill);
//             StateStoppedAction?.Invoke();
//             IsActive = false;
//         }
//
//         
//         public void Dispose() => _cancellationTokenSource.Cancel();
//
//     }
// }