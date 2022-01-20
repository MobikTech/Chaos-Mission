using System;
using System.Threading.Tasks;
using ChaosMission.Settings.PlayerMoving;
using ChaosMission.UnityExtensions;
using UnityEngine;

namespace ChaosMission.Player.MovingStates
{
    public class Climbable : IMovingState
    {
        private readonly Rigidbody2D _rigidbody2D;
        private readonly Collider2D _collider2D;
        private readonly IClimbingSettings _climbingSettings;
        private readonly float _originalGravityScale;

        public Action StateStartedAction { get; set; }
        public Action StateStoppedAction { get; set; }
        public bool IsActive { get; private set; }

        public Climbable(Rigidbody2D rigidbody2D, Collider2D collider2D, IClimbingSettings climbingSettings)
        {
            _rigidbody2D = rigidbody2D;
            _collider2D = collider2D;
            _climbingSettings = climbingSettings;
            
            _originalGravityScale = _rigidbody2D.gravityScale;
            
            Update();
        }

        private async void Update()
        {
            if (IsVerticalMoving() && TouchesWall())
            {
                Climbing();
            }

            await Task.Yield();
        }

        private bool TouchesWall()
        {
            var bounds = _collider2D.bounds;
            Vector3 colliderCenter = bounds.center;
            Vector2 colliderSize = bounds.size;

            Vector2 direction = _collider2D.transform.right;
            Vector2 origin = new Vector2(colliderCenter.x + direction.x * colliderSize.x / 2, 
                colliderCenter.y);
            
            return Physics2D
                .Raycast(origin, direction, _climbingSettings.Distance, _climbingSettings.WallLayerMask.value)
                .collider != null;
        }

        private async void Climbing()
        {
            IsActive = true;
            StateStartedAction?.Invoke();

            _rigidbody2D.gravityScale = 0f;
            _rigidbody2D.velocity = Vector2.down * _climbingSettings.SlippingFactor;
            
            while (!OnGround())
            {
                await Task.Delay(UnityTimeHelper.GetMillisecondsToNextFixedUpdate());
            }

            _rigidbody2D.gravityScale = _originalGravityScale;
            
            StateStoppedAction?.Invoke();
            IsActive = false;
        }

        private bool IsVerticalMoving() => _rigidbody2D.velocity.y != 0f;
        
        private bool OnGround()
        {
            Bounds bounds = _collider2D.bounds;
            Vector2 colliderSize = bounds.size;
            Vector3 colliderCenter = bounds.center;

            Vector2 boxCenter = new Vector2(colliderCenter.x, colliderCenter.y - colliderSize.y / 2);
            Vector2 boxSize = new Vector2(
                colliderSize.x * _climbingSettings.GroundCheckerWidthPercent,
                colliderSize.y * _climbingSettings.GroundCheckerHeightPercent);

            return Physics2D.OverlapBox(boxCenter, boxSize, 0f, _climbingSettings.GroundLayerMask.value) != null;
        }
        
    }
}