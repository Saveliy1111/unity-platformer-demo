using UnityEngine;

public class DestroyableObject : MonoBehaviour
{
    [SerializeField] private GameObject _destructionParticles;
    private Health _health;

    void Awake()
    {
        _health = GetComponent<Health>();
    }

    void OnEnable()
    {
        _health.OnDeath += HandleDestruction;
    }

    void OnDisable()
    {
        _health.OnDeath -= HandleDestruction;
    }

   private void HandleDestruction()
    {
        if (_destructionParticles != null)
        {
            Instantiate(_destructionParticles, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
