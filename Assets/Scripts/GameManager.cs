using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PowerUps _powerUps;
    [SerializeField] private CameraShake _camShake;
    [SerializeField] private PlayerController _pC;
    [SerializeField] public AudioManager _aM;
    [SerializeField] private AsteroidSpawner _aS;
    [SerializeField] private EnemyBehaviour _eB;
    [SerializeField] private ParticleSystem _explosion;
    [SerializeField] private ParticleSystem _asteroidExplosion;
    [SerializeField] private ParticleSystem _enemyExplosion;
    [SerializeField] private ParticleSystem _enemy2Explosion;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _finalScoreText;
    [SerializeField] private Text _maxScoreText;
    [SerializeField] private Text _livesText;
    [SerializeField] private Text _respawnCountText;
    [SerializeField] private Canvas _respawnCountTextCanvas;
    [SerializeField] private GameObject _gameOverMenu;
    [SerializeField] private GameObject _inGamePanel;
    [SerializeField] private GameObject _pauseMenu;
    
    public GameObject _player;
    private bool _paused;

    //private int _lifes = 3;

    //public int Lifes
    //{
    //    get { return _lifes; }
    //    set { _lifes = value; }
    //}
    // public variables ej: PLives instead of _pLives // Deberian ser tipo get/set.

    public int _pLives = 3;     
    public float _respawnTime = 3f;
    private float _respawnInvulnerability = 3f;
    private float _respawnCount = 3f;
    public bool _playerDead;

    private int _score = 0;
    private int _finalScore;
    public int _maxScore;
    private int _1upScore = 5000;

    private void Start()
    {
        _1upScore = 5000;
        _respawnCountText.enabled = false;
        _pauseMenu.SetActive(false);
        _aS._inMenu = false;
        _eB._inMenu = false;
        _inGamePanel.SetActive(true);
        _gameOverMenu.SetActive(false);
        _livesText.text = _pLives.ToString();
    }

    private void Update()
    {
        print(_eB._inMenu);
        //PAUSE MENU
        if (Input.GetKeyDown(KeyCode.Escape) || (Input.GetKeyDown(KeyCode.P)))
        {
            if (_paused)
            {
                Time.timeScale = 1;

                _pauseMenu.SetActive(false);
                _paused = false;
            }
            else
            {
                Time.timeScale = 0;

                _pauseMenu.SetActive(true);
                _paused = true;
            }
            
        }

        _respawnCountTextCanvas.transform.rotation = Quaternion.Euler(Vector3.zero);

        if (_score >= _1upScore)
        {
            _pLives++;
            _aM.OneUp();
            _1upScore += 5000;
            OneUPTextUpdate();
        }
    }

    public void AsteroidDestroyed(AsteroidBehaviour asteroid)
    {
        _asteroidExplosion.transform.position = asteroid.transform.position;
        _asteroidExplosion.Play();
        _aM.AsteroidExplosion();
        StartCoroutine(_camShake.Shake(0.15f, 0.1f));

        if (asteroid._size < 0.75f)
        {
            _score += 100;
        }
        else if (asteroid._size < 1.3f)
        {
            _score += 50;
        }
        else
        {
            _score += 10;
        }

        _scoreText.text = _score.ToString();
         
    }

    public void EnemyDestroyed(EnemyBehaviour enemy)
    {
        StartCoroutine(_camShake.Shake(0.15f, 0.1f));
        _enemyExplosion.transform.position = enemy.transform.position;
        _enemyExplosion.Play();

        _score += 200;
        _scoreText.text = _score.ToString();
    }

    public void EnemySinDestroyed(Enemy2Behaviour enemy2)
    {
        StartCoroutine(_camShake.Shake(0.15f, 0.1f));
        _enemy2Explosion.transform.position = enemy2.transform.position;
        _enemy2Explosion.Play();

        _score += 150;
        _scoreText.text = _score.ToString();
    }

    public void PlayerDead()
    {
        StartCoroutine(_camShake.Shake(0.15f, 0.1f));
        _playerDead = true;
        _explosion.transform.position = _pC.transform.position;
        _explosion.Play();
        _aM.PlayerExplosion();
        _pLives--;
        _livesText.text = _pLives.ToString();

        if(_pLives < 0)
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
        _playerDead = false;
        StartCoroutine(ReEnablePlayer());
    }

    private IEnumerator ReEnablePlayer()
    {
        StartCoroutine(InvulnerabilityFrames());
        yield return new WaitForSeconds(_respawnInvulnerability);
        _pC.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private IEnumerator InvulnerabilityFrames()
    {
        Color alpha = Color.blue;
        alpha.a = 125;
        _respawnCountText.enabled = true;
        _respawnCountText.text = _respawnCount.ToString();

        yield return new WaitForSeconds(0.5f);
        _pC.GetComponent<SpriteRenderer>().color = alpha;
        yield return new WaitForSeconds(0.5f);
        _pC.GetComponent<SpriteRenderer>().color = Color.white;
        _respawnCount--;
        _respawnCountText.text = _respawnCount.ToString();
        yield return new WaitForSeconds(0.5f);
        _pC.GetComponent<SpriteRenderer>().color = alpha;
        yield return new WaitForSeconds(0.5f);
        _pC.GetComponent<SpriteRenderer>().color = Color.white;
        _respawnCount--;
        _respawnCountText.text = _respawnCount.ToString();
        yield return new WaitForSeconds(0.5f);
        _pC.GetComponent<SpriteRenderer>().color = alpha;
        yield return new WaitForSeconds(0.5f);
        _pC.GetComponent<SpriteRenderer>().color = Color.white;
        _respawnCount--;
        _respawnCountText.text = _respawnCount.ToString();

        _respawnCount = 3f;
        _respawnCountText.enabled = false;
    }

    public void OneUPTextUpdate()
    {
        _livesText.text = _pLives.ToString();
    }

    private void GameOver()
    {
        _aS._gameOver = true;
        _inGamePanel.SetActive(false);
        _finalScore = _score;
        _finalScoreText.text = _finalScore.ToString();
        _maxScore = SaveSystem.LoadRecord();

        if (_score > _maxScore)
        {
            _maxScore = _score;
            SaveRecord();
        }
        _maxScoreText.text = _maxScore.ToString();
        

        _pLives = 3;
        _score = 0;

        _gameOverMenu.SetActive(true);
    }
    public void ContinueT()
    {
        Time.timeScale = 1;
        _pauseMenu.SetActive(false);
        _paused = false;
    }

    public void RestartLevel()
    {
        StartCoroutine(RL());
    }

    public void MainMenu()
    {
        StartCoroutine(MM());
    }

    private IEnumerator RL()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Game");
    }

    private IEnumerator MM()
    {
        Time.timeScale = 1;
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("TitleScreenV2");
    }

    public void SaveRecord()
    {
        SaveSystem.SaveRecord(this);
    }
    
    public void LoadRecord()
    {

        //RecordData data = SaveSystem.LoadRecord();

        //_maxScore = data._maxScore;
    }
}
