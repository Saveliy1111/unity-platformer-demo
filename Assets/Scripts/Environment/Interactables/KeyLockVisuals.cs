using UnityEngine;

public class KeyLockVisual : MonoBehaviour
{
    [SerializeField] private AudioClip _unlockSound;

    public void HandleUnlocked()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySound(_unlockSound);
        }

        Destroy(gameObject);
    }
}