using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private GameManager _GM;
    [SerializeField] private SpriteRenderer _sR;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _fakeTarget;
    [SerializeField] private Transform _bulletSpawner;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Animator _animator;

    [SerializeField] private Sprite[] _sprites;

    private float _speed = 0.3f;

    //private float _impulseForce;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sR = GetComponent<SpriteRenderer>();
        _GM = FindObjectOfType<GameManager>();
    }
    void Start()
    {
        _sR.sprite = _sprites[Random.Range(0, _sprites.Length)];        //Sprite del asteroide
        StartCoroutine(FindPlayer());
        InvokeRepeating("Shot", 2f , 2f);

        if (_sR.sprite == _sprites[1])
        {
            _animator.SetBool("Enemy1", true);
        }
        else
        {
            _animator.SetBool("Enemy1", false);
        }
    }

    private void FixedUpdate()
    {
        Vector3 dir = _player.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //transform.LookAt(_player.transform);

        _rb.AddForce(dir * _speed, ForceMode2D.Force);

        _animator.SetBool("Moving", true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            _GM._aM.PlayerExplosion();
            FindObjectOfType<GameManager>().EnemyDestroyed(this);
            Destroy(gameObject);
        }
    }

    private void Shot()
    {
        _GM._aM.EnemyShot();
        Instantiate(_bullet, _bulletSpawner.position, _bulletSpawner.rotation);
    }

    private IEnumerator FindPlayer()
    {
        if (_GM._playerDead)
        {
            _player = _fakeTarget;
            _speed = 0;
            yield return new WaitForSeconds(3f);
            _speed = 0.3f;
            StartCoroutine(FindPlayer());
        }
        else
        {
            _player = _GM._player;
        }
    }

}

