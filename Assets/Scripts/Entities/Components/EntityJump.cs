using UnityEngine;

public class EntityJump : MonoBehaviour
{
    [Header("Jump Settings")]
    [SerializeField] private float _jumpForce = 7.5f;

    private Rigidbody2D _rigidbody;
    private GroundDetector _groundDetector;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _groundDetector = GetComponent<GroundDetector>();
    }

    public void Jump()
    {
        if (_groundDetector.isGrounded)
        {
            _rigidbody.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
        }
    }
}
