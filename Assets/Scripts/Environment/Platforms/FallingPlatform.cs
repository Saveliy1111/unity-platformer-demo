using UnityEngine;
using System.Collections;

public class FallingPlatform : MonoBehaviour
{
    [Header("Falling Platform Settings")]
    [SerializeField] private float _fallDelay = 0.5f;
    [SerializeField] private float _destroyDelay = 2f;
    [SerializeField] private float _shakeMagnitude = 0.5f;

    private Rigidbody2D _rigidBody;
    private bool _isFalling = false;
    private Vector2 _initialPosition;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _initialPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Constants.PLAYER_TAG) && !_isFalling)
        {
            StartCoroutine(FallSequence());
        }
    }

    private IEnumerator FallSequence()
    {
        _isFalling = true;
        float elapsedTime = 0f;

        while (elapsedTime < _fallDelay)
        {
            float offsetX = Random.Range(-_shakeMagnitude, _shakeMagnitude);
            float offsetY = Random.Range(-_shakeMagnitude, _shakeMagnitude);

            transform.position = _initialPosition + new Vector2(offsetX, offsetY);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = _initialPosition;
        _rigidBody.bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Collider2D>().enabled = false;

        yield return new WaitForSeconds(_destroyDelay);
        Destroy(gameObject);

    }
}
