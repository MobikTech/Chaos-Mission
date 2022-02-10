using UnityEngine;

namespace ChaosMission.Extensions.UnityExtensions
{
    public static class Collider2DExtensions
    {
        public static bool OverlapBoxOnDown(this Collider2D collider2D, LayerMask layerMask, 
            float boxHeightPercent, float boxWidthPercent)
        {
            Bounds bounds = collider2D.bounds;
            Vector2 colliderSize = bounds.size;
            Vector3 colliderCenter = bounds.center;

            Vector2 boxCenter = new Vector2(colliderCenter.x, colliderCenter.y - colliderSize.y / 2);
            Vector2 boxSize = new Vector2(
                colliderSize.x * boxWidthPercent,
                colliderSize.y * boxHeightPercent);

            return Physics2D.OverlapBox(boxCenter, boxSize, 0f, layerMask.value) != null;
        }
        
        public static bool RaycastFromCenterToRight(this Collider2D collider2D, LayerMask layerMask, 
            float distance)
        {
            var bounds = collider2D.bounds;
            Vector3 colliderCenter = bounds.center;
            Vector2 colliderSize = bounds.size;

            Vector2 direction = collider2D.transform.right;
            Vector2 origin = new Vector2(colliderCenter.x + direction.x * colliderSize.x / 2, 
                colliderCenter.y);
            
            return Physics2D.Raycast(origin, direction, distance, layerMask.value).collider != null;
        }
    }
}