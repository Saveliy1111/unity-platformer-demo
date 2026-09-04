using System;
using UnityEngine;

public class Stun : MonoBehaviour
{
    private CountdownTimer _stunTimer = new CountdownTimer();
    
    public bool IsStunned { get; private set; }

    public event Action<bool> OnStunStateChanged;

    public void ApplyStun(float duration)
    {
        _stunTimer.StartCountdown(duration);
        if (!IsStunned)
        {
            IsStunned = true;
            OnStunStateChanged?.Invoke(true);
        }
    }

    private void Update()
    {
        if (IsStunned)
        {
            _stunTimer.Tick();
            if (_stunTimer.IsFinished)
            {
                IsStunned = false;
                OnStunStateChanged?.Invoke(false);
            }
        }
    }
}