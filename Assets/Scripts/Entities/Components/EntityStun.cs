using UnityEngine;

public class EntityStun : MonoBehaviour
{
    private CountdownTimer _stunTimer = new CountdownTimer();
    
    public bool IsStunned { get; private set; }

    public void Stun(float duration)
    {
        _stunTimer.StartCountdown(duration);
        IsStunned = true;
    }

    private void Update()
    {
        if (IsStunned)
        {
            _stunTimer.Tick();
            if (_stunTimer.IsFinished)
            {
                IsStunned = false;
            }
        }
    }
}