using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class LevelDoor : MonoBehaviour
{
    [Header("Interaction Events")]
    public UnityEvent OnUnlockAttempt;
    public UnityEvent OnEnterAttempt;

    [Header("Visual/Audio Events")]
    public UnityEvent OnDoorUnlocked;

    private bool _isPlayerNear = false;
    private bool _isUnlocked = false;

    void Update()
    {
        if (_isPlayerNear && Input.GetKey(KeyCode.E))
        {
            if (!_isUnlocked)
            {
                OnUnlockAttempt?.Invoke();
            }
            if (_isUnlocked)
            {
                OnEnterAttempt?.Invoke();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Constants.PLAYER_TAG))
        {
            _isPlayerNear = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Constants.PLAYER_TAG))
        {
            _isPlayerNear = false;
        }
    }

    public void Unlock()
    {
      if (!_isUnlocked)
      {
        _isUnlocked = true;
        OnDoorUnlocked?.Invoke();
      }
    }
}

