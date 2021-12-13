using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private string[] _powerUps;

    private void Start()
    {
        int i = Random.Range(0, _powerUps.Length);
        gameObject.tag = _powerUps[i];
        Debug.Log(gameObject.tag);

        _rb.GetComponent<Rigidbody2D>();
        _rb.SetRotation(Random.Range(0, 360));
        _rb.AddForce(transform.up * _speed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
