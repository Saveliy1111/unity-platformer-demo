using UnityEngine;

[RequireComponent(typeof(Stun))]
public class DIsableOnStun : MonoBehaviour
{
    [SerializeField] private Behaviour[] _componentsToDisable;

    private Stun _stunComponent;

    void Awake()
    {
        _stunComponent = GetComponent<Stun>();
    }

    void OnEnable()
    {
        _stunComponent.OnStunStateChanged += HandleStunStateChanged;
    }

    void OnDisable()
    {
        _stunComponent.OnStunStateChanged -= HandleStunStateChanged;
    }

    private void HandleStunStateChanged(bool isStunned)
    {
        foreach (Behaviour component in _componentsToDisable)
        {
            if (component != null)
            {
                component.enabled = !isStunned;
            }
        }
    }
}
