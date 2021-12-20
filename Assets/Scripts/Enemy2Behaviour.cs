using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Behaviour : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private SpriteRenderer _sR;
    [SerializeField] private GameManager _GM;
    [SerializeField] private GameObject _enemy2;
    [SerializeField] private Transform _direction;
    [SerializeField] private float _speed;
    [SerializeField] private float _health = 30f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sR = GetComponent<SpriteRenderer>();
        _GM = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        _enemy2.transform.Rotate(new Vector3(0, 0, 1));
    }

    public void SetTrayectory(Vector2 direction)
    {
        _rb.AddForce(direction * _speed);

        Destroy(gameObject, _health);
    }

    IEnumerator Behaviour2()
    {
        float moveTime = Random.Range(0.5f, 3f);
        _direction.transform.Rotate(new Vector3(0, 0, Random.Range(0, 360)));
        yield return new WaitForSeconds(moveTime);
        _rb.velocity = Vector2.zero;
        _rb.AddForce(_direction.transform.up * 2, ForceMode2D.Impulse);
        StartCoroutine(Behaviour2());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Bullet") || collision.collider.CompareTag("Shield"))
        {
            _GM._aM.PlayerExplosion();
            FindObjectOfType<GameManager>().EnemySinDestroyed(this);
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(Behaviour2());
    }
}
