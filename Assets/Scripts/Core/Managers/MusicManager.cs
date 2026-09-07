using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    [SerializeField] private AudioSource _musicSource;

    private AudioClip _currentClip;

    void Awake()
    {
        Instance = this;
    }

    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        if (clip == null) return;
        if (_currentClip == clip && _musicSource.isPlaying) return;

        _currentClip = clip;
        _musicSource.clip = clip;
        _musicSource.loop = loop;
        _musicSource.Play();
    }

    public void StopMusic()
    {
        _musicSource.Stop();
        _currentClip = null;
    }

    public void SetVolume(float volume)
    {
        _musicSource.volume = Mathf.Clamp01(volume);
    }
}