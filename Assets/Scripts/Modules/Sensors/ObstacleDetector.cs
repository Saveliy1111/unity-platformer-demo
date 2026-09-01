using UnityEngine;

public class ObstacleDetector : MonoBehaviour
{
    [Header("Detection Settings")]
    [SerializeField] private Transform _castPoint;
    [SerializeField] private float _wallRayLength = 0.5f;
    [SerializeField] private float _edgeRayLength = 1.0f;
    [SerializeField] private float _safeDropDepth = 5f;
    [SerializeField] private LayerMask _obstacleLayer;

    [Header("Multi-Ray Settings")]
    [SerializeField] private float _raySpacing = 0.3f;

    private IOrientation _orientation;

    public bool IsHittingWall { get; private set; }
    public bool IsLedge { get; private set; }
    public bool IsSafeDrop { get; private set; }
    public bool IsAbyss { get; private set; }

    private void Start()
    {
        _orientation = GetComponent<IOrientation>();
    }

    void Update()
    {
        RayCastForObstacles();
    }

    private void RayCastForObstacles()
    {
        if (_castPoint == null) return;

        CheckForWall();
        CheckForLedge();
        CheckDropDepth();
    }

    private void CheckForWall()
    {
        Vector2 forwardDirection = _orientation.ForwardVector;
        Vector2 castPos = _castPoint.position;

        RaycastHit2D topHit = Physics2D.Raycast(castPos + new Vector2(0, _raySpacing), forwardDirection, _wallRayLength, _obstacleLayer);
        RaycastHit2D midHit = Physics2D.Raycast(castPos, forwardDirection, _wallRayLength, _obstacleLayer);
        RaycastHit2D botHit = Physics2D.Raycast(castPos - new Vector2(0, _raySpacing), forwardDirection, _wallRayLength, _obstacleLayer);
        
        IsHittingWall = topHit.collider != null || midHit.collider != null || botHit.collider != null;
    }

    private void CheckForLedge()
    {
        RaycastHit2D edgeHit = Physics2D.Raycast(_castPoint.position, Vector2.down, _edgeRayLength, _obstacleLayer);
        IsLedge = edgeHit.collider == null;
    }

    private void CheckDropDepth()
    {
        IsSafeDrop = false;
        IsAbyss = false;

        if (IsLedge)
        {
            RaycastHit2D deepHit = Physics2D.Raycast(_castPoint.position, Vector2.down, _safeDropDepth, _obstacleLayer);
            
            if (deepHit.collider != null)
            {
                IsSafeDrop = true;
            }
            else
            {
                IsAbyss = true;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (_castPoint == null || _orientation == null) return;

        Vector2 forwardDirection = _orientation.ForwardVector;
        Vector2 castPos = _castPoint.position;

        Gizmos.color = Color.red;
        Gizmos.DrawRay(castPos + new Vector2(0, _raySpacing), forwardDirection * _wallRayLength);
        Gizmos.DrawRay(castPos, forwardDirection * _wallRayLength);
        Gizmos.DrawRay(castPos - new Vector2(0, _raySpacing), forwardDirection * _wallRayLength);

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(castPos, Vector2.down * _edgeRayLength);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(castPos + (Vector2.down * _edgeRayLength), Vector2.down * (_safeDropDepth - _edgeRayLength));
    }
}
