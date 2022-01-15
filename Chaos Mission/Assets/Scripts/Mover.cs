using System;
using System.Collections;
using ChaosMission.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ChaosMission
{
    [RequireComponent(typeof(InputHandler), typeof(Rigidbody2D))]
    public sealed class Mover : MonoBehaviour
    {
        [Header("Moving Setting")]
        [SerializeField] private float _maxMoveSpeed = 6f;
        [SerializeField, Range(0, 1)] private float _accelerationFactor = 0.3f;
        
        [Space, Header("Jumping Setting")]
        [SerializeField] public float _jumpForce = 13f;
        [SerializeField] private BoxCollider2D _boxCollider2D;
        [SerializeField] private LayerMask _groundLayerMask;

        private const float GroundCheckerHeightPercent = 0.1f;
        private const float GroundCheckerWidthPercent = 0.9f;
        
        private Rigidbody2D _rigidbody2D;
        private InputHandler _inputHandler;

        private bool _jumping = false;
        

#region UnityMethods

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _inputHandler = GetComponent<InputHandler>();
        }

        private void OnDestroy() => OnDisable();

        private void OnEnable()
        {
            _inputHandler.EnableAction(InputActions.Moving);
            _inputHandler.EnableAction(InputActions.Jumping);
            _inputHandler.AddHandler(InputActions.Moving, OnMove);
            _inputHandler.AddHandler(InputActions.Jumping, OnJump);
        }

        private void OnDisable()
        {
            _inputHandler.DisableAction(InputActions.Moving);
            _inputHandler.DisableAction(InputActions.Jumping);
            _inputHandler.RemoveHandler(InputActions.Moving, OnMove);
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

            ChangeJumpingGizmosColor();
            Gizmos.DrawWireCube(gizmoSquareCenter, gizmoSquareSize);
        }

#endregion

        private void OnMove(InputAction.CallbackContext context) => StartCoroutine(Moving(context));
        private void OnJump(InputAction.CallbackContext context)
        {
            if (OnGround())
            {
                _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
                _jumping = true;
                StartCoroutine(Jumping());
            }
        }

        private bool OnGround()
        {
            Vector2 colliderSize = _boxCollider2D.size;
            Vector3 colliderCenter = _boxCollider2D.bounds.center;

            Vector2 boxCenter = new Vector2(colliderCenter.x, colliderCenter.y - colliderSize.y / 2);
            Vector2 boxSize = new Vector2(
                colliderSize.x * GroundCheckerWidthPercent,
                colliderSize.y * GroundCheckerHeightPercent);

            return Physics2D.OverlapBox(boxCenter, boxSize, 0f, _groundLayerMask.value);
        }

        

        private void ChangeJumpingGizmosColor()
        {
            switch (_jumping)
            {
                case true:
                    Gizmos.color = Color.red;
                    break;
                case false:
                    Gizmos.color = Color.green;
                    break;
            }
        }

        private IEnumerator Moving(InputAction.CallbackContext context)
        {
            WaitForFixedUpdate fixedUpdate = new WaitForFixedUpdate();
            
            while (context.phase == InputActionPhase.Performed)
            {
                Vector2 currentVelocity = _rigidbody2D.velocity;
                float xVelocityAddition = context.ReadValue<float>() * _accelerationFactor;
                _rigidbody2D.velocity = new Vector2(
                        Mathf.Clamp(currentVelocity.x + xVelocityAddition, -_maxMoveSpeed, _maxMoveSpeed), 
                        currentVelocity.y);
                
                yield return fixedUpdate;
            }
            
            _rigidbody2D.velocity = new Vector2(0f, _rigidbody2D.velocity.y);
        }

        private IEnumerator Jumping()
        {
            while (OnGround())
            {
                yield return null;
            }
            while (!OnGround())
            {
                yield return null;
            }

            _jumping = false;
        }
    }
}