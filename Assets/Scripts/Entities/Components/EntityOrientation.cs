using UnityEngine;

public class EntityOrientation : MonoBehaviour, IEntityOrientation
{
    [Tooltip("True if the sprite is facing left, false if it is facing right")]
    [SerializeField] private bool _facesLeftByDefault = true;

    public bool FacesLeftByDefault => _facesLeftByDefault;
}