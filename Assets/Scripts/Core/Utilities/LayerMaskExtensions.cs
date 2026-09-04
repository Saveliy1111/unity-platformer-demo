using UnityEngine;

public static class LayerMaskExtensions
{
    //checks if the layerMask contains the specified layerIndex
    public static bool Contains(this LayerMask layerMask, int layerIndex)
    {
        return (layerMask.value & (1 << layerIndex)) > 0;
    }
}