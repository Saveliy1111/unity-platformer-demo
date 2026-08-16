using UnityEngine;

public static class DirectionUtils
{
    public static Vector2 GetForwardVector(Transform transform, IEntityOrientation cachedOrientation = null)
    {
        IEntityOrientation orientation = cachedOrientation ?? transform.GetComponent<IEntityOrientation>();
        
        bool facesLeft = orientation != null && orientation.FacesLeftByDefault;
        
        Vector2 rightVector = transform.right;
        if (facesLeft) 
        {
            return -rightVector;
        }
        
        return rightVector;
    }
}