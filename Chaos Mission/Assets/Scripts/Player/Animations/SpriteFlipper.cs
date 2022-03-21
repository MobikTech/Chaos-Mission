using System;
using ChaosMission.Common;
using UnityEngine;

namespace ChaosMission.Player.Animations
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Transform))]
    public class SpriteFlipper : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;
        private Transform _transform;
        private float _lastXVelocityValue = 1f;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (CustomMath.HaveDifferentSigns(_lastXVelocityValue, _rigidbody2D.velocity.x))
            {
                Flip();
                _lastXVelocityValue = _rigidbody2D.velocity.x;
            }

        }

        private void Flip()
        {
            // Vector3 lastRotation = _transform.rotation.eulerAngles;
            _transform.right = -_transform.right;
            // _transform.Rotate(new Vector3(lastRotation.x, 180 - lastRotation.y, lastRotation.z));
        }
    }
}