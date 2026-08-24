using UnityEngine;
public interface IEntityOrientation
{
    float FacingDirection { get; }
    Vector2 ForwardVector { get; }
}