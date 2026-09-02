using System;
using UnityEngine;

public interface IMountable
{
    GameObject OwnerObject { get; }
    Transform SocketTransform { get; }
    bool IsMounted { get; }

    event Action<int, Transform> OnMountTookDamage;

    bool AttachRider();
    void DetachRider();
}
