using UnityEngine;
using UnityEngine.Events;

public class GroundDetector : MonoBehaviour
{
    [Header("Ground Check Settings")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundCheckRadius = 0.4f;
    [SerializeField] private LayerMask _groundLayer;

    [Header("Events (For UI & Visuals)")]
    public UnityEvent OnLandVisuals;

    public bool isGrounded {get; private set;}

    private bool _wasGrounded = true;

    void Update()
    {
        isGrounded 
        = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);

        if (!_wasGrounded && isGrounded)  OnLandVisuals?.Invoke();

        _wasGrounded = isGrounded;
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
