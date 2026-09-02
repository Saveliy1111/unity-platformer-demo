using UnityEngine;

public class MountedState : IMountState
{
    private MountController _mountController;

    public MountedState(MountController context)
    {
        _mountController = context;
    }

    public void Enter()
    {
        if (_mountController.CurrentMount == null || !_mountController.CurrentMount.AttachRider())
        {
            _mountController.ChangeState(_mountController.DismountedState);
            return;
        }

        TogglePhysics(false);

        _mountController.transform.SetParent(_mountController.CurrentMount.SocketTransform);
        _mountController.transform.localPosition = Vector3.zero;

        _mountController.CurrentMount.OnMountTookDamage += HandleSharedDamage;
        _mountController.OnMountVisuals?.Invoke();

        if (InputManager.Instance != null)
        {
            InputManager.Instance.TriggerForceSwitch(_mountController.CurrentMount.OwnerObject);
        }
    }

    public void Exit()
    {
        if (_mountController.CurrentMount != null)
        {
            _mountController.CurrentMount.OnMountTookDamage -= HandleSharedDamage;
            _mountController.CurrentMount.DetachRider();
            _mountController.CurrentMount = null;
        }

        TogglePhysics(true);

        _mountController.transform.SetParent(null);
        
        _mountController.OnDismountVisuals?.Invoke();
    }

    public void HandleInteract()
    {
        _mountController.ChangeState(_mountController.DismountedState);
        
        if (InputManager.Instance != null)
        {
            InputManager.Instance.TriggerForceSwitch(_mountController.OwnerObject);
        }
    }

    public void HandleSwitchCharacter()
    {
        _mountController.ChangeState(_mountController.DismountedState);
    }

    private void TogglePhysics(bool enable)
    {
        if (enable)
        {
            _mountController.Rigidbody.bodyType = RigidbodyType2D.Dynamic;
            _mountController.Collider.enabled = true;
            _mountController.Movement.UnlockMovement();
        }
        else
        {
            _mountController.Rigidbody.bodyType = RigidbodyType2D.Kinematic;
            _mountController.Rigidbody.linearVelocity = Vector2.zero;
            _mountController.Collider.enabled = false;
            _mountController.Movement.LockMovement();
        }
    }

    private void HandleSharedDamage(int damage, Transform attacker)
    {
        if (_mountController.PlayerHealth != null)
        {
            _mountController.PlayerHealth.TakeDamage(damage, attacker);
        }
    }
}