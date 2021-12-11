using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameManager _GM;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _bulletSpawner;

    [SerializeField] private float _speed;
    private bool _moving;

    [SerializeField] private float _rotationSpeed = 0.5f;
    private float _rotationDirection;

    [SerializeField] private float _maxHealth;
    [SerializeField] private float _currentHealth;
    private bool _dead;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _moving = Input.GetKey(KeyCode.W);
        //MOVEMENT
        if (Input.GetKey(KeyCode.W))
        {
            _moving = true;
        }
        //if (Input.GetKey(KeyCode.S))
        //{
        //    _moving = true;
        //}
        if (Input.GetKey(KeyCode.A))
        {
            _rotationDirection = 1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            _rotationDirection = - 1f;
        }
        if(!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            _rotationDirection = 0f;
        }

        //SHOOTING
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(_bullet, _bulletSpawner.position, _bulletSpawner.rotation);
        }
    }

    private void FixedUpdate()
    {
        if (_moving)
        {
            _rb.AddForce(transform.up * _speed);
        }

        if (_rotationDirection != 0)
        {
            _rb.AddTorque(_rotationDirection * _rotationSpeed, ForceMode2D.Force);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Asteroid"))
        {
            _dead = true;
            if (_dead)
            {

                _rb.velocity = Vector3.zero;
                _rb.angularVelocity = 0f;

                gameObject.SetActive(false);
                _GM.PlayerDead();
            }
        }
    }
}
