using UnityEngine;

public class RiderVisuals : MonoBehaviour
{
    private Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    public void SetMountStart()
    {
        _animator.SetBool("isMounted", true);
    }

    public void SetMountStop()
    {
        _animator.SetBool("isMounted", false);
    }

    public void TriggerMountedHurt()
    {
        _animator.SetTrigger("mountedHurt");
    }
}
