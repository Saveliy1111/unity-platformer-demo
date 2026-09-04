using System;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Header ("Health Settings")]
    [SerializeField] private int _maxHealth = 3;
    [SerializeField] private float _iFrameDuration = 0.2f;

    private int _currentHealth;
    public int CurrentHealth => _currentHealth;
    
    public bool IsDead { get; private set; }
    public bool IsInvincible { get; set; }

    private Knockback _knockbackComponent;
    private float _lastDamageTime = -Mathf.Infinity;

    public event Action<int, Transform> OnTakeDamage;
    public event Action OnDeath;

    [Header ("Events (For UI & Visuals)")]
    public UnityEvent<int> OnHealthChangedVisuals;
    public UnityEvent OnTakeDamageVisuals;
    public UnityEvent OnDeathVisuals;

    void Start()
    {      
        _knockbackComponent = GetComponent<Knockback>();
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
        if (IsDead || IsInvincible || Time.time - _lastDamageTime < _iFrameDuration)
        {
            return;
        }

        _lastDamageTime = Time.time;

        _currentHealth -= damage;
        OnHealthChangedVisuals?.Invoke(_currentHealth);

        OnTakeDamage?.Invoke(damage, attackerTransform);
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

    public void Die()
    {
        IsDead = true;
        OnDeath?.Invoke();
        OnDeathVisuals?.Invoke();
    }
}
