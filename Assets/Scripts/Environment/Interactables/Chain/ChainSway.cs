using DG.Tweening;
using UnityEngine;

public class ChainSway : MonoBehaviour
{
    [Header("Sway Settings")]
    [SerializeField] private float _swayDuration = 1.5f;
    [SerializeField] private Vector3 _swayPunchAngle = new Vector3(0f, 0f, 45f);
    [SerializeField] private int _vibrato = 5;
    [SerializeField] private float _elasticity = 0.5f;

    public void TriggerSway(Transform attacker)
    {
        transform.DOKill(true);

        float direction = (attacker != null && attacker.position.x > transform.position.x) ? -1f : 1f;
        Vector3 finalPunch = _swayPunchAngle * direction;

        transform.DOPunchRotation(finalPunch, _swayDuration, _vibrato, _elasticity);
    }
}
