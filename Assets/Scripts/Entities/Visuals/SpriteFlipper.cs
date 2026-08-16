using UnityEngine;

public class SpriteFlipper : MonoBehaviour
{
    [SerializeField] private EntityOrientation _orientation;


    private EntityStun _stunController;
    private EntityMovement _movementComponent;

    void Start()
    {
        _orientation = GetComponent<EntityOrientation>();
        _stunController = GetComponent<EntityStun>();
        _movementComponent = GetComponent<EntityMovement>();
    }

    void Update()
    {
        if (_stunController != null && _stunController.IsStunned)
        {
            return;
        }

        float movementDirection = _movementComponent.DirectionX;

        if (Mathf.Abs(movementDirection) < 0.01f) return; 

        if (movementDirection > 0)
        {
            float yRotation = _orientation.FacesLeftByDefault ? 180f : 0f;
            transform.localRotation = Quaternion.Euler(0, yRotation, 0);
        }
        else if (movementDirection < 0)
        {
            float yRotation = _orientation.FacesLeftByDefault ? 0f : 180f;
            transform.localRotation = Quaternion.Euler(0, yRotation, 0);
        }
    }
}
