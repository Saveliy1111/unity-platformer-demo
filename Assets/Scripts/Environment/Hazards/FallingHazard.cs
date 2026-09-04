using UnityEngine;

public class FallingHazard : MonoBehaviour
{
    [Header("Hazard Settings")]
    [SerializeField] private int _damage = 5;
    [SerializeField] private float _minFallSpeed = 5f;

    private Rigidbody2D _rb;
    private float _lastFrameVelocityY;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        _lastFrameVelocityY = _rb.linearVelocity.y;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_lastFrameVelocityY > -_minFallSpeed) return;

        ContactPoint2D contact = collision.GetContact(0);
        
        if (contact.normal.y > 0.5f)
        {
            if (collision.gameObject.TryGetComponent(out Health health))
            {
                health.TakeDamage(_damage, transform);
            }
        }
    }
}
