using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehaviour : MonoBehaviour
{
    [SerializeField] private GameManager _GM;
    [SerializeField] private SpriteRenderer _sR;
    [SerializeField] private Rigidbody2D _rb;

    [SerializeField] private Sprite[] _sprites;
    public float _size = 1f;
    public float _minSize = 0.3f;
    public float _maxSize = 2f;

    private float _speed = 10f;

    private float _health = 50f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sR = GetComponent<SpriteRenderer>();
        _GM = GetComponent<GameManager>();
    }
    void Start()
    {
        _sR.sprite = _sprites[Random.Range(0, _sprites.Length)];        //Sprite del asteroide
        transform.eulerAngles = new Vector3(0f, 0f, Random.value * 360f);       //Rotacion del asteroide
        transform.localScale = Vector3.one * _size;     //Tamaño del asteroide

        _rb.mass = _size * 2;

    }

    public void SetTrayectory(Vector2 direction)
    {
        _rb.AddForce(direction * _speed);

        Destroy(gameObject, _health);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Bullet") || collision.collider.CompareTag("Shield"))
        {
            if(_size * 0.5 >= _minSize)
            {
                SplittedAsteroid();
                SplittedAsteroid();
            }

            //_GM.AsteroidDestroyed(this);
            FindObjectOfType<GameManager>().AsteroidDestroyed(this);
            Destroy(gameObject);
        }
    }

    //Division del asteroide
    private void SplittedAsteroid()
    {
        Vector2 position = transform.position;
        position += Random.insideUnitCircle * 0.5f;

        AsteroidBehaviour half = Instantiate(this, position, this.transform.rotation);
        half._size = _size * 0.5f;

        half.SetTrayectory(Random.insideUnitCircle.normalized * _speed);

    }
}
