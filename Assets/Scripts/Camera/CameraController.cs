using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Transform _player;
    [SerializeField] private float _smoothing;
    [SerializeField] private Vector2 _maxPosition;
    [SerializeField] private Vector2 _minPosition;
    [SerializeField] private bool _gizmozShowLimit;
    void Start()
    {
        transform.position = new Vector3(_player.position.x, _player.position.y, transform.position.z);
    }
    void Update()
    {
        if (transform.position != _player.position)
        {
            Vector3 targetPosition = new Vector3(_player.position.x, _player.position.y, transform.position.z);
            targetPosition.x = Mathf.Clamp(targetPosition.x, _minPosition.x, _maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, _minPosition.y, _maxPosition.y);
            transform.position = Vector3.Lerp(transform.position, targetPosition, _smoothing);
        }
    }

    //отрисовка границ камеры
    private void OnDrawGizmos()
    {
        if (_gizmozShowLimit)
        {
            Gizmos.color = Color.red;
            
            Gizmos.DrawLine(new Vector2(_maxPosition.x + 9, _maxPosition.y + 5), new Vector2(_minPosition.x - 9, _maxPosition.y + 5)); //вверх
            Gizmos.DrawLine(new Vector2(_minPosition.x - 9, _minPosition.y - 5), new Vector2(_maxPosition.x + 9, _minPosition.y - 5)); //низ

            Gizmos.DrawLine(new Vector2(_minPosition.x - 9, _minPosition.y - 5), new Vector2(_minPosition.x - 9, _maxPosition.y + 5));//лево
            Gizmos.DrawLine(new Vector2(_maxPosition.x + 9, _maxPosition.y + 5), new Vector2(_maxPosition.x + 9, _minPosition.y - 5));//право
        }
    }
}
