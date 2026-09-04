using UnityEngine;

public class SwayRelay : MonoBehaviour, IHitResponder
{
    [SerializeField] private ChainSway _mainSway;

    public void OnHit(Transform attacker)
    {
        if (_mainSway != null)
        {
            _mainSway.TriggerSway(attacker);
        }
    }
}