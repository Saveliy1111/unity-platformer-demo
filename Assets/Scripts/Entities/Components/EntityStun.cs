using UnityEngine;

public class EntityStun : MonoBehaviour
{
    public bool IsStunned { get; private set; }
    
    private float _stunTimer;

    public void Stun(float duration)
    {
        _stunTimer = duration;
        IsStunned = true;
    }

    private void Update()
    {
        if (_stunTimer > 0)
        {
            _stunTimer -= Time.deltaTime;
            if (_stunTimer <= 0)
            {
                IsStunned = false;
            }
        }
    }
}