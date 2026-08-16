using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Header ("Health Settings")]
    [SerializeField] private int _maxHealth = 3;

    private int _currentHealth;
    private bool _isDead;
    private EntityKnockback _knockbackComponent;


    [Header ("Events")]
    public UnityEvent<int> OnHealthChanged;
    public UnityEvent OnTakeDamage;
    public UnityEvent OnDeath;

    void Start()
    {      
        _knockbackComponent = GetComponent<EntityKnockback>();
        ResetHealth();
    }

    public void ResetHealth()
    {
        _currentHealth = _maxHealth;
        _isDead = false;

        OnHealthChanged?.Invoke(_currentHealth);
    }

    public void TakeDamage(int damage)
    {
        TakeDamage(damage, null);
    }

    public void TakeDamage(int damage, Transform attackerTransform)
    {
        if (!_isDead)
        {
            _currentHealth -= damage;

            OnTakeDamage?.Invoke();
            OnHealthChanged?.Invoke(_currentHealth);

            if (attackerTransform != null && _knockbackComponent != null)
            {
                _knockbackComponent.Apply(attackerTransform);
            }

            if (_currentHealth <= 0)
            {
                Die();
            }
        }
    }

    public void Die()
    {
        _isDead = true;
        OnDeath?.Invoke();
    }
}
