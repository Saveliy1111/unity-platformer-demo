using System;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Header ("Health Settings")]
    [SerializeField] private int _maxHealth = 3;

    private int _currentHealth;
    public bool IsDead { get; private set; }
    private EntityKnockback _knockbackComponent;

    public event Action OnTakeDamage;
    public event Action OnDeath;

    [Header ("Events (For UI & Visuals)")]
    public UnityEvent<int> OnHealthChangedVisuals;
    public UnityEvent OnTakeDamageVisuals;
    public UnityEvent OnDeathVisuals;

    void Start()
    {      
        _knockbackComponent = GetComponent<EntityKnockback>();
        ResetHealth();
    }

    public void ResetHealth()
    {
        _currentHealth = _maxHealth;
        IsDead = false;

        OnHealthChangedVisuals?.Invoke(_currentHealth);
    }

    public void TakeDamage(int damage)
    {
        TakeDamage(damage, null);
    }

    public void TakeDamage(int damage, Transform attackerTransform)
    {
        if (!IsDead)
        {
            _currentHealth -= damage;
            OnHealthChangedVisuals?.Invoke(_currentHealth);

            OnTakeDamage?.Invoke();
            OnTakeDamageVisuals?.Invoke();

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
        IsDead = true;
        OnDeath?.Invoke();
        OnDeathVisuals?.Invoke();
    }
}
