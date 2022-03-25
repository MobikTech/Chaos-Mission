using UnityEngine;

namespace ChaosMission.Shooting
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Throwable : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D = null!;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void Throw(Vector2 force) => _rigidbody2D.AddForce(force, ForceMode2D.Impulse);
    }
}
