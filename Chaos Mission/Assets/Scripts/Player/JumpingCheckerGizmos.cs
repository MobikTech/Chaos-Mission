using ChaosMission.Settings.PlayerMoving;
using UnityEngine;

namespace ChaosMission.Player
{
    [RequireComponent(typeof(Collider2D), typeof(MoveController))]
    public class JumpingCheckerGizmos : MonoBehaviour
    {
        //todo: serialize interface IJumpingSettings
        [SerializeField] private MovingSettings _movingSettings;
        private MoveController _moveController;
        private Collider2D _collider2D;

        private void Awake()
        {
            _moveController = GetComponent<MoveController>();
            _collider2D = GetComponent<Collider2D>();
        }

        private void OnDrawGizmosSelected()
        {
            var bounds = _collider2D.bounds;
            Vector2 colliderSize = bounds.size;
            Vector3 gizmoSquareCenter = bounds.center;
            
            gizmoSquareCenter.y -= colliderSize.y / 2 + colliderSize.y * _movingSettings.GroundCheckerHeightPercent / 2;
            Vector3 gizmoSquareSize = new Vector3(
                colliderSize.x * _movingSettings.GroundCheckerWidthPercent, 
                colliderSize.y * _movingSettings.GroundCheckerHeightPercent,
                0.01f);
        
            Gizmos.color = _moveController.MovingStates[MovingState.Jumping].IsActive ? Color.red : Color.green;
            Gizmos.DrawWireCube(gizmoSquareCenter, gizmoSquareSize);
        }
    }
}