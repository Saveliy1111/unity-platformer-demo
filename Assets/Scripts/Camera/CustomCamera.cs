using UnityEngine;

public class CustomCamera : MonoBehaviour
{
    [Header("Target Settings")]
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset = new Vector3(0f, 0f, -10f);

    [Header("Smoothness Settings")]
    [SerializeField] private float _smoothTime = 0.2f;

    private Vector3 _currentVelocity = Vector3.zero;

    void LateUpdate()
    {
        Vector3 targetPosition = _target.position + _offset;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref _currentVelocity,
            _smoothTime
        );
    }
}
