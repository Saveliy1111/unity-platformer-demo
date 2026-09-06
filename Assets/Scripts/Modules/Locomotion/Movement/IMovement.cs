using UnityEngine;

public interface IMovement
{
    float DirectionX { get; }
    void SetDirection(float directionX);
    void SetDirection(Vector2 direction);
    void SetSpeed(float newSpeed);
    void LockMovement();
    void UnlockMovement();
}