using UnityEngine;

public class DamageProxy : MonoBehaviour
{
    [SerializeField] private Health _parentHealth;

    public void TakeDamage(int damage, Transform attackerTransform = null)
    {
        if (_parentHealth != null)
        {
            _parentHealth.TakeDamage(damage, attackerTransform);
        }
    }
}