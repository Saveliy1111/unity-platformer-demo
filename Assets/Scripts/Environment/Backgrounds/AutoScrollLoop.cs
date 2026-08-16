using UnityEngine;

public class AutoScrollLoop : MonoBehaviour
{
    [SerializeField] private float _scrollSpeed = -1f;
    
    private float _singleTileWidth;
    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
        _singleTileWidth = GetComponent<SpriteRenderer>().sprite.bounds.size.x;
    }

    private void Update()
    {
        transform.Translate(new Vector3(_scrollSpeed * Time.deltaTime, 0, 0));

        if (transform.position.x < _startPosition.x - _singleTileWidth)
        {
            transform.position 
                = new Vector3(transform.position.x + _singleTileWidth, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > _startPosition.x + _singleTileWidth)
        {
            transform.position 
                = new Vector3(transform.position.x - _singleTileWidth, transform.position.y, transform.position.z);
        }
    }
}