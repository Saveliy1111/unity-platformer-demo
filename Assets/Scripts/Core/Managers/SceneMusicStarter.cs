using UnityEngine;

public class SceneMusicStarter : MonoBehaviour
{
    [SerializeField] private AudioClip _musicClip;

    void Start()
    {
        if (MusicManager.Instance != null)
        {
            MusicManager.Instance.PlayMusic(_musicClip);
        }
    }
}