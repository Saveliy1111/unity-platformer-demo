using UnityEngine;
using UnityEngine.Events;

public class GroundDetector : MonoBehaviour
{
    [Header("Ground Check Settings")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private Vector2 _boxSize = new Vector2(0.8f, 0.1f);
    [SerializeField] private LayerMask _environmentLayer;

    [Header("Events (For UI & Visuals)")]
    public UnityEvent OnLandVisuals;

    public bool isGrounded {get; private set;}

    private bool _wasGrounded = true;

    void Update()
    {
        isGrounded = Physics2D.OverlapBox(_groundCheck.position, _boxSize, 0f, _environmentLayer);

        if (!_wasGrounded && isGrounded)  OnLandVisuals?.Invoke();

        _wasGrounded = isGrounded;
    }

    private void OnDrawGizmosSelected()
    {
        if (_groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(_groundCheck.position, _boxSize);
        }
    }
}
