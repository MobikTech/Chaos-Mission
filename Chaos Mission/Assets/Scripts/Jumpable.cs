using System;
using System.Collections;
using ChaosMission.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ChaosMission
{
    [RequireComponent(typeof(InputHandler), typeof(Rigidbody2D))]
    public class Jumpable : MonoBehaviour
    {
        [Space, Header("Jumping Setting")]
        [SerializeField] public float _jumpForce = 13f;
        [SerializeField] private BoxCollider2D _boxCollider2D;
        [SerializeField] private LayerMask _groundLayerMask;

        private const float GroundCheckerHeightPercent = 0.1f;
        private const float GroundCheckerWidthPercent = 0.9f;
        
        private Rigidbody2D _rigidbody2D;
        private InputHandler _inputHandler;

        public bool OnJumping { get; private set; } = false;

        public Action StartJumping;
        public Action StopJumping;
        
        
#region UnityMethods

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _inputHandler = GetComponent<InputHandler>();
        }

        private void OnDestroy() => OnDisable();

        private void OnEnable()
        {
            _inputHandler.EnableAction(InputActions.Jumping);
            _inputHandler.AddHandler(InputActions.Jumping, OnJump);
        }

        private void OnDisable()
        {
            _inputHandler.DisableAction(InputActions.Jumping);
            _inputHandler.RemoveHandler(InputActions.Jumping, OnJump);
        }
        
        private void OnDrawGizmosSelected()
        {
            Vector2 colliderSize = _boxCollider2D.size;
            
            Vector3 gizmoSquareCenter = _boxCollider2D.bounds.center;
            gizmoSquareCenter.y -= colliderSize.y / 2 + colliderSize.y * GroundCheckerHeightPercent / 2;
            Vector3 gizmoSquareSize = new Vector3(
                colliderSize.x * GroundCheckerWidthPercent, 
                colliderSize.y * GroundCheckerHeightPercent,
                0.01f);

            Gizmos.color = OnJumping ? Color.red : Color.green;
            Gizmos.DrawWireCube(gizmoSquareCenter, gizmoSquareSize);
        }

#endregion

        private void OnJump(InputAction.CallbackContext context)
        {
            if (!OnGround())
            {
                return;
            }
            _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            OnJumping = true;
            StartCoroutine(Jumping());
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
        
        
        private IEnumerator Jumping()
        {
            StartJumping?.Invoke();
            while (OnGround())
            {
                yield return null;
            }
            while (!OnGround())
            {
                yield return null;
            }

            OnJumping = false;
            StopJumping?.Invoke();
        }

    }
}