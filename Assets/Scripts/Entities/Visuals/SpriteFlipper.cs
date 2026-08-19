using UnityEngine;

public class SpriteFlipper : MonoBehaviour
{
    private IEntityOrientation _orientation;
    private EntityStun _stunController;
    private EntityMovement _movementComponent;

    void Start()
    {
        _orientation = GetComponentInParent<IEntityOrientation>();
        _stunController = GetComponentInParent<EntityStun>();
        _movementComponent = GetComponentInParent<EntityMovement>();
    }

    void Update()
    {
        if (_stunController != null && _stunController.IsStunned)
        {
            return;
        }

        float movementDirection = _movementComponent.DirectionX;
        if (Mathf.Abs(movementDirection) < 0.01f) return; 

        FlipSprite(movementDirection);
    }

    private void FlipSprite(float movementDirection)
    {
        bool facesLeft = false;
        
        if (_orientation is EntityOrientation orientationComponent)
        {
            facesLeft = orientationComponent.FacesLeftByDefault;
        }

        if (movementDirection > 0)
        {
            float yRotation = facesLeft ? 180f : 0f;
            transform.localRotation = Quaternion.Euler(0, yRotation, 0);
        }
        else if (movementDirection < 0)
        {
            float yRotation = facesLeft ? 0f : 180f;
            transform.localRotation = Quaternion.Euler(0, yRotation, 0);
        }
    }
}
