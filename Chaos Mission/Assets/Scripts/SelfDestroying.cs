using System.Collections;
using UnityEngine;

public class SelfDestroying : MonoBehaviour
{
    [SerializeField] private float lifeTime = 5f;

    private void Start()
    {
        StartCoroutine(Destroying());
    }
    private IEnumerator Destroying()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
