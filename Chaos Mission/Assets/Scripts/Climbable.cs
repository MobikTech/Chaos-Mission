using System;
using System.Collections;
using UnityEngine;

namespace ChaosMission
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Climbable : MonoBehaviour
    {
        [Space, Header("Climb Setting")]
        [SerializeField] private float _slippingFactor= 1.2f;
        [SerializeField] private BoxCollider2D _boxCollider2D;
        [SerializeField] private LayerMask _groundLayerMask;
        [SerializeField] private float _distance = 0.01f;

        private const float GroundCheckerHeightPercent = 0.9f;
        private const float GroundCheckerWidthPercent = 0.1f;
        
        private Rigidbody2D _rigidbody;
        private float _originalGravityScale;

        public Action StartClimbing;
        public Action StopClimbing;
        

        #region UnityMethods
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _originalGravityScale = _rigidbody.gravityScale;
        }

        private void Update()
        {
            if (CanClimb())
            {
                StartCoroutine(Climbing());
            }
        }

        #endregion
        
        private bool CanClimb() => !OnGround() && TouchesGround();
        private bool TouchesGround()
        {
            Vector3 colliderCenter = _boxCollider2D.bounds.center;
            Vector2 colliderSize = _boxCollider2D.size;

            Vector2 direction = _boxCollider2D.transform.right;
            Vector2 origin = new Vector2(colliderCenter.x + direction.x * colliderSize.x/2, 
                colliderCenter.y);
            
            return Physics2D.Raycast(origin, direction, _distance, _groundLayerMask.value).collider != null;
        }

        private IEnumerator Climbing()
        {
            StartClimbing?.Invoke();

            _rigidbody.gravityScale = 0f;

            _rigidbody.velocity = Vector2.down * _slippingFactor;
            while (CanClimb())
            {
                yield return null;
            }

            _rigidbody.gravityScale = _originalGravityScale;
            
            
            StopClimbing?.Invoke();
        }
        private bool OnGround()
        {
            Vector2 colliderSize = _boxCollider2D.size;
            Vector3 colliderCenter = _boxCollider2D.bounds.center;

            Vector2 boxCenter = new Vector2(colliderCenter.x, colliderCenter.y - colliderSize.y / 2);
            Vector2 boxSize = new Vector2(
                colliderSize.x * GroundCheckerWidthPercent,
                colliderSize.y * GroundCheckerHeightPercent);

            return Physics2D.OverlapBox(boxCenter, boxSize, 0f, _groundLayerMask.value) != null;
        }

    }
}