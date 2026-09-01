using UnityEngine;

public class CastPointFlipper : MonoBehaviour
{
    private IOrientation _orientation;
    private float _initialOffsetX;

    void Start()
    {
        _orientation = GetComponentInParent<IOrientation>();
        _initialOffsetX = Mathf.Abs(transform.localPosition.x);
    }

    void Update()
    {
        if (_orientation == null) return;

        float newX = _initialOffsetX * _orientation.FacingDirection;
        
        transform.localPosition = new Vector3(newX, transform.localPosition.y, transform.localPosition.z);
    }
}