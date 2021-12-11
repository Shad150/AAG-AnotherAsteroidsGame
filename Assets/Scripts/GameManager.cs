using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController _pC;

    [SerializeField] private ParticleSystem _explosion;

    public int _pLives = 3;
    public float _respawnTime = 3f;
    private float _respawnInvulnerability = 3f;

    private int score = 0;

    public void AsteroidDestroyed(AsteroidBehaviour asteroid)
    {
        _explosion.transform.position = asteroid.transform.position;
        _explosion.Play();


    }

    public void PlayerDead()
    {
        _explosion.transform.position = _pC.transform.position;
        _explosion.Play();
        _pLives--;

        if(_pLives <= 0)
        {
            GameOver();
        }
        else
        {
            Invoke(nameof(Respawn), _respawnTime);
        }
    }

    private void Respawn()
    {
        _pC.gameObject.layer = LayerMask.NameToLayer("IgnoreCollision");
        _pC.transform.position = Vector3.zero;
        _pC.gameObject.SetActive(true);
        StartCoroutine(ReEnablePlayer());
    }

    private IEnumerator ReEnablePlayer()
    {
        yield return new WaitForSeconds(_respawnInvulnerability);
        _pC.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void GameOver()
    {

    }
}
