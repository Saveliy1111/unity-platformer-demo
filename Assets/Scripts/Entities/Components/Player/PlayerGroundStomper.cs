using UnityEngine;

[RequireComponent(typeof(EntityGroundStomp))]
public class PlayerGroundStomper : MonoBehaviour, IPlayerActiveListener
{
    private EntityGroundStomp _groundStomp;
    private bool _isActive = false;

    void Awake()
    {
        _groundStomp = GetComponent<EntityGroundStomp>();
    }

    void Start()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnStomp += HandleStomp;
        }
    }

    void OnDestroy()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnStomp -= HandleStomp;
        }
    }

    public void OnActiveStateChanged(bool isActive)
    {
        _isActive = isActive;
    }

    private void HandleStomp()
    {
        if (!_isActive) return;
        
        _groundStomp.TryStomp();
    }
}
