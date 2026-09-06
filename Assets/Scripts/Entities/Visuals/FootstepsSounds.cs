using UnityEngine;
 
public class FootstepSounds : MonoBehaviour
{
    [SerializeField] private AudioClip[] _footstepSounds;
 
    public void PlayFootstepSound()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayRandomSound(_footstepSounds);
        }
    }
}
