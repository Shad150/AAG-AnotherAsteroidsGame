using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletBehaviour : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private float _speed;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Destroy());
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
