using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject stone;
    
    private void OnShoot(InputValue inputValue)
    {
        var mousePosition = inputValue.Get<Vector3>();
        var playerPosition = transform.position;
        Instantiate(stone, playerPosition, Quaternion.FromToRotation(playerPosition, mousePosition));
    }
}
