using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    void Awake()
    {
        _slider = GetComponent<Slider>();
    }
    public void InitializeMaxHealth(int maxHealth)
    {
        if (_slider == null) _slider = GetComponent<Slider>();
        _slider.maxValue = maxHealth;
        _slider.value = maxHealth;
    }

    public void UpdateHealth(int currentHealth)
    {
        _slider.value = currentHealth;
    }
}