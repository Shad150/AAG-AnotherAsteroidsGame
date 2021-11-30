using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehaviour : MonoBehaviour
{
    private Rigidbody2D _rb;
    private float _impulseForce;

    void Start()
    {
        _rb.AddForce(Vector2.right * _impulseForce, ForceMode2D.Impulse);
    }

    void Update()
    {
        
    }
}
