using UnityEngine;

[System.Serializable]
public class CountdownTimer
{
    private float _currentTime;
    public bool IsFinished => _currentTime <= 0f;

    public void StartCountdown(float duration)
    {
        _currentTime = duration;
    }

    public void Tick()
    {
        if (_currentTime > 0f)
        {
            _currentTime -= Time.deltaTime;
        }
    }
}