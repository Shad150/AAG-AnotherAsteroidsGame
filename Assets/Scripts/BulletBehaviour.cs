using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    [SerializeField] private float _speed;

    private void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Destroy());
    }

    private void Update()
    {
        gameObject.transform.Translate(Vector3.up * _speed *Time.deltaTime);
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
            Destroy(gameObject);
    }
}
