using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sr;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Transform _direction;
    [SerializeField] private float _speed = 0.7f;
    [SerializeField] private string[] _powerUps;
    [SerializeField] private Sprite[] _powerUpsSprites;

    private void Start()
    {
        int i = Random.Range(0, _powerUps.Length);
        _sr.sprite = _powerUpsSprites[i];
        gameObject.tag = _powerUps[i];
        Debug.Log(gameObject.tag);

        _rb.GetComponent<Rigidbody2D>();
        _direction.transform.rotation = Quaternion.Euler(0,0,Random.Range(0, 360));
        //_rb.SetRotation(_direction.rotation);
        _rb.AddForce(_direction.transform.up * _speed, ForceMode2D.Impulse);

        StartCoroutine(Destroy());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player" || collision.collider.CompareTag("Shield"))
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
