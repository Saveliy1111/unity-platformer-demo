using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    private PlayerInputActions _inputActions;

    public event Action<float> OnMove;
    public event Action OnMoveCanceled;
    public event Action OnJump;
    public event Action OnJumpCanceled;
    public event Action OnSwitchCharacter;
    public event Action OnShoot;
    public event Action OnStomp;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            _inputActions = new PlayerInputActions();
            
            _inputActions.Player.Move.performed += ctx => OnMove?.Invoke(ctx.ReadValue<float>());
            _inputActions.Player.Move.canceled += ctx => OnMoveCanceled?.Invoke();
            
            _inputActions.Player.Jump.started += ctx => OnJump?.Invoke();
            _inputActions.Player.Jump.canceled += ctx => OnJumpCanceled?.Invoke();
            
            _inputActions.Player.SwitchCharacter.started += ctx => OnSwitchCharacter?.Invoke();

            _inputActions.Player.LaserShoot.started += ctx => OnShoot?.Invoke();
            _inputActions.Player.Stomp.started += ctx => OnStomp?.Invoke();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        _inputActions?.Enable();
    }

    void OnDisable()
    {
        _inputActions?.Disable();
    }
}
