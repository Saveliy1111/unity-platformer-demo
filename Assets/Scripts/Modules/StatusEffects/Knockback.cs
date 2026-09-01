using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Knockback : MonoBehaviour
{
    [Header("Knockback Settings")]
    [SerializeField] private float _knockbackForce = 5f;
    [SerializeField] private float _knockbackVelocityY = 0.5f;
    [SerializeField] private float _stunDuration = 0.2f;

    private Rigidbody2D _rigidbody;
    private Stun _stunComponent;
    private Weight _weightComponent;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _stunComponent = GetComponent<Stun>();
        _weightComponent = GetComponent<Weight>();
    }

    public void Apply(Transform attackerTransform)
    {
        if (attackerTransform == null) return;

        Vector2 knockbackDirection = (transform.position - attackerTransform.position).normalized;
        knockbackDirection.y = _knockbackVelocityY; 
        knockbackDirection = knockbackDirection.normalized;

        float resistance = _weightComponent != null ? _weightComponent.KnockbackResistance : 0f;
        float finalKnockbackForce = _knockbackForce * (1f - resistance);

        _rigidbody.linearVelocity = Vector2.zero;
        _rigidbody.AddForce(knockbackDirection * finalKnockbackForce, ForceMode2D.Impulse);

        if (_stunComponent != null)
        {
            _stunComponent.ApplyStun(_stunDuration);
        }
    }
}