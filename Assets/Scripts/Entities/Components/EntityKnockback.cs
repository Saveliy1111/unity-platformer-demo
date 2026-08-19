using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EntityKnockback : MonoBehaviour
{
    [Header("Knockback Settings")]
    [SerializeField] private float _knockbackForce = 5f;
    [SerializeField] private float _knockbackVelocityY = 0.5f;
    [SerializeField] private float _stunDuration = 0.2f;

    private Rigidbody2D _rigidbody;
    private EntityStun _stunComponent;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _stunComponent = GetComponent<EntityStun>();
    }

    public void Apply(Transform attackerTransform)
    {
        if (attackerTransform == null) return;

        Vector2 knockbackDirection = (transform.position - attackerTransform.position).normalized;
        knockbackDirection.y = _knockbackVelocityY; 
        knockbackDirection = knockbackDirection.normalized;


        _rigidbody.linearVelocity = Vector2.zero;
        _rigidbody.AddForce(knockbackDirection * _knockbackForce, ForceMode2D.Impulse);

        if (_stunComponent != null)
        {
            _stunComponent.Stun(_stunDuration);
        }
    }
}