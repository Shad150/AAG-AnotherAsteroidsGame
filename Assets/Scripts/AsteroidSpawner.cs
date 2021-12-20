using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private AsteroidBehaviour _asteroid;
    [SerializeField] private EnemyBehaviour _enemy;
    [SerializeField] private Enemy2Behaviour _enemy2;

    private float _spawnRate = 1.5f;
    private int _spawnAmount = 1;

    private float _enemySpawnRate = 10f;
    private int _enemySpawnAmount = 1;

    private float _enemy2SpawnRate = 3f;
    private int _enemy2SpawnAmount = 2;

    private float _spawnDistance = 15f;
    private float _coneAngle = 15f;

    public bool _inMenu;
    public bool _gameOver;

    void Start()
    {
        _gameOver = false;

        if (_inMenu)
        {
            InvokeRepeating("Spawn", 1.5f, 1.5f);
            InvokeRepeating("Spawn2Enemy", 3f, 3f);
            InvokeRepeating("SpawnEnemy", 5f, 5f);
        }
        else
        {
            InvokeRepeating("Spawn2Enemy", _enemy2SpawnRate, _enemySpawnRate);
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

        if (!_gameOver)
        {
            for (int i = 0; i < _enemySpawnAmount; i++)
            {
                Vector3 spawnDir = Random.insideUnitCircle.normalized * _spawnDistance;
                Vector3 spawnPoint = transform.position + spawnDir;

                float angleCone = Random.Range(-_coneAngle, _coneAngle);
                Quaternion rotation = Quaternion.AngleAxis(angleCone, Vector3.forward);

                EnemyBehaviour enemy = Instantiate(_enemy, spawnPoint, rotation);
                enemy.SetTrayectory(rotation * -spawnDir);
            }
        }


    }

    private void Spawn2Enemy()
    {

        for (int i = 0; i < _enemy2SpawnAmount; i++)
        {
            Vector3 spawnDir = Random.insideUnitCircle.normalized * _spawnDistance;
            Vector3 spawnPoint = transform.position + spawnDir;

            float angleCone = Random.Range(-_coneAngle, _coneAngle);
            Quaternion rotation = Quaternion.AngleAxis(angleCone, Vector3.forward);

            Enemy2Behaviour enemy2 = Instantiate(_enemy2, spawnPoint, rotation);
            enemy2.SetTrayectory(rotation * -spawnDir);
        }
    }


}
