using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _bulletSpawner;
    [SerializeField] private float _speed;
    private float vertical;

    private Vector2 _movement;


    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _rb.AddRelativeForce(_movement * _speed, ForceMode2D.Force);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            _rb.AddRelativeForce(_movement * -_speed, ForceMode2D.Force);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            _rb.AddRelativeForce(_movement * _speed, ForceMode2D.Force);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            _rb.AddRelativeForce(_movement * _speed, ForceMode2D.Force);
        }
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(_bullet, _bulletSpawner.position, _bulletSpawner.rotation);
        }
    }

    private void Move()
    {
        _rb.AddRelativeForce(_movement * _speed, ForceMode2D.Force);
    }

}
