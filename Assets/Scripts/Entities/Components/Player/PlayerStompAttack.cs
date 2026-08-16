using UnityEngine;

public class PlayerStompAttack : MonoBehaviour
{
    [Header ("Stomp Attack Settings")]
    [SerializeField] int _attackDamage = 1;
    [SerializeField] private float _bounceForce = 10f;

    [Header("Player References")]
    [SerializeField] private Rigidbody2D _playerRb;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Constants.ENEMY_TAG))
        {
            Health enemyHealth = collision.GetComponent<Health>();

            if (enemyHealth != null)
            {
                Transform attackerTransform = transform.parent != null ? transform.parent : transform;
                enemyHealth.TakeDamage(_attackDamage, attackerTransform);

                BouncePlayer();
            }
        }
    }

    private void BouncePlayer()
    {
        if (_playerRb != null)
        {
            _playerRb.linearVelocity = new Vector2(_playerRb.linearVelocity.x, 0f);

            _playerRb.AddForce(Vector2.up * _bounceForce, ForceMode2D.Impulse);
        }
    }
}
