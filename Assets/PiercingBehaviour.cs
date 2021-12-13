using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiercingBehaviour : MonoBehaviour
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
        gameObject.transform.Translate(Vector3.up * _speed * Time.deltaTime);
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
