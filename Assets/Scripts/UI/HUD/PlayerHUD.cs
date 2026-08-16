using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [Header("Hearts UI")]
    [SerializeField] private Image[] _heartImages;
    [SerializeField] private Sprite _fullHeartSprite;
    [SerializeField] private Sprite _emptyHeartSprite;

    [Header("Keys UI")]
    [SerializeField] private Image[] _keyImages;
    [SerializeField] private Sprite _fullKeySprite;
    [SerializeField] private Sprite _emptyKeySprite;

    public void UpdateHearts(int currentHealth)
    {
        for (int i = 0; i < _heartImages.Length; i++)
        {
            if (i < currentHealth)
            {
                _heartImages[i].sprite = _fullHeartSprite;

            }
            else
            {
                _heartImages[i].sprite = _emptyHeartSprite;
            }
        }

    }

    public void UpdateKeys(int currentKeys)
    {
        for (int i = 0; i < _keyImages.Length; i++)
        {
            if (i < currentKeys)
            {
                _keyImages[i].sprite = _fullKeySprite;

            }
            else
            {
                _keyImages[i].sprite = _emptyKeySprite;
            }
        }

    }
}
