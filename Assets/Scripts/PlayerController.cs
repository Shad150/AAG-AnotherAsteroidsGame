using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private BulletBehaviour _bB;
    [SerializeField] private GameManager _GM;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _bulletSpawner;
    [SerializeField] private GameObject _piercingBullet;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _shield;
    private bool pierce;
    private bool shield;

    [SerializeField] private float _speed;
    private bool _moving;
    private bool _movingS;
    private bool _mForward;

    [SerializeField] private float _rotationSpeed = 0.5f;
    private float _rotationDirection;

    [SerializeField] private float _maxHealth;
    [SerializeField] private float _currentHealth;
    public bool _dead;

    public bool _1Up;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _shield.SetActive(false);
    }

    private void Update()
    {
        _moving = Input.GetKey(KeyCode.W);
        //MOVEMENT
        if (Input.GetKey(KeyCode.W))
        {
            _animator.SetBool("Moving", true);
            _moving = true;
            _mForward = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            _animator.SetBool("Moving", true);
            _moving = true;
            _mForward = false;
        }
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
            _GM._aM.PlayerShot();
            if (pierce)
            {
                Instantiate(_piercingBullet, _bulletSpawner.position, _bulletSpawner.rotation);
            }
            else
            {
                Instantiate(_bullet, _bulletSpawner.position, _bulletSpawner.rotation);
            }
        }

    }

    private void FixedUpdate()
    {
        if (_moving && _mForward)
        {
            _rb.AddForce(transform.up * _speed);
            StartCoroutine(ThrustingSound());
        }
        else if (_moving && !_mForward)
        {
            _rb.AddForce(-transform.up * _speed);
            StartCoroutine(ThrustingSound());
        }

        if (_rotationDirection != 0)
        {
            _rb.AddTorque(_rotationDirection * _rotationSpeed, ForceMode2D.Force);
        }

        if (!_moving)
        {
            _animator.SetBool("Moving", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Asteroid") || collision.collider.CompareTag("EBullet") || collision.collider.CompareTag("Enemy"))
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

        if (collision.collider.CompareTag("1Up"))
        {
            _1Up = true;
            _GM._pLives++;
        }

        if (collision.collider.CompareTag("Piercing"))
        {
            StartCoroutine(PiercingAmmo());
        }
        if (collision.collider.CompareTag("Shield"))
        {
            Debug.Log("ActivatedShield");
            StartCoroutine(Shield());
        }
    }

    IEnumerator PiercingAmmo()
    {
        pierce = true;
        yield return new WaitForSeconds(10f);
        pierce = false;
    }

    IEnumerator Shield()
    {
        shield = true;
        _shield.SetActive(true);
        yield return new WaitForSeconds(10f);
        shield = false;
        _shield.SetActive(false);
    }


    IEnumerator ThrustingSound()
    {
        if (!_movingS && _moving)
        {
            _movingS = true;
            _GM._aM.PlayerThrust();
            yield return new WaitForSeconds(0.3f);
            _movingS = false;
            StartCoroutine(ThrustingSound());
        }
    }

    private void OnEnable()
    {
        pierce = false;
        shield = false;
    }
}
