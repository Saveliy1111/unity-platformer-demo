using System;
using UnityEngine;

public class MountSocket : MonoBehaviour, IMountable
{
    [SerializeField] private Transform _mountSocketTransform;
    [SerializeField] private GameObject _sharedHitbox;

    private Health _health;
    
    public GameObject OwnerObject => gameObject;
    public Transform SocketTransform => _mountSocketTransform;
    public bool IsMounted { get; private set; } 

    public event Action<int, Transform> OnMountTookDamage;

    void Awake()
    {
        _health = GetComponent<Health>();
    }

    void OnEnable()
    {
        _health.OnTakeDamage += HandleDamageTaken;
    }

    void OnDisable()
    {
        _health.OnTakeDamage -= HandleDamageTaken;
    }

    public bool AttachRider()
    {
        if (IsMounted) return false;
        IsMounted = true;

        if (_sharedHitbox != null) _sharedHitbox.SetActive(true);

        return true;
    }

    public void DetachRider()
    {
        if (_sharedHitbox != null) _sharedHitbox.SetActive(false);
        IsMounted = false;
    }

    private void HandleDamageTaken(int damage, Transform attacker)
    {
        if (IsMounted)
        {
            OnMountTookDamage?.Invoke(damage, attacker);
        }
    }
}
