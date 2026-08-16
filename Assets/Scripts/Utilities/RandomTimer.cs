using UnityEngine;

[System.Serializable]
public class RandomTimer
{
    [SerializeField] private float _minTime = 1f;
    [SerializeField] private float _maxTime = 3f;
    
    private float _currentTime;

    public void Start()
    {
        _currentTime = Random.Range(_minTime, _maxTime);
    }
    
    public bool Tick()
    {
        _currentTime -= Time.deltaTime;
        return _currentTime <= 0f;
    }
}