using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [Header("Detection Settings")]
    [SerializeField] private Transform _castPoint;
    [SerializeField] private LayerMask _playerLayer;

    [Header ("Ranges")]
    [SerializeField] private float _detectionRange = 5f;
    [SerializeField] private float _aggroRadius = 8f;

    private IEntityOrientation _orientation;

    public bool HasAggro { get; private set; }
    public Transform Target { get; private set; }

    void Start()
    {
        _orientation = GetComponent<IEntityOrientation>();
    }

    void Update()
    {
        if (!HasAggro)
        {
            SearchForPlayer();
        }
        else
        {
            CheckDistanceToPlayer();
        }

    }
    
    private void SearchForPlayer()
    {
        if (_castPoint == null) return;
        
        Vector2 forwardDirection = _orientation.ForwardVector;

        RaycastHit2D hit = Physics2D.Raycast(_castPoint.position, forwardDirection, _detectionRange, _playerLayer);
        if (hit.collider != null)
        {
            HasAggro = true;
            Target = hit.collider.transform;
        }
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
           Vector2 forwardDirection = _orientation.ForwardVector;

            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(_castPoint.position, forwardDirection * _detectionRange);
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _aggroRadius);
        }
    }
}
