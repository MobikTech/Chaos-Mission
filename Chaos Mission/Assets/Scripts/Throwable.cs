using UnityEngine;

namespace ChaosMission
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Throwable: MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void Throw(Vector2 force) => _rigidbody2D.AddForce(force, ForceMode2D.Impulse);
    }
}
