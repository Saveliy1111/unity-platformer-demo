using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Shooter))]
public class PlayerShooter : MonoBehaviour, IPlayerActiveListener
{
    private Shooter _entityShoot;
    private Camera _mainCamera;
    private bool _isActive = false;

    void Awake()
    {
        _entityShoot = GetComponent<Shooter>();
        _mainCamera = Camera.main;
    }

    void Start()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnShoot += HandleShoot;
        }
    }

    void OnDestroy()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnShoot -= HandleShoot;
        }
    }

    public void OnActiveStateChanged(bool isActive)
    {
        _isActive = isActive;
    }

    private void HandleShoot()
    {
        if (!_isActive || Mouse.current == null) return;

        Vector3 targetPosition = MouseUtility.GetWorldPosition(_mainCamera, transform.position.z);
        _entityShoot.StartShooting(targetPosition);
    }
}
