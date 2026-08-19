using UnityEngine;

[System.Serializable]
public class CooldownTimer
{
    private float _currentTime;

    public void StartCooldown(float duration)
    {
        _currentTime = duration;
    }

    public bool Tick()
    {
        if (_currentTime > 0f)
        {
            _currentTime -= Time.deltaTime;
        }

        return _currentTime <= 0f;
    }
}