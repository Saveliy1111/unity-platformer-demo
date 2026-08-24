using UnityEngine;

public class EntityStun : MonoBehaviour
{
    private CooldownTimer _stunTimer = new CooldownTimer();
    
    public bool IsStunned { get; private set; }

    public void Stun(float duration)
    {
        _stunTimer.StartCooldown(duration);
        IsStunned = true;
    }

    private void Update()
    {
        if (IsStunned)
        {
            if (_stunTimer.Tick())
            {
                IsStunned = false;
            }
        }
    }
}