using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [Header("Jump Pad Settings")]
    [SerializeField] private float _bounceForce = 15f;
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag(Constants.PLAYER_TAG))
        {
            Rigidbody2D playerRigidBody = collider.GetComponent<Rigidbody2D>();

            if(playerRigidBody != null)
            {
                playerRigidBody.linearVelocity = new Vector2(playerRigidBody.linearVelocity.x, _bounceForce);
            }
        }
    }
}
