using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private BulletBehaviour _bB;
    [SerializeField] private GameManager _GM;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _bulletSpawner, _bulletSpawner2, _bulletSpawner3;
    [SerializeField] private GameObject _piercingBullet;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _shield;
    [SerializeField] private bool pierce;
    [SerializeField] private bool shield;
    [SerializeField] private bool triple;
    [SerializeField] private Image[] _ScreenEdges;
    [SerializeField] private Color _currentPU;

    [SerializeField] private float _speed;
    private bool _moving;
    private bool _movingS;
    private bool _mForward;

    [SerializeField] private float _rotationSpeed = 0.5f;
    private float _rotationDirection;

    [SerializeField] private float _maxHealth;
    [SerializeField] private float _currentHealth;
    public bool _dead;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        //_shield.SetActive(false);
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
            if (triple && pierce)
            {
                _GM._aM.PiercingShot();
                Instantiate(_piercingBullet, _bulletSpawner.position, _bulletSpawner.rotation);
                Instantiate(_piercingBullet, _bulletSpawner2.position, _bulletSpawner2.rotation);
                Instantiate(_piercingBullet, _bulletSpawner3.position, _bulletSpawner3.rotation);
            }
            else if (pierce)
            {
                _GM._aM.PiercingShot();
                Instantiate(_piercingBullet, _bulletSpawner.position, _bulletSpawner.rotation);
            }
            else if (triple)
            {
                _GM._aM.PlayerShot();
                Instantiate(_bullet, _bulletSpawner.position, _bulletSpawner.rotation);
                Instantiate(_bullet, _bulletSpawner2.position, _bulletSpawner2.rotation);
                Instantiate(_bullet, _bulletSpawner3.position, _bulletSpawner3.rotation);
            }
            else
            {
                _GM._aM.PlayerShot();
                Instantiate(_bullet, _bulletSpawner.position, _bulletSpawner.rotation);
            }
        }
        _shield.transform.position = transform.position;

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
        if (!shield)
        {
            if (collision.collider.CompareTag("Asteroid") || collision.collider.CompareTag("EBullet") || collision.collider.CompareTag("Enemy") || collision.collider.CompareTag("EnemySin"))
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
        

        if (collision.collider.CompareTag("1Up"))
        {
            _GM._aM.OneUp();
            _GM._pLives++;
            _GM.OneUPTextUpdate();
        }

        if (collision.collider.CompareTag("Piercing"))
        {
            _GM._aM.PowerUp();
            if (pierce)
            {
                StopCoroutine(PiercingAmmo());
                pierce = false;
                StopCoroutine(SecondsToPUEnd());
                StartCoroutine(PiercingAmmo());
            }
            else
            {
                StartCoroutine(PiercingAmmo());
            }
        }
        if (collision.collider.CompareTag("ShieldPU"))
        {
            _GM._aM.PowerUp();
            if (shield)
            {
                StopCoroutine(Shield());
                shield = false;
                _shield.SetActive(true);
                StopCoroutine(SecondsToPUEnd());
                StartCoroutine(Shield());
            }
            else
            {
                StartCoroutine(Shield());
            }
        }
        if (collision.collider.CompareTag("TripleShot"))
        {
            _GM._aM.PowerUp();
            if (triple)
            {
                StopCoroutine(TripleShot());
                triple = false;
                StopCoroutine(SecondsToPUEnd());
                StartCoroutine(TripleShot());
            }
            else
            {
                StartCoroutine(TripleShot());
            }
        }
        
    }

    public IEnumerator PiercingAmmo()
    {
        pierce = true;

        for (int i = 0; i < _ScreenEdges.Length; i++)
        {
            _ScreenEdges[i].color = Color.magenta;
        }
        _currentPU = Color.magenta;
        yield return new WaitForSeconds(7f);
        StartCoroutine(SecondsToPUEnd());
        yield return new WaitForSeconds(3f);

        pierce = false;
    }

    public IEnumerator TripleShot()
    {
        triple = true;
        _bullet.GetComponent<SpriteRenderer>().color = Color.blue;
        for (int i = 0; i < _ScreenEdges.Length; i++)
        {
            _ScreenEdges[i].color = Color.blue;
        }
        _currentPU = Color.blue;
        yield return new WaitForSeconds(7f);
        StartCoroutine(SecondsToPUEnd());
        yield return new WaitForSeconds(3f);
        _bullet.GetComponent<SpriteRenderer>().color = Color.white;
        triple = false;
    }

    public IEnumerator Shield()
    {
        shield = true;

        for (int i = 0; i < _ScreenEdges.Length; i++)
        {
            _ScreenEdges[i].color = Color.cyan;
        }
        _currentPU = Color.cyan;
        _shield.SetActive(true);
        yield return new WaitForSeconds(7f);
        StartCoroutine(SecondsToPUEnd());
        yield return new WaitForSeconds(3f);
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

    IEnumerator SecondsToPUEnd()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < _ScreenEdges.Length; i++)
        {
            _ScreenEdges[i].color = _currentPU;
        }
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < _ScreenEdges.Length; i++)
        {
            _ScreenEdges[i].color = Color.white;
        }
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < _ScreenEdges.Length; i++)
        {
            _ScreenEdges[i].color = _currentPU;
        }
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < _ScreenEdges.Length; i++)
        {
            _ScreenEdges[i].color = Color.white;
        }
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < _ScreenEdges.Length; i++)
        {
            _ScreenEdges[i].color = _currentPU;
        }
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < _ScreenEdges.Length; i++)
        {
            _ScreenEdges[i].color = Color.white;
        }
    }

    private void OnEnable()
    {
        for (int i = 0; i < _ScreenEdges.Length; i++)
        {
            _ScreenEdges[i].color = Color.white;
        }
        _bullet.GetComponent<SpriteRenderer>().color = Color.white;
        triple = false;
        pierce = false;
        shield = false;
        _movingS = false;
        _shield.SetActive(false);
    }
}
