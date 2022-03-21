using System.Collections;
using UnityEngine;

namespace ChaosMission.Shooting
{
    public sealed class SelfDestroying : MonoBehaviour
    {
        [SerializeField] private float _lifeTime = 5f;

        private void Start()
        {
            StartCoroutine(Destroying());
        }
        
        private IEnumerator Destroying()
        {
            yield return new WaitForSeconds(_lifeTime);
            Destroy(gameObject);
        }
    }
}
