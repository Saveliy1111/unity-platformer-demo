using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [Header("Ground Check Settings")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundCheckRadius = 0.4f;
    [SerializeField] private LayerMask _groundLayer;

    public bool isGrounded {get; private set;}

    void Update()
    {
        isGrounded 
        = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        if (_groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(_groundCheck.position, _groundCheckRadius);
        }
    }
}
