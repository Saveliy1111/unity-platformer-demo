using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Weight))]
public class PushableObject : MonoBehaviour, IPushable
{
    private Rigidbody2D _rigidbody;
    private Weight _weight;
    private bool _isPushedThisFrame;
    private float _pushVelocityX;

    public int WeightLevel => _weight != null ? _weight.WeightLevel : 0;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _weight = GetComponent<Weight>();

        LockXAxis();
    }

    void FixedUpdate()
    {
        if (_isPushedThisFrame)
        {
            UnlockXAxis();
            _rigidbody.linearVelocity = new Vector2(_pushVelocityX, _rigidbody.linearVelocity.y);
            
            _isPushedThisFrame = false;
        }
        else
        {
            LockXAxis();
        }
    }

    public void Push(float velocityX)
    {
        _isPushedThisFrame = true;
        _pushVelocityX = velocityX;
    }

    private void LockXAxis()
    {
        _rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
    }

    private void UnlockXAxis()
    {
        _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
