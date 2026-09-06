using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class TextColorPulse : MonoBehaviour
{
    [Header("Pulse Colors")]
    [SerializeField] private Color _colorA;
    [SerializeField] private Color _colorB;

    [Header("Pulse Settings")]
    [SerializeField] private float _pulseSpeed = 1f;

    private TMP_Text _text;

    void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        float t = (Mathf.Sin(Time.time * _pulseSpeed) + 1f) * 0.5f;
        _text.color = Color.Lerp(_colorA, _colorB, t);
    }
}