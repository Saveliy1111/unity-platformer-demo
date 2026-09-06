using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Sign : MonoBehaviour
{
    [SerializeField] private GameObject _textPopup;

    private int _playersInRange = 0;

    void Awake()
    {
        if (_textPopup != null)
        {
            _textPopup.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag(Constants.PLAYER_TAG)) return;

        _playersInRange++;
        UpdatePopupVisibility();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag(Constants.PLAYER_TAG)) return;

        _playersInRange = Mathf.Max(0, _playersInRange - 1);
        UpdatePopupVisibility();
    }

    private void UpdatePopupVisibility()
    {
        if (_textPopup == null) return;
        _textPopup.SetActive(_playersInRange > 0);
    }
}