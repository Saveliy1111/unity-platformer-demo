using UnityEngine;

public class DismountedState : IMountState
{
    private MountController _mountController;

    public DismountedState(MountController context)
    {
        _mountController = context;
    }

    public void Enter() { }
    
    public void Exit() { }

    public void HandleInteract()
    {
        if (!_mountController.IsActivePlayer) return;

        Collider2D hit = Physics2D.OverlapBox(_mountController.ScanPoint.position, _mountController.ScanBoxSize, 0f, _mountController.MountableLayer);
        if (hit != null && hit.TryGetComponent(out IMountable mountable))
        {
            if (!mountable.IsMounted)
            {
                _mountController.CurrentMount = mountable;
                _mountController.ChangeState(_mountController.MountedState);
            }
        }
    }

    public void HandleSwitchCharacter() { }
}