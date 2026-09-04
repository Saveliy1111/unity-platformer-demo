using UnityEngine;

public class ChainDropper : MonoBehaviour
{
    [SerializeField] private Health _weakLinkHealth;
    [SerializeField] private Rigidbody2D _targetRigidbody;

    void OnEnable()
    {
        if (_weakLinkHealth != null)
        {
            _weakLinkHealth.OnDeath += HandleChainBreak;
        }
    }

    void OnDisable()
    {
        if (_weakLinkHealth != null)
        {
            _weakLinkHealth.OnDeath -= HandleChainBreak;
        }
    }

    private void HandleChainBreak()
    {
        if (_targetRigidbody != null)
        {
            _targetRigidbody.transform.SetParent(null);
            _targetRigidbody.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
