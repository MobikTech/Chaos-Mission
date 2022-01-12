using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject stonePrefab;
    [SerializeField] private float throwForce = 20f;
    
    private void OnShoot()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePosition.z = 0f;
        var playerPosition = transform.position;
        Flying stone = Instantiate(stonePrefab, mousePosition, Quaternion.identity).GetComponent<Flying>();
        stone.Throw(throwForce*(mousePosition-playerPosition));
    }
}
