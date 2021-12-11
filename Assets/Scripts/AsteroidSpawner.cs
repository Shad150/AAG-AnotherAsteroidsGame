using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private AsteroidBehaviour _asteroid;

    private float _spawnRate = 2f;
    private int _spawnAmount = 1;

    private float _spawnDistance = 15f;
    private float _coneAngle = 15f;

    void Start()
    {
        InvokeRepeating("Spawn", _spawnRate, _spawnRate);
    }

    void Update()
    {
        
    }

    private void Spawn()
    {
        for (int i = 0; i < _spawnAmount; i++)
        {
            Vector3 spawnDir = Random.insideUnitCircle.normalized * _spawnDistance;     //Una posición en el circulo de spawn
            Vector3 spawnPoint = transform.position + spawnDir;

            float angleCone = Random.Range(-_coneAngle, _coneAngle);        //Para que los asteroides no vayan directos al centro de la pantalla, hago un angulo para darle variacion
            Quaternion rotation = Quaternion.AngleAxis(angleCone, Vector3.forward);     //Se lo aplico al aje Z

            AsteroidBehaviour asteroid = Instantiate(_asteroid, spawnPoint, rotation);
            asteroid._size = Random.Range(asteroid._minSize, asteroid._maxSize);
            asteroid.SetTrayectory(rotation * -spawnDir);      
        }
    }

    
}
