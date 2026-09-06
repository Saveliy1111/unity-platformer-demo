using UnityEngine;

public interface IEnemyNavigationStrategy
{
    float CurrentDirection { get; }
    bool IsMoving { get; }
    float ExecutePatrol(float currentDirection, float speed);
    void ExecuteChase(Transform target, float speed, float deadzoneX);
    void StopMovement();
}