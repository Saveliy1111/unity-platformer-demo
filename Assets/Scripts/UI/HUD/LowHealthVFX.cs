using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class LowHealthVFX : MonoBehaviour
{
    private Image _vignetteImage;

    void Start()
    {
        _vignetteImage = GetComponent<Image>();
        
        if (_vignetteImage != null)
        {
            _vignetteImage.enabled = false;
        }
    }

    public void CheckHealth(int currentHealth)
    {
        if (_vignetteImage == null) return;

        // Включваме картинката (и съответно шейдъра), само ако кръвта е 1
        if (currentHealth == 1)
        {
            _vignetteImage.enabled = true;
        }
        else
        {
            _vignetteImage.enabled = false;
        }
    }
}
