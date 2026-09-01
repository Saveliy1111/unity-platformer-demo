using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("Level Settings")]
    [SerializeField] private int _keysRequiredToWin = 3;

    [Header("Scene Dependencies")]
    [SerializeField] private LevelDoor _levelDoor;

    private int _currentKeys = 0;
    private bool _isGameOver = false;

    public void UpdateKeyCount(int currentKeys)
    {
        _currentKeys = currentKeys;
    }

    public void HandleDoorUnlockAttempt()
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

    public void HandleDoorEnterAttempt()
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
        Debug.Log("Player is dead!");
        Invoke(nameof(RestartLevel), 2f);
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
