using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [Header("Detection Settings")]
    [SerializeField] private Transform _castPoint;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private LayerMask _environmentLayer;

    [Header ("Ranges and Vision")]
    [SerializeField] private float _detectionRange = 5f;
    [SerializeField] private float _aggroRadius = 8f;
    [SerializeField] [Range(0f, 360f)] private float _viewAngle = 90f;

    private IEntityOrientation _orientation;

    public bool HasAggro { get; private set; }
    public Transform Target { get; private set; }

    void Start()
    {
        _orientation = GetComponent<IEntityOrientation>();
    }

    void Update()
    {
        if (!HasAggro) SearchForPlayer();
        else CheckDistanceToPlayer();
    }

    public void ForceAggro(Transform attacker)
    {
        if (attacker != null)
        {
            HasAggro = true;
            Target = attacker;
        }
    }
    
    private void SearchForPlayer()
    {
        if (_castPoint == null) return;
        
        Collider2D hitPlayer = Physics2D.OverlapCircle(_castPoint.position, _detectionRange, _playerLayer);
        if (hitPlayer == null) return;

        Vector2 directionToPlayer = (hitPlayer.transform.position - _castPoint.position).normalized;
        float distanceToPlayer = Vector2.Distance(_castPoint.position, hitPlayer.transform.position);
        Vector2 forwardDirection = _orientation.ForwardVector;

       if (IsTargetInViewAngle(directionToPlayer) && HasLineOfSightToTarget(directionToPlayer, distanceToPlayer))
        {
            HasAggro = true;
            Target = hitPlayer.transform;
        }
    }

    private bool IsTargetInViewAngle(Vector2 directionToTarget)
    {
        float angleToTarget = Vector2.Angle(_orientation.ForwardVector, directionToTarget);
        return angleToTarget <= _viewAngle / 2f;
    }

    private bool HasLineOfSightToTarget(Vector2 directionToTarget, float distanceToTarget)
    {
        RaycastHit2D obstacleHit = Physics2D.Raycast(_castPoint.position, directionToTarget, distanceToTarget, _environmentLayer);
        return obstacleHit.collider == null;
    }

    private void CheckDistanceToPlayer()
    {
        if (Target == null)
        {
            HasAggro = false;
            return;
        }

        float distance = Vector2.Distance(transform.position, Target.position);

        if (distance > _aggroRadius)
        {
            HasAggro = false;
            Target = null;
        }
    }

    private void OnDrawGizmos()
    {
        if (_castPoint == null || _orientation == null) return;

        if (!HasAggro)
        {
            Gizmos.color = Color.yellow;
            Vector2 forward = _orientation.ForwardVector;
            Vector2 upperLimit = Quaternion.Euler(0, 0, _viewAngle / 2f) * forward;
            Vector2 lowerLimit = Quaternion.Euler(0, 0, -_viewAngle / 2f) * forward;

            Gizmos.DrawRay(_castPoint.position, upperLimit * _detectionRange);
            Gizmos.DrawRay(_castPoint.position, lowerLimit * _detectionRange);
            Gizmos.DrawWireSphere(_castPoint.position, _detectionRange);
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _aggroRadius);
        }
    }
}
