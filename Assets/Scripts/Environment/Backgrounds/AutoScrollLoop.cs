using UnityEngine;

public class AutoScrollLoop : MonoBehaviour
{
    [SerializeField] private float _scrollSpeed = -1f;
    
    private float _singleTileWidth;
    private Vector3 _startPosition;

    void Start()
    {
        _startPosition = transform.localPosition;
        _singleTileWidth = GetComponent<SpriteRenderer>().sprite.bounds.size.x;
    }

    void Update()
    {
        transform.Translate(new Vector3(_scrollSpeed * Time.deltaTime, 0, 0), Space.Self);

        if (transform.localPosition.x < _startPosition.x - _singleTileWidth)
        {
            transform.localPosition 
                = new Vector3(transform.localPosition.x + _singleTileWidth, transform.localPosition.y, transform.localPosition.z);
        }
        else if (transform.localPosition.x > _startPosition.x + _singleTileWidth)
        {
            transform.localPosition 
                = new Vector3(transform.localPosition.x - _singleTileWidth, transform.localPosition.y, transform.localPosition.z);
        }
    }
}