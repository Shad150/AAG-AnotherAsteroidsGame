using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private float _speed;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();   
    }

    private void Update()
    {
        gameObject.transform.Translate(Vector3.right * _speed *Time.deltaTime);
    }
}
