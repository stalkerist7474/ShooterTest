using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Networking.Match;

public class Player : MonoBehaviour
{
    
    
    [SerializeField] private Player _player;
    [SerializeField] public float _speed;
    [SerializeField] private float _timeSmoothMove = 0.1f;
    [SerializeField] private int _maxHeath;
    [SerializeField] private int _ammunitionBullets;
    [SerializeField] public Transform _shootpoint;
    [SerializeField] private Bullet _bulletTemplate;

    public Vector2 MovementInput;

    private Vector3 _currenPosition;
    private Rigidbody2D _rigidbody2D;
    private Vector2 _smoothedMovementInput;
    private Vector2 _movementInputSmoothVelocity;
  
    private int _currentHeath;
    

    

    public event UnityAction<int, int> HealthChanged;
    public event UnityAction<int> MoneyChanged;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _currentHeath = _maxHeath;

    }



    private void FixedUpdate()
    {
        _rigidbody2D.velocity = Vector3.zero;

        if (!MovementInput.Equals(new Vector2(0, 0)))
        {

            _smoothedMovementInput = Vector2.SmoothDamp(_smoothedMovementInput, MovementInput, ref _movementInputSmoothVelocity, _timeSmoothMove);


            transform.rotation = Quaternion.LookRotation(Vector3.forward, MovementInput);


            _rigidbody2D.velocity = _smoothedMovementInput * _speed;
        }
    }

    private void OnMove(InputValue inputValue)
    {
        

        MovementInput = inputValue.Get<Vector2>();
        Debug.Log("Move");
        Debug.Log(MovementInput);
        

    }

    private void OnFire()
    {
        
        Debug.Log("Fire");
        Instantiate(_bulletTemplate, _shootpoint.position, transform.rotation);
        


    }

    public void ApplyDamage(int damage)
    {
        _currentHeath -= damage;
        HealthChanged?.Invoke(_currentHeath, _maxHeath);

        if (_currentHeath <= 0)
        {
            Debug.Log("Die");
            //Destroy(gameObject);
        }
    }
}
