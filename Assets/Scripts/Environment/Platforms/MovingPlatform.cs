using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Moving Platform Settings")]
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private Transform[] _waypoints;
    
    private int _currentWaypointIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_waypoints.Length == 0) return;

        Transform targetWaypoint = _waypoints[_currentWaypointIndex];
        MoveTowardsWaypoint(targetWaypoint);
        CheckIfWaypointReached(targetWaypoint);
    }

    private void MoveTowardsWaypoint(Transform targetWaypoint)
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            targetWaypoint.position,
            _moveSpeed * Time.deltaTime
        );
    }

    private void CheckIfWaypointReached(Transform targetWaypoint)
    {
         if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            _currentWaypointIndex++;
            if (_currentWaypointIndex >= _waypoints.Length)
            {
                _currentWaypointIndex = 0;
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Constants.PLAYER_TAG))
        {
            collision.gameObject.transform.SetParent(transform);
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
