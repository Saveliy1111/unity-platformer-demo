using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EntityKnockback : MonoBehaviour
{
    [Header("Knockback Settings")]
    [SerializeField] private float _knockbackForce = 5f;
    [SerializeField] private float _stunDuration = 0.2f;

    private Rigidbody2D _rb;
    private EntityStun _stunComponent;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _stunComponent = GetComponent<EntityStun>();
    }

    public void Apply(Transform attackerTransform)
    {
        if (attackerTransform == null) return;

        Vector2 knockbackdirection = (transform.position - attackerTransform.position).normalized;
        knockbackdirection.y = 0.5f; 
        knockbackdirection = knockbackdirection.normalized;


        _rb.linearVelocity = Vector2.zero;
        _rb.AddForce(knockbackdirection * _knockbackForce, ForceMode2D.Impulse);

        if (_stunComponent != null)
        {
            _stunComponent.Stun(_stunDuration);
        }
    }
}