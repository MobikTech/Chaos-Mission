using ChaosMission.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ChaosMission
{
    public sealed class PlayerController : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;

#region UnityMethods

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void OnDestroy() => OnDisable();

        private void OnEnable()
        {
            InputHandler.InputCollection.Player.Enable();
            
            InputHandler.InputCollection.Player.Moving.performed += Move;
            // InputHandler.InputCollection.Player.Shooting.performed += Shoot;
        }

        private void OnDisable()
        {
            InputHandler.InputCollection.Player.Disable();
            
            InputHandler.InputCollection.Player.Moving.performed -= Move;
            // InputHandler.InputCollection.Player.Shooting.performed -= Shoot;
        }

#endregion

        private void Move(InputAction.CallbackContext context)
        {
            _rigidbody2D.AddForce(context.ReadValue<Vector2>(), ForceMode2D.Force);
        }

        // private void Shoot(InputAction.CallbackContext context)
        // {
        //     Debug.Log("shot");
        // }
    }
}