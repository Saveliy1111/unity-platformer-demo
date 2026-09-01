using UnityEngine;

public class DamageOnTouch : MonoBehaviour, IOnDeathListener
{
    [Header("Damage Settings")]
    [SerializeField] private int _damage = 1;
    [SerializeField] private string _targetTag = Constants.PLAYER_TAG;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!this.enabled) return;
        TryDealDamage(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!this.enabled) return;
        TryDealDamage(collider.gameObject);
    }

    private void TryDealDamage(GameObject target)
    {
        if (target.CompareTag(_targetTag))
        {
            Health targetHealth = target.GetComponent<Health>();

            if(targetHealth != null)
            {
                targetHealth.TakeDamage(_damage, transform);
            }
        }
    }

    public void HandleDeath()
    {
        this.enabled = false;
    }
}
