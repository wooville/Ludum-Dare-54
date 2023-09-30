using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    [SerializeField]
    private Camera _gameCamera;
    public static Vector2 screenBounds;
    private float _objectWidth;
    // private float _objectHeight;
    
    // Start is called before the first frame update
    void Start()
    {
        screenBounds = _gameCamera.ScreenToWorldPoint(new Vector3(_gameCamera.scaledPixelWidth / 100, _gameCamera.scaledPixelHeight / 100, Camera.main.transform.position.z));
        _objectWidth = transform.GetComponent<BoxCollider2D>().bounds.size.x / 2;
        // _objectHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;
        // print(_screenBounds);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, screenBounds.x + _objectWidth, screenBounds.x * -1 - _objectWidth);
        // clampedPosition.y = Mathf.Clamp(clampedPosition.y, _screenBounds.y + _objectHeight, _screenBounds.y * -1 - _objectHeight);
        transform.position = clampedPosition;
    }
}
