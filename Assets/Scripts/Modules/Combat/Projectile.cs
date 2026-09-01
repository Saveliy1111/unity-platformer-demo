using UnityEngine;
using UnityEngine.Pool;

public class Projectile : MonoBehaviour
{
    protected IObjectPool<Projectile> _pool;
    protected Transform _shooterTransform;

    public void Setup(IObjectPool<Projectile> pool, Transform shooterTransform)
    {
        _pool = pool;
        _shooterTransform = shooterTransform;
    }

    public virtual void Launch()
    {
    }

    protected virtual void ReturnToPool()
    {
        if (gameObject.activeSelf && _pool != null)
        {
            _pool.Release(this);
        }
    }
}
