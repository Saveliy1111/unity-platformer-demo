using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Moving Platform Settings")]
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private Transform[] _waypoints;
    
    private Vector2[] _cachedTargetPositions;
    private int _currentWaypointIndex = 0;

    private void Start()
    {
        _cachedTargetPositions = new Vector2[_waypoints.Length];

        for (int i = 0; i < _waypoints.Length; i++)
        {
            if (_waypoints[i] != null)
            {
                _cachedTargetPositions[i] = _waypoints[i].position;
                Destroy(_waypoints[i].gameObject);
            }
        }
    }

    private void Update()
    {
        if (_cachedTargetPositions == null || _cachedTargetPositions.Length == 0) return;

        Vector2 targetPosition = _cachedTargetPositions[_currentWaypointIndex];
        
        MoveTowardsWaypoint(targetPosition);
        CheckIfWaypointReached(targetPosition);
    }

    private void MoveTowardsWaypoint(Vector2 targetPosition)
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            targetPosition,
            _moveSpeed * Time.deltaTime
        );
    }

    private void CheckIfWaypointReached(Vector2 targetPosition)
    {
         if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            SwitchToNextWaypoint();
        }
    }

    private void SwitchToNextWaypoint()
    {
        _currentWaypointIndex++;
        if (_currentWaypointIndex >= _cachedTargetPositions.Length)
        {
            _currentWaypointIndex = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Constants.PLAYER_TAG))
        {
            collision.gameObject.transform.SetParent(transform);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground")) 
        {
            SwitchToNextWaypoint();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Constants.PLAYER_TAG))
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}