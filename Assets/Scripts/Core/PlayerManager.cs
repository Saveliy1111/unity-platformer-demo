using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

public class PlayerManager : MonoBehaviour
{
    [Header("Player References")]
    [SerializeField] private GameObject _player1;
    [SerializeField] private GameObject _player2;

    [Header("Camera Settings")]
    [SerializeField] private CinemachineCamera _virtualCamera;

    private GameObject _activePlayer;

    void Start()
    {
        _activePlayer = _player1;

        NotifyActiveState(_player1, true);
        NotifyActiveState(_player2, false);
        UpdateCameraTarget();

        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnSwitchCharacter += HandleSwitchCharacter;
        }
    }

    void OnDestroy()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnSwitchCharacter -= HandleSwitchCharacter;
        }
    }

    private void HandleSwitchCharacter()
    {
        NotifyActiveState(_activePlayer, false);
        
        _activePlayer = (_activePlayer == _player1) ? _player2 : _player1;
        
        NotifyActiveState(_activePlayer, true);
        UpdateCameraTarget();
    }

    private void NotifyActiveState(GameObject player, bool isActive)
    {
        if (player == null) return;
    
        IPlayerActiveListener[] listeners = player.GetComponentsInChildren<IPlayerActiveListener>();
        foreach (var listener in listeners)
        {
            listener.OnActiveStateChanged(isActive);
        }
    }

    private void UpdateCameraTarget()
    {
        if (_virtualCamera != null && _activePlayer != null)
        {
            _virtualCamera.Target.TrackingTarget = _activePlayer.transform;
        }
    }
}
