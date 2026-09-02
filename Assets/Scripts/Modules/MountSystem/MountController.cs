using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(Movement))]
public class MountController : MonoBehaviour, IPlayerActiveListener
{
    [Header("Scanning")]
    public Transform ScanPoint;
    public Vector2 ScanBoxSize = new Vector2(0.85f, 0.85f);
    public LayerMask MountableLayer;

    [Header("Visual Events")]
    public UnityEvent OnMountVisuals;
    public UnityEvent OnDismountVisuals;

    public Rigidbody2D Rigidbody { get; private set; }
    public Collider2D Collider { get; private set; }
    public Movement Movement { get; private set; }
    public GameObject OwnerObject => gameObject;
    public Health PlayerHealth { get; private set; }

    public IMountable CurrentMount { get; set; }
    public bool IsActivePlayer { get; private set; }

    private IMountState _currentState;
    public DismountedState DismountedState { get; private set; }
    public MountedState MountedState { get; private set; }

    private float _lastInputTime;

    void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Collider = GetComponent<Collider2D>();
        Movement = GetComponent<Movement>();
        PlayerHealth = GetComponent<Health>();

        DismountedState = new DismountedState(this);
        MountedState = new MountedState(this);
    }

    void Start()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnInteract += OnInteractPressed;
            InputManager.Instance.OnSwitchCharacter += OnSwitchCharacterPressed;
        }

        if (PlayerHealth != null)
        {
            PlayerHealth.OnDeath += HandleDeath;
        }
        
        ChangeState(DismountedState);
    }

    void OnDestroy()
    {
         if (InputManager.Instance != null)
        {
            InputManager.Instance.OnInteract -= OnInteractPressed;
            InputManager.Instance.OnSwitchCharacter -= OnSwitchCharacterPressed;
        }

        if (PlayerHealth != null)
        {
            PlayerHealth.OnDeath -= HandleDeath;
        }
    }

    public void OnActiveStateChanged(bool isActive)
    {
        IsActivePlayer = isActive;
    }

    private void OnInteractPressed()
    {
        if (Time.time - _lastInputTime < 0.2f) return;
        _lastInputTime = Time.time;
        _currentState?.HandleInteract();
    }

    private void OnSwitchCharacterPressed()
    {
        _currentState?.HandleSwitchCharacter();
    }

    public void ChangeState(IMountState newState)
    {
        Debug.Log($"<color=magenta>[MountController]</color> Смяна на състоянието към: {newState.GetType().Name}");
        _currentState?.Exit();
        _currentState = newState;
        _currentState?.Enter();
    }

    private void HandleDeath()
    {
        if (_currentState == MountedState)
        {
            ChangeState(DismountedState);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (ScanPoint != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(ScanPoint.position, ScanBoxSize);
        }
    }
}