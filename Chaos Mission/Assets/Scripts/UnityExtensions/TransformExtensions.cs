using System;
using UnityEngine;

namespace ChaosMission.UnityExtensions
{
    public enum Axes
    {
        X,
        Y,
        Z
    }

    public static class TransformExtensions
    {
        public static bool TryFlip(this Transform transform, Axes inAxis, Vector3 flipDirection)
        {
            Vector3 currentLookVector3 = inAxis switch
            {
                Axes.X => transform.right,
                Axes.Y => transform.up,
                Axes.Z => transform.forward,
                _ => throw new ArgumentOutOfRangeException(nameof(inAxis), inAxis, null)
            };
            
            if (flipDirection == currentLookVector3 ||
                (flipDirection != currentLookVector3 && flipDirection != -currentLookVector3) )
            {
                return false;
            }

            switch (inAxis)
            {
                case Axes.X:
                    transform.right = flipDirection;
                    break;
                case Axes.Y:
                    transform.up = flipDirection;
                    break;
                case Axes.Z:
                    transform.forward = flipDirection;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(inAxis), inAxis, null);
            }
            
            return true;
        }
    }
}