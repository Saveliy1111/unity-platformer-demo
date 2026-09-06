using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class YellowKey : MonoBehaviour
{
    [Header("Visual/Audio Events")]
    public UnityEvent OnCollectedVisuals;

    private bool _isCollected = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isCollected) return;
        if (!collision.gameObject.CompareTag(Constants.PLAYER_TAG)) return;

        _isCollected = true;

        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.CollectKey();
        }

        OnCollectedVisuals?.Invoke();
        Destroy(gameObject);
    }
}
