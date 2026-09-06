using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [Header("Keys UI")]
    [SerializeField] private Image[] _keyImages;
    [SerializeField] private Sprite _fullKeySprite;
    [SerializeField] private Sprite _emptyKeySprite;

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
