using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject stonePrefab;
    [SerializeField] private float throwForce = 13f;
    [SerializeField] private float offset = 2f;
    
    private void OnShoot()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePosition.z = 0f;
        var playerPosition = transform.position;
        var direction = mousePosition - playerPosition;
        var spawnPosition = playerPosition + offset * direction.normalized;
        Flying stone = Instantiate(stonePrefab, spawnPosition, Quaternion.identity, transform).GetComponent<Flying>();
        stone.Throw(throwForce* direction.normalized);
    }
}
