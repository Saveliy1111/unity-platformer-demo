using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    [Header("Level Settings")]
    [SerializeField] private int _keysRequiredToWin = 3;

    [Header("Scene Dependencies")]
    [SerializeField] private LevelDoor _levelDoor;

    [Header("Events (For UI & Visuals)")]
    public UnityEvent<int> OnKeyCountChangedVisuals;

    private int _currentKeys = 0;
    private bool _isGameOver = false;

     void Awake()
    {
        Instance = this;
    }

    void OnEnable()
    {
        if (_levelDoor != null)
        {
            _levelDoor.OnUnlockAttempt += HandleDoorUnlockAttempt;
            _levelDoor.OnEnterAttempt += HandleDoorEnterAttempt;
        }
    }

    void OnDisable()
    {
        if (_levelDoor != null)
        {
            _levelDoor.OnUnlockAttempt -= HandleDoorUnlockAttempt;
            _levelDoor.OnEnterAttempt -= HandleDoorEnterAttempt;
        }
    }

    public void CollectKey()
    {
        _currentKeys++;
        OnKeyCountChangedVisuals?.Invoke(_currentKeys);
    }

    private  void HandleDoorUnlockAttempt()
    {
        if (_currentKeys >= _keysRequiredToWin)
        {
            _levelDoor.Unlock();
        }
        else
        {
            Debug.Log("Need more keys to unlock the door");
        }
    }

    private  void HandleDoorEnterAttempt()
    {
        if (_isGameOver) return;
        
        _isGameOver = true;
        Debug.Log("Level completed!");
        
        Invoke(nameof(RestartLevel), 2f);
    }

    public void HandlePlayerDeath()
    {
        if (_isGameOver) return;

        _isGameOver = true;
        Invoke(nameof(RestartLevel), 2f);
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
