using UnityEngine;
using UnityEngine.InputSystem;

public static class MouseUtility
{
    public static Vector3 GetWorldPosition(Camera camera, float targetZ)
    {
        if (camera == null || Mouse.current == null) return Vector3.zero;

        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 screenPosWithZ = new Vector3(mouseScreenPos.x, mouseScreenPos.y, Mathf.Abs(camera.transform.position.z - targetZ));
        
        Vector3 mouseWorldPos = camera.ScreenToWorldPoint(screenPosWithZ);
        mouseWorldPos.z = targetZ;

        return mouseWorldPos;
    }
}