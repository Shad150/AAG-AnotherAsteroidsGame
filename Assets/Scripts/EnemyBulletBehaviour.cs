using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletBehaviour : MonoBehaviour
{
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    [SerializeField] private float _speed;

    private void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Destroy());
        StartCoroutine(ColorShift());
    }

    private void Update()
    {
        gameObject.transform.Translate(Vector3.up * _speed *Time.deltaTime);
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(15f);
        Destroy(gameObject);
    }

    private IEnumerator ColorShift()
    {
        Debug.Log("BulletColorShift");
        _sr.color = Color.yellow;
        yield return new WaitForSeconds(.1f);
        _sr.color = Color.red;
        yield return new WaitForSeconds(.1f);
        StartCoroutine(ColorShift());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
