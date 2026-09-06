using UnityEngine;

public class ItemDropper : MonoBehaviour
{
    [Header("Item Drop Settings")]
    [SerializeField] private GameObject _itemPrefab;
    [SerializeField] [Range(0f, 1f)] private float _dropChance = 1f;

    private Health _health;

    void Awake()
    {
        _health = GetComponent<Health>();
    }

    void OnEnable()
    {
        _health.OnDeath += HandleDrop;
    }

    void OnDisable()
    {
        _health.OnDeath -= HandleDrop;
    }

    private void HandleDrop()
    {
        if (_itemPrefab == null) return;
        if (Random.value > _dropChance) return;

        Instantiate(_itemPrefab, transform.position, Quaternion.identity);
    }
}