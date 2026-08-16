using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField, Range(0f, 1f)] private float _parallaxEffectMultiplier;
    
    private Vector3 _lastCameraPosition;

    private void Start()
    {
        if (_cameraTransform != null)
        {
            _lastCameraPosition = _cameraTransform.position;
        }
    }

    private void Update()
    {
        if (_cameraTransform == null) return;

        Vector3 deltaMovement = _cameraTransform.position - _lastCameraPosition;
        
        transform.position += new Vector3(deltaMovement.x * _parallaxEffectMultiplier, 0, 0);
        
        _lastCameraPosition = _cameraTransform.position;
    }
}