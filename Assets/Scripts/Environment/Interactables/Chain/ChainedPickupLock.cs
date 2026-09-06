using UnityEngine;

public class ChainedPickupLock : MonoBehaviour
{
    [SerializeField] private ChainDropper _chainDropper;
    [SerializeField] private Collider2D _pickupCollider;

    void Awake()
    {
        _pickupCollider.enabled = false;
    }

    void OnEnable()
    {
        if (_chainDropper != null)
        {
            _chainDropper.OnChainBreak += HandleChainBreak;
        }
    }

    void OnDisable()
    {
        if (_chainDropper != null)
        {
            _chainDropper.OnChainBreak -= HandleChainBreak;
        }
    }

    private void HandleChainBreak()
    {
        _pickupCollider.enabled = true;
    }
}