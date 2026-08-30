using UnityEngine;

public class StompVisuals : MonoBehaviour
{
    private Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void TriggerStompFall()
    {
        _animator.SetBool("isStomping", true);
        _animator.Play("Green'kor_StompFall");
    }

    public void TriggerStompImpact()
    {
        _animator.SetBool("isStomping", false);
        _animator.SetTrigger("stompImpact");
    }
}
