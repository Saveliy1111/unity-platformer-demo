using System.Collections;
using UnityEngine;

public class HitStop : MonoBehaviour
{
    private bool _isFrozen;

    public void TriggerHitStop(float duration)
    {
        if (_isFrozen) return;
        StartCoroutine(FreezeTimeRoutine(duration));
    }

    private IEnumerator FreezeTimeRoutine(float duration)
    {
        _isFrozen = true;

        float originalTimeScale = Time.timeScale;

        Time.timeScale = 0;

        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = originalTimeScale;
        _isFrozen = false;
    }
}
