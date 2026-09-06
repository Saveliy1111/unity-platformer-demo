using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonTextAnimator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("References")]
    [SerializeField] private Transform _textTransform;

    [Header("Hover Settings")]
    [SerializeField] private float _hoverScale = 1.15f;
    [SerializeField] private float _hoverDuration = 0.15f;

    [Header("Press Settings")]
    [SerializeField] private float _pressScale = 0.9f;
    [SerializeField] private float _pressDuration = 0.1f;

    private Vector3 _originalScale;

    void Awake()
    {
        if (_textTransform != null)
        {
            _originalScale = _textTransform.localScale;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        AnimateTo(_originalScale * _hoverScale, _hoverDuration);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        AnimateTo(_originalScale, _hoverDuration);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        AnimateTo(_originalScale * _pressScale, _pressDuration);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        AnimateTo(_originalScale * _hoverScale, _hoverDuration);
    }

    private void AnimateTo(Vector3 targetScale, float duration)
    {
        if (_textTransform == null) return;

        _textTransform.DOKill();
        _textTransform.DOScale(targetScale, duration);
    }
}