using AiEditorToolsSdk.Domain.Abstractions.Services;
using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Weight))]
public class Pusher : MonoBehaviour
{
    [Header("Push Settings")]
    [SerializeField] private float _baseSpeed = 5f;
    [SerializeField] private float _pushSpeed = 2f;
    [SerializeField] private Vector2 _boxSize = new Vector2(0.5f, 1.5f);
    [SerializeField] private LayerMask _pushableLayer;
    [SerializeField] private Transform _castPoint;

    private Movement _movement;
    private IOrientation _orientation;
    private Weight _myWeight;
    private bool _isPushing;

    void Start()
    {
        _movement = GetComponent<Movement>();
        _orientation = GetComponent<IOrientation>();
        _myWeight = GetComponent<Weight>();
    }

    void FixedUpdate()
    {
        if (_orientation == null) return;
        UpdatePushState();
    }

    private void UpdatePushState()
    {
        IPushable detectedPushableObject = ScanForPushableObject();

        bool canPush = false;
        if(detectedPushableObject != null && detectedPushableObject.WeightLevel <= _myWeight.WeightLevel)
        {
            canPush = true;
        }

        InteractWithPushable(canPush, detectedPushableObject);
    }

    private IPushable ScanForPushableObject()
    {
        if (_castPoint == null) return null;

        Collider2D hitCollider = Physics2D.OverlapBox(_castPoint.position, _boxSize, 0f, _pushableLayer);
        
        if (hitCollider != null && hitCollider.TryGetComponent(out IPushable pushable))
        {
            return pushable;
        }

        return null;
    }

    private void InteractWithPushable(bool canPush, IPushable pushableObject)
    {
        if (canPush)
        {
            if (!_isPushing)
            {
                _isPushing = true;
                _movement.SetSpeed(_pushSpeed);
            }

            if (Mathf.Abs(_movement.DirectionX) > 0.01f)
            {
                float pushVelocity = _movement.DirectionX * _pushSpeed;
                pushableObject.Push(pushVelocity);
            }
        }
        else if (_isPushing)
        {
            _isPushing = false;
            _movement.SetSpeed(_baseSpeed);
        }
    }


    private void OnDrawGizmos()
    {
       if (_castPoint != null)
        {
            Gizmos.color = _isPushing ? Color.green : Color.yellow;
            Gizmos.DrawWireCube(_castPoint.position, _boxSize);
        }
    }
}
