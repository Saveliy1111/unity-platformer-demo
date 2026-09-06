using UnityEngine;

public class PlayerDeathHandler : MonoBehaviour
{
    private Health _health;
    private Collider2D[] _colliders;

    void Awake()
    {
        _health = GetComponent<Health>();
        _colliders = GetComponentsInChildren<Collider2D>();
    }

    void OnEnable()
    {
        if (_health != null)
        {
            _health.OnDeath += HandleDeath;
        }
    }

    void OnDisable()
    {
        if (_health != null)
        {
            _health.OnDeath -= HandleDeath;
        }
    }

    private void HandleDeath()
    {
        foreach (Collider2D col in _colliders)
        {
            col.enabled = false;
        }

        if (PlayerManager.Instance != null)
        {
            PlayerManager.Instance.HandlePlayerDeath(gameObject);
        }
    }
}