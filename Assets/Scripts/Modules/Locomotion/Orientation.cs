using UnityEngine;

public class Orientation : MonoBehaviour, IOrientation
{
    [Tooltip("True if the sprite is facing left, false if it is facing right")]
    [SerializeField] private bool _facesLeftByDefault = true;

    public bool FacesLeftByDefault => _facesLeftByDefault;
    public float FacingDirection { get; private set; }
    public Vector2 ForwardVector => new Vector2(FacingDirection, 0f);

    private void Start()
    {
        FacingDirection = _facesLeftByDefault ? -1f : 1f;
    }

    public void SetFacingDirection(float directionX)
    {
        if (Mathf.Abs(directionX) > 0.01f)
        {
            FacingDirection = Mathf.Sign(directionX);
        }
    }
}