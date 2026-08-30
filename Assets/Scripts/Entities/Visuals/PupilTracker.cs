using UnityEngine;
using UnityEngine.InputSystem;

public class PupilTracker : MonoBehaviour, IPlayerActiveListener
{
    [Header("Tracking Settings")]
    [SerializeField] private float _maxRadius = 0.08f;
    private bool _isActive = false;

    private Camera _mainCamera;

    void Start()
    {
        _mainCamera = Camera.main;
    }

    void Update()
    {
        if (!_isActive) return;
        if (!CanTrackMouse()) return;

        Vector3 targetLocalPosition = CalculateTargetLocalPosition();
        ApplyPupilPosition(targetLocalPosition);
    }

    public void OnActiveStateChanged(bool isActive)
    {
        _isActive = isActive;

        if (!_isActive)
        {
            ApplyPupilPosition(new Vector3(0f, 0f, transform.localPosition.z));
        }
    }

    private bool CanTrackMouse()
    {
        return _mainCamera != null && Mouse.current != null;
    }

    private Vector3 CalculateTargetLocalPosition()
    {
        Vector3 mouseWorldPos = MouseUtility.GetWorldPosition(_mainCamera, transform.position.z);

        Vector3 localMousePos = transform.parent.InverseTransformPoint(mouseWorldPos);
        Vector2 clampedLocalPos = Vector2.ClampMagnitude(new Vector2(localMousePos.x, localMousePos.y), _maxRadius);

        return new Vector3(clampedLocalPos.x, clampedLocalPos.y, transform.localPosition.z);
    }

    private void ApplyPupilPosition(Vector3 newPosition)
    {
        transform.localPosition = newPosition;
    }
}