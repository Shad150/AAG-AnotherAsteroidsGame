using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private AsteroidBehaviour _asteroid;
    [SerializeField] private EnemyBehaviour _enemy;

    private float _spawnRate = 2f;
    private int _spawnAmount = 1;

    private float _enemySpawnRate = 10f;
    private int _enemySpawnAmount = 1;

    private float _spawnDistance = 15f;
    private float _coneAngle = 15f;

    public bool _inMenu;

    void Start()
    {
        if (_inMenu)
        {
            InvokeRepeating("Spawn", 1.5f, 1.5f);
        }
        else
        {
            InvokeRepeating("Spawn", _spawnRate, _spawnRate);
            InvokeRepeating("SpawnEnemy", _enemySpawnRate, _enemySpawnRate);
        }
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

    private void SpawnEnemy()
    {
        if (!_inMenu)
        {
            for (int i = 0; i < _enemySpawnAmount; i++)
            {
                Vector3 spawnDir = Random.insideUnitCircle.normalized * _spawnDistance;     //Una posición en el circulo de spawn
                Vector3 spawnPoint = transform.position + spawnDir;

                float angleCone = Random.Range(-_coneAngle, _coneAngle);        //Para que los asteroides no vayan directos al centro de la pantalla, hago un angulo para darle variacion
                Quaternion rotation = Quaternion.AngleAxis(angleCone, Vector3.forward);     //Se lo aplico al aje Z

                EnemyBehaviour enemy = Instantiate(_enemy, spawnPoint, rotation);
            }
        }
        
    }

    
}
