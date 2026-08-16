using UnityEngine;

public class YellowKey : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag(Constants.PLAYER_TAG))
        {
            PlayerInventory playerInventory = collider.gameObject.GetComponent<PlayerInventory>();

            if(playerInventory != null)
            {
                playerInventory.AddKey();
                Destroy(gameObject);
            }
        }
    }
}
