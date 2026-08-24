using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag(Constants.PLAYER_TAG))
        {
            Health playerHealth = collider.gameObject.GetComponent<Health>();

            if (playerHealth != null)
            {
                playerHealth.Die();
            }
        }
    }
}