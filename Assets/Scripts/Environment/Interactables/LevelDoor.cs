using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class LevelDoor : MonoBehaviour
{
    private bool _isPlayerNear = false;
    private bool _isUnlocked = false;

    public event Action OnUnlockAttempt;
    public event Action OnEnterAttempt;

    [Header("Visual/Audio Events")]
    public UnityEvent OnDoorUnlocked;

    void Update()
    {
        if (_isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            if (!_isUnlocked)
            {
                OnUnlockAttempt?.Invoke();
            }
            else
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